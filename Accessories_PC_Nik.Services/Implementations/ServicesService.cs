using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Models;

namespace Accessories_PC_Nik.Services.Implementations
{
    public class ServicesService : IServicesService
    {
        private readonly IServicesReadRepository servicesReadRepository;
        public ServicesService(IServicesReadRepository servicesReadRepository)
        {
            this.servicesReadRepository = servicesReadRepository;
        }
        async Task<IEnumerable<ServicesModel>> IServicesService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await servicesReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => new ServicesModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Duration = x.Duration,
                Price = x.Price,
            });
        }

        async Task<ServicesModel?> IServicesService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await servicesReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null) return null;

            return new ServicesModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Duration = item.Duration,
                Price = item.Price,
            };
        }
    }
}
