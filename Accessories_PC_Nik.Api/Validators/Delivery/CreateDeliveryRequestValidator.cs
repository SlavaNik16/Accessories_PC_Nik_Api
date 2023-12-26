using Accessories_PC_Nik.Api.ModelsRequest.Delivery;
using FluentValidation;

namespace Accessories_PC_Nik.Api.Validators.Delivery
{
    /// <summary>
    /// Валидатор класса <see cref="CreateDeliveryRequest"/>
    /// </summary>
    public class CreateDeliveryRequestValidator : AbstractValidator<CreateDeliveryRequest>
    {
        /// <summary>
        /// Инициализирую <see cref="CreateDeliveryRequestValidator"/>
        /// </summary>
        public CreateDeliveryRequestValidator()
        {

            RuleFor(x => x.From)
                .NotNull()
                .NotEmpty()
                .WithMessage("Откуда не должно быть пустым или null");

            RuleFor(x => x.To)
                .NotNull()
                .NotEmpty()
                .WithMessage("Куда не должно быть пустым или null");

            RuleFor(x => x.Price)
               .NotNull()
               .WithMessage("Стоимость не должен быть null")
               .Must(x => x >= 0)
               .WithMessage("Стоимость не может быть отрицательной");

        }
    }
}
