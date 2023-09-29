using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Models;

namespace Accessories_PC_Nik.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderReadRepository orderReadRepository;
        public OrderService(IOrderReadRepository orderReadRepository)
        {
            this.orderReadRepository = orderReadRepository;
        }
        async Task<IEnumerable<OrderModel>> IOrderService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await orderReadRepository.GetAllAsync(cancellationToken);
            
            return result.Select(x => new OrderModel
            {
                Id = x.Id,
                Client_id = x.Сlient_id,
                ServicesModel = new ServicesModel
                {
                    Id = x.Services.Id,
                    Name = x.Services.Name,
                    Description = x.Services.Description,
                    Duration = x.Services.Duration,
                    Price = x.Services.Price,
                },
                ComponentsModel = new ComponentsModel
                {
                    Id = x.Сomponents.Id,
                    typeComponents = x.Сomponents.typeComponents,
                    Description = x.Сomponents.Description,
                    MaterialType = x.Сomponents.MaterialType,
                    Price = x.Сomponents.Price,
                },
                Count = x.Count,
                OrderTime = x.OrderTime,
                DeliveryModel = new DeliveryModel
                {
                    Id = x.Delivery.Id,
                    IsDelivery = x.Delivery.IsDelivery,
                    From = x.Delivery.From,
                    To = x.Delivery.To,
                    Price = x.Delivery.Price,
                },
                Comment = x.Comment,
            });
        }

        async Task<OrderModel?> IOrderService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await orderReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null) return null;

            return new OrderModel
            {
                Id = item.Id,
                Client_id = item.Сlient_id,
                ServicesModel = new ServicesModel
                {
                    Id = item.Services.Id,
                    Name = item.Services.Name,
                    Description = item.Services.Description,
                    Duration = item.Services.Duration,
                    Price = item.Services.Price,
                },
                ComponentsModel = new ComponentsModel
                {
                    Id = item.Сomponents.Id,
                    typeComponents = item.Сomponents.typeComponents,
                    Description = item.Сomponents.Description,
                    MaterialType = item.Сomponents.MaterialType,
                    Price = item.Сomponents.Price,
                },
                Count = item.Count,
                OrderTime = item.OrderTime,
                DeliveryModel = new DeliveryModel
                {
                    Id = item.Delivery.Id,
                    IsDelivery = item.Delivery.IsDelivery,
                    From = item.Delivery.From,
                    To = item.Delivery.To,
                    Price = item.Delivery.Price,
                },
                Comment = item.Comment,
            };
        }
    }
}
