using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Models;
using AutoMapper;

namespace Accessories_PC_Nik.Services.Implementations
{
    public class ServicesService : IServicesService
    {
        private readonly IServicesReadRepository servicesReadRepository;
        private readonly IMapper mapper;
        public ServicesService(IServicesReadRepository servicesReadRepository,
             IMapper mapper)
        {
            this.servicesReadRepository = servicesReadRepository;
            this.mapper = mapper;
        }
        async Task<IEnumerable<ServicesModel>> IServicesService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await servicesReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<ServicesModel>>(result);
        }

        async Task<ServicesModel?> IServicesService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await servicesReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null) return null;

            return mapper.Map<ServicesModel>(item);
        }
    }
}
