using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Models;

namespace Accessories_PC_Nik.Services.Implementations
{
    public class ComponentsService : IComponentsService
    {
        private readonly IComponentsReadRepository componentsReadRepository;
        public ComponentsService(IComponentsReadRepository componentsReadRepository)
        {
            this.componentsReadRepository = componentsReadRepository;
        }
        async Task<IEnumerable<ComponentsModel>> IComponentsService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await componentsReadRepository.GetAllAsync(cancellationToken);

            return result.Select(x => new ComponentsModel
            {
                    Id = x.Id,
                    typeComponents = x.typeComponents,
                    Description = x.Description,
                    MaterialType = x.MaterialType,
                    Price = x.Price,
            });
        }

        async Task<ComponentsModel?> IComponentsService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await componentsReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null) return null;

            return new ComponentsModel
            {
                Id = item.Id,
                typeComponents = item.typeComponents,
                Description = item.Description,
                MaterialType = item.MaterialType,
                Price = item.Price,
               
            };
        }
    }
}
