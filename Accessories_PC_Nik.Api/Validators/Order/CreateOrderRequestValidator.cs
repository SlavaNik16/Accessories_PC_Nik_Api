using FluentValidation;
using Accessories_PC_Nik.Api.ModelsRequest.Person;

namespace Accessories_PC_Nik.Api.Validators.Person
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrderRequestValidator : AbstractValidator<CreateDeliveryRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public CreateOrderRequestValidator()
        {

            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Фамилия не должна быть пустой или null")
                .MaximumLength(80)
                .WithMessage("Фамилия больше 80 символов");

            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Имя не должно быть пустым или null")
                .MaximumLength(80)
                .WithMessage("Имя больше 80 символов");

            RuleFor(x => x.Email)
               .NotNull()
               .NotEmpty()
               .WithMessage("Почта не должна быть пустой или null")
               .EmailAddress()
               .WithMessage("Требуется действительная почта!");

            RuleFor(x => x.Phone)
                .NotNull()
                .NotEmpty()
                .WithMessage("Телефон не должна быть пустой или null");
        }
    }
}
