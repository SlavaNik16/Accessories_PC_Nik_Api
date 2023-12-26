using Accessories_PC_Nik.Api.ModelsRequest.Delivery;
using FluentValidation;

namespace Accessories_PC_Nik.Api.Validators.Delivery
{
    /// <summary>
    /// Валидатор класса <see cref="EditDeliveryRequest"/>
    /// </summary>
    public class EditDeliveryRequestValidator : AbstractValidator<EditDeliveryRequest>
    {
        /// <summary>
        /// Инициализирую <see cref="EditDeliveryRequestValidator"/>
        /// </summary>
        public EditDeliveryRequestValidator()
        {
            RuleFor(x => x.Id)
              .NotNull()
              .NotEmpty()
              .WithMessage("Id не должен быть пустым или null");

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
