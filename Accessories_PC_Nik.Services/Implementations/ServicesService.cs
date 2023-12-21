using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Services.Anchors;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Models;
using AutoMapper;

namespace Accessories_PC_Nik.Services.Implementations
{
    public class ServicesService : IServicesService, IServiceAnchor
    {
        private readonly IServicesReadRepository servicesReadRepository;
        private readonly IMapper mapper;
        public ServicesService(IServicesReadRepository servicesReadRepository,
             IMapper mapper)
        {
            this.servicesReadRepository = servicesReadRepository;
            this.mapper = mapper;
        }
        async Task<IEnumerable<ServiceModel>> IServicesService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await servicesReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<ServiceModel>>(result);
        }

        async Task<ServiceModel?> IServicesService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await servicesReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null) return null;

            return mapper.Map<ServiceModel>(item);
        }
    }
}
