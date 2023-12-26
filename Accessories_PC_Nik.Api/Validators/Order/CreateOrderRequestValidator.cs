using Accessories_PC_Nik.Api.ModelsRequest.Order;
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
        public CreateOrderRequestValidator()
        {

            
        }
    }
}
