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
    public class WorkersService : IWorkersService, IServiceAnchor
    {
        private readonly IWorkersReadRepository workersReadRepository;
        private readonly IClientsReadRepository clientsReadRepository;
        private readonly IWorkersWriteRepository workersWriteRepository;
        private readonly IAccessKeyReadRepository accessKeyReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public WorkersService(IWorkersReadRepository workersReadRepository,
            IWorkersWriteRepository workersWriteRepository,
            IClientsReadRepository clientsReadRepository,
            IAccessKeyReadRepository accessKeyReadRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.workersReadRepository = workersReadRepository;
            this.workersWriteRepository = workersWriteRepository;
            this.clientsReadRepository = clientsReadRepository;
            this.accessKeyReadRepository = accessKeyReadRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }



        async Task<IEnumerable<WorkerModel>> IWorkersService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await workersReadRepository.GetAllAsync(cancellationToken);

            var clients = await clientsReadRepository.GetByIdsAsync(result.Select(x => x.ClientId).Distinct(), cancellationToken);

            var listWorker = new List<WorkerModel>();
            foreach (var worker in result)
            {
                var work = mapper.Map<WorkerModel>(worker);
                if (!clients.TryGetValue(worker.ClientId, out var client))
                {
                    continue;
                }
                work.Clients = mapper.Map<ClientModel>(client);
                listWorker.Add(work);
            }
            return listWorker;
        }

        async Task<WorkerModel?> IWorkersService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await workersReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null) return null;


            var client = await clientsReadRepository.GetByIdAsync(item.ClientId, cancellationToken);

            var work = mapper.Map<WorkerModel>(item);
            work.Clients = mapper.Map<ClientModel>(client);

            return work;
        }

        async Task<WorkerModel> IWorkersService.AddAsync(WorkerRequestModel source, CancellationToken cancellationToken)
        {
            var item = new Worker
            {
                Id = Guid.NewGuid(),
                Number = source.Number,
                Series = source.Series,
                IssuedAt = source.IssuedAt,
                IssuedBy = source.IssuedBy,
                DocumentType = source.DocumentType,
                AccessLevel = source.AccessLevel,
                ClientId = source.ClientId,

            };
            workersWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<WorkerModel>(item);
        }

        async Task<WorkerModel> IWorkersService.EditAccessKeyAsync(Guid id, Guid key, CancellationToken cancellationToken)
        {
            var targetWorker = await workersReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetWorker == null)
            {
                throw new AccessoriesEntityNotFoundException<Client>(id);
            }

            var targetAccessLevel = await  accessKeyReadRepository.GetAccessLevelByKeyAsync(key, cancellationToken);
            if(targetAccessLevel == null)
            {
                throw new AccessoriesInvalidOperationException($"Такого ключа нет в наличии!");
            }
            targetWorker.AccessLevel = targetAccessLevel.Value;

            workersWriteRepository.Update(targetWorker);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<WorkerModel>(targetWorker);
        }

        async Task<WorkerModel> IWorkersService.EditAsync(WorkerRequestModel source, CancellationToken cancellationToken)
        {
            var targetWorker = await workersReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetWorker == null)
            {
                throw new AccessoriesEntityNotFoundException<Client>(source.Id);
            }

            targetWorker.Number = source.Number;
            targetWorker.Series = source.Series;
            targetWorker.IssuedAt = source.IssuedAt;
            targetWorker.IssuedBy = source.IssuedBy;
            targetWorker.DocumentType = source.DocumentType;
            targetWorker.AccessLevel = source.AccessLevel;

            var client = await clientsReadRepository.GetByIdAsync(source.ClientId, cancellationToken);
            targetWorker.ClientId = client!.Id;
            targetWorker.Client = client;

            workersWriteRepository.Update(targetWorker);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<WorkerModel>(targetWorker);
        }

        async Task IWorkersService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetWorker = await workersReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetWorker == null)
            {
                throw new AccessoriesEntityNotFoundException<Service>(id);
            }
            if (targetWorker.DeletedAt.HasValue)
            {
                throw new AccessoriesInvalidOperationException($"Сервис с идентификатором {id} уже удален");
            }

            workersWriteRepository.Delete(targetWorker);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
