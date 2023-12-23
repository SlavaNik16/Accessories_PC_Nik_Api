using FluentValidation;
using Accessories_PC_Nik.Api.ModelsRequest.Discipline;

namespace Accessories_PC_Nik.Api.Validators.Discipline
{
    /// <summary>
    /// 
    /// </summary>
    public class EditAccessKeyRequestValidator : AbstractValidator<DisciplineRequest>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public EditAccessKeyRequestValidator()
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
