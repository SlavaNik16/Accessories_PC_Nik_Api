using FluentValidation;
using Accessories_PC_Nik.Api.ModelsRequest.Discipline;

namespace Accessories_PC_Nik.Api.Validators.Discipline
{
    /// <summary>
    /// Валидатор класса <see cref="CreateAccessKeyRequest"/>
    /// </summary>
    public class CreateAccessKeyRequestValidator : AbstractValidator<CreateAccessKeyRequest>
    {
        /// <summary>
        /// Инициализирую <see cref="CreateAccessKeyRequestValidator"/>
        /// </summary>
        public CreateAccessKeyRequestValidator()
        {
            RuleFor(discipline => discipline.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Имя не должно быть пустым или null")
                .MaximumLength(200)
                .WithMessage("Имя дисциплины больше 200 символов");
        }
    }
}
