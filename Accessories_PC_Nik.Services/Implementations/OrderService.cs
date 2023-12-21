using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Services.Anchors;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Models;
using AutoMapper;

namespace Accessories_PC_Nik.Services.Implementations
{
    public class OrderService : IOrderService, IServiceAnchor
    {
        private readonly IOrderReadRepository orderReadRepository;
        private readonly IServicesReadRepository servicesReadRepository;
        private readonly IComponentsReadRepository componentsReadRepository;
        private readonly IDeliveryReadRepository deliveryReadRepository;
        private readonly IClientsReadRepository clientsReadRepository;
        private readonly IMapper mapper;
        public OrderService(IOrderReadRepository orderReadRepository,
             IServicesReadRepository servicesReadRepository,
             IComponentsReadRepository componentsReadRepository,
             IDeliveryReadRepository deliveryReadRepository,
             IClientsReadRepository clientsReadRepository,
             IMapper mapper )
        {
            this.orderReadRepository = orderReadRepository;
            this.servicesReadRepository = servicesReadRepository;
            this.componentsReadRepository = componentsReadRepository;
            this.deliveryReadRepository = deliveryReadRepository;
            this.clientsReadRepository = clientsReadRepository;
            this.mapper = mapper;
        }
        async Task<IEnumerable<OrderModel>> IOrderService.GetAllAsync(CancellationToken cancellationToken)
        {
            var orders = await orderReadRepository.GetAllAsync(cancellationToken);
           
            var servicesId = orders.Select(x=>x.ServiceId).Distinct().Cast<Guid>();
            var componentsId = orders.Select(x => x.ComponentId).Distinct().Cast<Guid>();
            var deliveriesId = orders.Select(x => x.DeliveryId).Distinct().Cast<Guid>();
            var clientsId = orders.Select(x => x.ClientId).Distinct().Cast<Guid>();

            var services = await servicesReadRepository.GetByIdsAsync(servicesId, cancellationToken);
            var components = await componentsReadRepository.GetByIdsAsync(componentsId, cancellationToken);
            var deliveries = await deliveryReadRepository.GetByIdsAsync(deliveriesId, cancellationToken);
            var clients = await clientsReadRepository.GetByIdsAsync(clientsId, cancellationToken);

            var listOrders = new List<OrderModel>();

            foreach(var order in orders)
            {
                var ord = mapper.Map<OrderModel>(order);

                if(order.ServiceId.HasValue && 
                   services.TryGetValue(order.ServiceId!.Value, out var service))
                {
                    ord.Services = mapper.Map<ServiceModel>(service);
                }
                if (order.ComponentId.HasValue &&
                    components.TryGetValue(order.ComponentId!.Value, out var component))
                {
                    ord.Components = mapper.Map<ComponentModel>(component);
                }
                //В заказы должен быть хотя бы 1 услуга или покупка
                if(ord.Components == null && ord.Services == null)
                {
                    continue;
                }
                if (order.DeliveryId.HasValue &&
                    deliveries.TryGetValue(order.DeliveryId!.Value, out var delivery))
                {
                    ord.Delivery = mapper.Map<DeliveryModel>(delivery);
                }
                if (!clients.TryGetValue(order.ClientId, out var client))
                {
                    continue;
                }

                ord.Clients = mapper.Map<Contracts.Models.ClientModel>(client);

                listOrders.Add(ord);
            }
            return listOrders;
        }

        async Task<OrderModel?> IOrderService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await orderReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null) return null;

            var order = mapper.Map<OrderModel>(item);

            if(item.ServiceId.HasValue)
            {
                var service = await servicesReadRepository.GetByIdAsync(item.ServiceId!.Value, cancellationToken);
                order.Services = mapper.Map<ServiceModel>(service);
            }
            if (item.ComponentId.HasValue)
            {
                var component = await componentsReadRepository.GetByIdAsync(item.ComponentId!.Value, cancellationToken);
                order.Components = mapper.Map<ComponentModel>(component);
            }
            if (item.DeliveryId.HasValue)
            {
                var delivery = await deliveryReadRepository.GetByIdAsync(item.DeliveryId!.Value, cancellationToken);
                order.Delivery = mapper.Map<DeliveryModel>(delivery);
            }
            
            var client = await deliveryReadRepository.GetByIdAsync(item.ClientId, cancellationToken);
            order.Clients = mapper.Map<Contracts.Models.ClientModel>(client);
            return order;
        }
    }
}
