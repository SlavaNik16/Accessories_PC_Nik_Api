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
    public class ComponentsService : IComponentsService, IServiceAnchor
    {
        private readonly IComponentsReadRepository componentsReadRepository;
        private readonly IComponentsWriteRepository componentsWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public ComponentsService(IComponentsReadRepository componentsReadRepository,
            IComponentsWriteRepository componentsWriteRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.componentsReadRepository = componentsReadRepository;
            this.componentsWriteRepository = componentsWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IEnumerable<ComponentModel>> IComponentsService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await componentsReadRepository.GetAllAsync(cancellationToken);

            return mapper.Map<IEnumerable<ComponentModel>>(result);
        }

        async Task<ComponentModel?> IComponentsService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await componentsReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null) return null;

            return mapper.Map<ComponentModel>(item);
        }
        async Task<ComponentModel> IComponentsService.AddAsync(ComponentRequestModel source, CancellationToken cancellationToken)
        {
            var item = new Component
            {
                Id = Guid.NewGuid(),
                TypeComponents = source.TypeComponents,
                Description = source.Description,
                MaterialType = source.MaterialType,
                Price = source.Price,
                Count = source.Count
            };
            componentsWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<ComponentModel>(item);
        }
        async Task<ComponentModel> IComponentsService.EditAsync(ComponentRequestModel source, CancellationToken cancellationToken)
        {
            var targetComponent = await componentsReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetComponent == null)
            {
                throw new AccessoriesEntityNotFoundException<Client>(source.Id);
            }

            targetComponent.MaterialType = source.MaterialType;
            targetComponent.Description = source.Description;
            targetComponent.MaterialType = source.MaterialType;
            targetComponent.Price = source.Price;
            targetComponent.Count = source.Count;

            componentsWriteRepository.Update(targetComponent);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<ComponentModel>(targetComponent);
        }
        async Task IComponentsService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetComponent = await componentsReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetComponent == null)
            {
                throw new AccessoriesEntityNotFoundException<Component>(id);
            }
            if (targetComponent.DeletedAt.HasValue)
            {
                throw new AccessoriesInvalidOperationException($"Компонент с идентификатором {id} уже удален");
            }

            componentsWriteRepository.Delete(targetComponent);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
