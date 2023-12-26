using Accessories_PC_Nik.Api.ModelsRequest.AccessKey;
using FluentValidation;

namespace Accessories_PC_Nik.Api.Validators.AccessKey
{
    /// <summary>
    ///  Валидатор класса <see cref="EditAccessKeyRequest"/>
    /// </summary>
    public class EditAccessKeyRequestValidator : AbstractValidator<EditAccessKeyRequest>
    {
        /// <summary>
        /// Инициализирую <see cref="EditAccessKeyRequestValidator"/>
        /// </summary>
        public EditAccessKeyRequestValidator()
        {
            RuleFor(x => x.Id)
               .NotNull()
               .NotEmpty()
               .WithMessage("Идентификатор не должен быть пустым или null");

            RuleFor(x => x.Types)
                .NotNull()
                .WithMessage("Уровень доступа не должен быть null");
        }
    }
}
