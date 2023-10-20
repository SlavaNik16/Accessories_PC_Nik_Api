using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Models;
using AutoMapper;

namespace Accessories_PC_Nik.Services.Implementations
{
    public class OrderService : IOrderService
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
            IEnumerable<Guid> servicesId, componentsId, deliveriesId, clientsId;
            servicesId = orders.Select(x=>x.Services_id).Distinct().Cast<Guid>();
            componentsId = orders.Select(x => x.Components_id).Distinct().Cast<Guid>();
            deliveriesId = orders.Select(x => x.Delivery_id).Distinct().Cast<Guid>();
            clientsId = orders.Select(x => x.Client_id).Distinct().Cast<Guid>();

            var services = await servicesReadRepository.GetByIdsAsync(servicesId, cancellationToken);
            var components = await componentsReadRepository.GetByIdsAsync(componentsId, cancellationToken);
            var deliveries = await deliveryReadRepository.GetByIdsAsync(deliveriesId, cancellationToken);
            var clients = await clientsReadRepository.GetByIdsAsync(clientsId, cancellationToken);

            var listOrders = new List<OrderModel>();

            foreach(var order in orders)
            {
                var ord = mapper.Map<OrderModel>(order);

                services.TryGetValue(order.Services_id ?? Guid.Empty, out var service);
                components.TryGetValue(order.Components_id ?? Guid.Empty, out var component);
                deliveries.TryGetValue(order.Delivery_id ?? Guid.Empty, out var delivery);
                clients.TryGetValue(order.Client_id, out var client);

                ord.ServicesModel = mapper.Map<ServicesModel>(service);
                ord.ComponentsModel = mapper.Map<ComponentsModel>(component);
                ord.DeliveryModel = mapper.Map<DeliveryModel>(delivery);
                ord.ClientsModel = mapper.Map<ClientsModel>(client);

                listOrders.Add(ord);
            }
            return listOrders;
        }

        async Task<OrderModel?> IOrderService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await orderReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null) return null;

            var service = await servicesReadRepository.GetByIdAsync(item.Services_id ?? Guid.Empty, cancellationToken);
            var component = await componentsReadRepository.GetByIdAsync(item.Components_id ?? Guid.Empty, cancellationToken);
            var delivery = await deliveryReadRepository.GetByIdAsync(item.Delivery_id ?? Guid.Empty, cancellationToken);
            var client = await deliveryReadRepository.GetByIdAsync(item.Client_id, cancellationToken);

            var order = mapper.Map<OrderModel>(item);
            order.ServicesModel = service != null
                ? mapper.Map<ServicesModel>(service)
                : null;
            order.ComponentsModel = component != null
               ? mapper.Map<ComponentsModel>(component)
               : null;
            order.DeliveryModel = delivery != null
               ? mapper.Map<DeliveryModel>(delivery)
               : null;

            order.ClientsModel = mapper.Map<ClientsModel>(client);

            return order;
        }
    }
}
