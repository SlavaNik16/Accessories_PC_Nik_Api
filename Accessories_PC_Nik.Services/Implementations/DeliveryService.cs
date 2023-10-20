using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Services.Anchors;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Models;
using AutoMapper;

namespace Accessories_PC_Nik.Services.Implementations
{
    public class DeliveryService : IDeliveryService, IServiceAnchor
    {
        private readonly IDeliveryReadRepository deliveryReadRepository;
         private readonly IMapper mapper;
        public DeliveryService(IDeliveryReadRepository deliveryReadRepository,
            IMapper mapper)
        {
            this.deliveryReadRepository = deliveryReadRepository;
            this.mapper = mapper;
        }
        async Task<IEnumerable<DeliveryModel>> IDeliveryService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await deliveryReadRepository.GetAllAsync(cancellationToken);

            return mapper.Map<IEnumerable<DeliveryModel>>(result);
        }

        async Task<DeliveryModel?> IDeliveryService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await deliveryReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null) return null;

            return mapper.Map<DeliveryModel>(item);
        }
    }
}
