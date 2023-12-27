using Accessories_PC_Nik.Common.Entity.InterfaceDB;
using Accessories_PC_Nik.Context.Contracts.Enums;
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
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public AccessKeyService(IAccessKeyReadRepository accessKeyReadRepository,
            IAccessKeyWriteRepository accessKeyWriteRepository,
            IWorkersReadRepository workersReadRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.accessKeyReadRepository = accessKeyReadRepository;
            this.accessKeyWriteRepository = accessKeyWriteRepository;
            this.workersReadRepository = workersReadRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }



        async Task<IEnumerable<AccessKeyModel>> IAccessKeyService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await accessKeyReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<AccessKeyModel>>(result);
        }

        async Task<AccessKeyModel?> IAccessKeyService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await accessKeyReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null) return null;

            return mapper.Map<AccessKeyModel>(item);
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

            var workerExists = await workersReadRepository.AnyByWorkerWithTypeAsync(source.WorkerId, source.Types, cancellationToken);
            if (!workerExists)
            {
                throw new AccessoriesInvalidOperationException($"Данный сотрудник не обладает правами, создавать ключ: c таким же уровнем или выше своего уровня доступа");
            }

            accessKeyWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<AccessKeyModel>(item);
        }

        async Task IAccessKeyService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetAccessKey = await accessKeyReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetAccessKey == null)
            {
                throw new AccessoriesEntityNotFoundException<AccessKey>(id);
            }
            if (targetAccessKey.DeletedAt.HasValue)
            {
                throw new AccessoriesInvalidOperationException($"Ключ с идентификатором {id} уже удален");
            }

            accessKeyWriteRepository.Delete(targetAccessKey);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
