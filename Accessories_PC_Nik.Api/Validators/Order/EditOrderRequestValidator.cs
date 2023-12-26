using Accessories_PC_Nik.Api.ModelsRequest.Order;
using FluentValidation;

namespace Accessories_PC_Nik.Api.Validators.Order
{
    /// <summary>
    /// Валидатор класса <see cref="EditOrderRequest"/>
    /// </summary>
    public class EditOrderRequestValidator : AbstractValidator<EditOrderRequest>
    {
        /// <summary>
        /// Инициализирую <see cref="EditOrderRequestValidator"/>
        /// </summary>
        public EditOrderRequestValidator()
        {
            RuleFor(x => x.Id)
               .NotNull()
               .NotEmpty()
               .WithMessage("Id не должен быть пустым или null");

         
        }
    }
}
