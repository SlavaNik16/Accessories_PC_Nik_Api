using Accessories_PC_Nik.Common.Entity.InterfaceDB;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Services.Anchors;
using Accessories_PC_Nik.Services.Contracts.Exceptions;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.ModelRequest;
using Accessories_PC_Nik.Services.Contracts.Models;
using AutoMapper;

namespace Accessories_PC_Nik.Services.Implementations
{
    public class OrderService : IOrderService, IServiceAnchor
    {
        private readonly IOrderReadRepository orderReadRepository;
        private readonly IOrderWriteRepository orderWriteRepository;
        private readonly IUnitOfWork unitOfWork;
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
             IOrderWriteRepository orderWriteRepository,
             IUnitOfWork unitOfWork,
             IMapper mapper)
        {
            this.orderReadRepository = orderReadRepository;
            this.servicesReadRepository = servicesReadRepository;
            this.componentsReadRepository = componentsReadRepository;
            this.deliveryReadRepository = deliveryReadRepository;
            this.clientsReadRepository = clientsReadRepository;
            this.orderWriteRepository = orderWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        async Task<IEnumerable<OrderModel>> IOrderService.GetAllAsync(CancellationToken cancellationToken)
        {
            var orders = await orderReadRepository.GetAllAsync(cancellationToken);

            var servicesId = orders.Where(x => x.ServiceId!.HasValue).Select(x => x.ServiceId!.Value).Distinct();
            var componentsId = orders.Where(x => x.ComponentId!.HasValue).Select(x => x.ComponentId!.Value).Distinct();
            var deliveriesId = orders.Where(x => x.DeliveryId!.HasValue).Select(x => x.DeliveryId!.Value).Distinct();
            var clientsId = orders.Select(x => x.ClientId).Distinct();

            var services = await servicesReadRepository.GetByIdsAsync(servicesId, cancellationToken);
            var components = await componentsReadRepository.GetByIdsAsync(componentsId, cancellationToken);
            var deliveries = await deliveryReadRepository.GetByIdsAsync(deliveriesId, cancellationToken);
            var clients = await clientsReadRepository.GetByIdsAsync(clientsId, cancellationToken);

            var listOrders = new List<OrderModel>();

            foreach (var order in orders)
            {
                var ord = mapper.Map<OrderModel>(order);

                if (order.ServiceId.HasValue &&
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
                if (ord.Components == null && ord.Services == null)
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

                ord.Clients = mapper.Map<ClientModel>(client);

                listOrders.Add(ord);
            }
            return listOrders;
        }

        async Task<OrderModel?> IOrderService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await orderReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null) return null;

            var order = mapper.Map<OrderModel>(item);

            if (item.ServiceId.HasValue)
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
            order.Clients = mapper.Map<ClientModel>(client);
            return order;
        }

        async Task<OrderModel> IOrderService.AddAsync(OrderRequestModel source, CancellationToken cancellationToken)
        {
            var item = new Order
            {
                Id = Guid.NewGuid(),
                ClientId = source.ClientId,
                ServiceId = source.ServiceId,
                ComponentId = source.ComponentId,
                OrderTime = source.OrderTime,
                DeliveryId = source.DeliveryId,
                Comment = source.Comment,

            };

            if (item.ComponentId == null && item.ServiceId == null)
            {
                throw new AccessoriesInvalidOperationException($"Заказ без покупок недействителен! Нужно хотя бы выбрать компонент или услугу!");
            }

            orderWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<OrderModel>(item);
        }

        async Task<OrderModel> IOrderService.EditAsync(OrderRequestModel source, CancellationToken cancellationToken)
        {
            var targetOrder = await orderReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetOrder == null)
            {
                throw new AccessoriesEntityNotFoundException<Client>(source.Id);
            }

            targetOrder.OrderTime = source.OrderTime;
            targetOrder.Comment = source.Comment;

            if (source.ComponentId == null && source.ServiceId == null)
            {
                throw new AccessoriesInvalidOperationException($"Заказ без покупок недействителен! Нужно хотя бы выбрать компонент или услугу!");
            }

            var client = await clientsReadRepository.GetByIdAsync(source.ClientId, cancellationToken);
            targetOrder.ClientId = client!.Id;
            targetOrder.Client = client;

            if (source.ServiceId.HasValue)
            {
                var service = await servicesReadRepository.GetByIdAsync(source.ServiceId.Value, cancellationToken);
                targetOrder.ServiceId = service!.Id;
                targetOrder.Service = service;
            }

            if (source.ComponentId.HasValue)
            {
                var component = await componentsReadRepository.GetByIdAsync(source.ComponentId.Value, cancellationToken);
                targetOrder.ComponentId = component!.Id;
                targetOrder.Component = component;
            }

            if (source.DeliveryId.HasValue)
            {
                var delivery = await deliveryReadRepository.GetByIdAsync(source.DeliveryId.Value, cancellationToken);
                targetOrder.DeliveryId = delivery!.Id;
                targetOrder.Delivery = delivery;
            }



            orderWriteRepository.Update(targetOrder);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<OrderModel>(targetOrder);
        }
        async Task IOrderService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetComponent = await orderReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetComponent == null)
            {
                throw new AccessoriesEntityNotFoundException<Order>(id);
            }
            if (targetComponent.DeletedAt.HasValue)
            {
                throw new AccessoriesInvalidOperationException($"Заказ с идентификатором {id} уже удален");
            }

            orderWriteRepository.Delete(targetComponent);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

    }
}
