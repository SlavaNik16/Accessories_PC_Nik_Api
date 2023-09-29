using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Models;

namespace Accessories_PC_Nik.Services.Implementations
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryReadRepository deliveryReadRepository;
        public DeliveryService(IDeliveryReadRepository deliveryReadRepository)
        {
            this.deliveryReadRepository = deliveryReadRepository;
        }
        async Task<IEnumerable<DeliveryModel>> IDeliveryService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await deliveryReadRepository.GetAllAsync(cancellationToken);

            return result.Select(x => new DeliveryModel
            {
                Id = x.Id,
                IsDelivery = x.IsDelivery,
                From = x.From,
                To = x.To,
                Price = x.Price,
            });
        }

        async Task<DeliveryModel?> IDeliveryService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await deliveryReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null) return null;

            return new DeliveryModel
            {
                Id = item.Id,
                IsDelivery = item.IsDelivery,
                From = item.From,
                To = item.To,
                Price = item.Price,
            };
        }
    }
}
