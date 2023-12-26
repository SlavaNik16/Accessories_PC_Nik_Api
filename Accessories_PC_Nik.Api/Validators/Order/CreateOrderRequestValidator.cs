using Accessories_PC_Nik.Api.ModelsRequest.Order;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using FluentValidation;

namespace Accessories_PC_Nik.Api.Validators.Order
{
    /// <summary>
    /// Валидатор класса <see cref="CreateOrderRequest"/>
    /// </summary>
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        /// <summary>
        /// Инициализирую <see cref="CreateOrderRequestValidator"/>
        /// </summary>
        public CreateOrderRequestValidator(IClientsReadRepository clientsReadRepository,
            IComponentsReadRepository componentsReadRepository,
            IServicesReadRepository servicesReadRepository,
            IDeliveryReadRepository deliveryReadRepository)
        {

            RuleFor(x => x.ClientId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Клиент не должен быть пустым или null")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var clientExists = await clientsReadRepository.AnyByIdAsync(id, CancellationToken);
                    return clientExists;
                })
                .WithMessage("Такого клиента не существует!");

            RuleFor(x => x.ComponentId)
                .MustAsync(async (id, CancellationToken) =>
                {
                    if (id == null) return true;
                    var componentExists = await componentsReadRepository.AnyByIdAsync(id.Value, CancellationToken);
                    return componentExists;
                })
                .WithMessage("Такого компонента не существует!");

            RuleFor(x => x.ServiceId)
                .MustAsync(async (id, CancellationToken) =>
                {
                    if (id == null) return true;
                    var componentExists = await servicesReadRepository.AnyByIdAsync(id.Value, CancellationToken);
                    return componentExists;
                })
                .WithMessage("Такой услуги не существует!");

            RuleFor(x => x.DeliveryId)
               .MustAsync(async (id, CancellationToken) =>
               {
                   if (id == null) return true;
                   var deliveryExist = await deliveryReadRepository.AnyByIdAsync(id.Value, CancellationToken);
                   return deliveryExist;
               })
               .WithMessage("Такой доставки не существует!");

        }
    }
}
