using Accessories_PC_Nik.Common.Entity.InterfaceDB;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Services.Anchors;
using Accessories_PC_Nik.Services.Contracts.Exceptions;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.ModelRequest;
using Accessories_PC_Nik.Services.Contracts.Models;
using AutoMapper;

namespace Accessories_PC_Nik.Services.Implementations
{
    public class AccessKeyService : IAccessKeyService, IServiceAnchor
    {
        private readonly IAccessKeyReadRepository accessKeyReadRepository;
        private readonly IAccessKeyWriteRepository accessKeyWriteRepository;
        private readonly IWorkersReadRepository workersReadRepository;
        private readonly IClientsReadRepository clientsReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public AccessKeyService(IAccessKeyReadRepository accessKeyReadRepository,
            IAccessKeyWriteRepository accessKeyWriteRepository,
            IWorkersReadRepository workersReadRepository,
            IClientsReadRepository clientsReadRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.accessKeyReadRepository = accessKeyReadRepository;
            this.accessKeyWriteRepository = accessKeyWriteRepository;
            this.workersReadRepository = workersReadRepository;
            this.clientsReadRepository = clientsReadRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }



        async Task<IEnumerable<AccessKeyModel>> IAccessKeyService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await accessKeyReadRepository.GetAllAsync(cancellationToken);

            var workersId = result.Select(x => x.WorkerId).Distinct();

            var workers = await workersReadRepository.GetByIdsAsync(workersId, cancellationToken);

            var listAccessKey = new List<AccessKeyModel>();

            foreach (var accessKey in result)
            {
                var access = mapper.Map<AccessKeyModel>(accessKey);

                if (!workers.TryGetValue(accessKey.WorkerId, out var worker))
                {
                    continue;
                }
                access.Worker = mapper.Map<WorkerModel>(worker);
                var client = await clientsReadRepository.GetByIdAsync(worker.ClientId, cancellationToken);
                if (client == null) continue;
                access.WorkerClient = mapper.Map<ClientModel>(client);
                listAccessKey.Add(access);
            }
            return listAccessKey;
        }

        async Task<AccessKeyModel?> IAccessKeyService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await accessKeyReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                throw new AccessoriesEntityNotFoundException<AccessKey>(id);
            }

            var worker = await workersReadRepository.GetByIdAsync(item.WorkerId, cancellationToken);
            var client = await clientsReadRepository.GetByIdAsync(worker!.ClientId, cancellationToken);
            var accessKeyModel = mapper.Map<AccessKeyModel>(item);
            accessKeyModel.Worker = mapper.Map<WorkerModel>(worker);
            accessKeyModel.WorkerClient = mapper.Map<ClientModel>(client);
            return accessKeyModel;
        }
        async Task<AccessKeyModel> IAccessKeyService.AddAsync(AccessKeyRequestModel source, CancellationToken cancellationToken)
        {
            var item = new AccessKey
            {
                Id = Guid.NewGuid(),
                Key = Guid.NewGuid(),
                Types = source.Types,
                WorkerId = source.WorkerId,
            };

            var worker = await workersReadRepository.GetByIdAsync(source.WorkerId, cancellationToken);
            if (worker == null)
            {
                throw new AccessoriesEntityNotFoundException<Worker>(source.WorkerId);
            }

            var workerExists = await workersReadRepository.AnyByWorkerWithTypeAsync(source.WorkerId, source.Types, cancellationToken);
            if (!workerExists)
            {
                throw new AccessoriesInvalidOperationException("Данный сотрудник не обладает правами, создавать ключ: c таким же уровнем или выше своего уровня доступа");
            }

            var client = await clientsReadRepository.GetByIdAsync(worker.ClientId, cancellationToken);

            accessKeyWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            var accessKeyModel = mapper.Map<AccessKeyModel>(item);
            accessKeyModel.Worker = mapper.Map<WorkerModel>(worker);
            accessKeyModel.WorkerClient = mapper.Map<ClientModel>(client);
            return accessKeyModel;
        }

        async Task IAccessKeyService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetAccessKey = await accessKeyReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetAccessKey == null)
            {
                throw new AccessoriesEntityNotFoundException<AccessKey>(id);
            }

            accessKeyWriteRepository.Delete(targetAccessKey);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
