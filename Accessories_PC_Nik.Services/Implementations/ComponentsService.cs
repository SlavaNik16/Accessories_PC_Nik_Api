using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Services.Anchors;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Models;
using AutoMapper;

namespace Accessories_PC_Nik.Services.Implementations
{
    public class ComponentsService : IComponentsService, IServiceAnchor
    {
        private readonly IComponentsReadRepository componentsReadRepository;
        private readonly IMapper mapper;
        public ComponentsService(IComponentsReadRepository componentsReadRepository,
            IMapper mapper)
        {
            this.componentsReadRepository = componentsReadRepository;
            this.mapper = mapper;
        }
        async Task<IEnumerable<ComponentsModel>> IComponentsService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await componentsReadRepository.GetAllAsync(cancellationToken);

            return mapper.Map<IEnumerable<ComponentsModel>>(result);
        }

        async Task<ComponentsModel?> IComponentsService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await componentsReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null) return null;

            return mapper.Map<ComponentsModel>(item);
        }
    }
}
