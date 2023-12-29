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
    public class DeliveryService : IDeliveryService, IServiceAnchor
    {
        private readonly IDeliveryReadRepository deliveryReadRepository;
        private readonly IDeliveryWriteRepository deliveryWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public DeliveryService(IDeliveryReadRepository deliveryReadRepository,
            IDeliveryWriteRepository deliveryWriteRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.deliveryReadRepository = deliveryReadRepository;
            this.deliveryWriteRepository = deliveryWriteRepository;
            this.unitOfWork = unitOfWork;
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
            if (item == null)
            {
                throw new AccessoriesEntityNotFoundException<Delivery>(id);
            }

            return mapper.Map<DeliveryModel>(item);
        }
        async Task<DeliveryModel> IDeliveryService.AddAsync(DeliveryRequestModel source, CancellationToken cancellationToken)
        {
            var item = new Delivery
            {
                Id = Guid.NewGuid(),
                From = source.From,
                To = source.To,
                Price = source.Price,
            };
            deliveryWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<DeliveryModel>(item);
        }
        async Task<DeliveryModel> IDeliveryService.EditAsync(DeliveryRequestModel source, CancellationToken cancellationToken)
        {
            var targetDelivery = await deliveryReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetDelivery == null)
            {
                throw new AccessoriesEntityNotFoundException<Delivery>(source.Id);
            }

            targetDelivery.From = source.From;
            targetDelivery.To = source.To;
            targetDelivery.Price = source.Price;

            deliveryWriteRepository.Update(targetDelivery);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<DeliveryModel>(targetDelivery);
        }
        async Task IDeliveryService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetComponent = await deliveryReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetComponent == null)
            {
                throw new AccessoriesEntityNotFoundException<Delivery>(id);
            }

            deliveryWriteRepository.Delete(targetComponent);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
