using Accessories_PC_Nik.Api.ModelsRequest.Component;
using FluentValidation;

namespace Accessories_PC_Nik.Api.Validators.Component
{
    /// <summary>
    /// Валидатор класса <see cref="CreateComponentRequest"/>
    /// </summary>
    public class CreateComponentRequestValidator : AbstractValidator<CreateComponentRequest>
    {
        /// <summary>
        /// Инициализирую <see cref="CreateComponentRequestValidator"/>
        /// </summary>
        public CreateComponentRequestValidator()
        {

            RuleFor(x => x.TypeComponents)
                .NotNull()
                .WithMessage("Тип компонента не должен быть null");

            RuleFor(x => x.Description)
                .MaximumLength(300)
                .WithMessage("Слишком больше описание. Оно должно быть не более 300 символов!");

            RuleFor(x => x.MaterialType)
                .NotNull()
                .WithMessage("Тип материала не должен быть null");

            RuleFor(x => x.Price)
                .NotNull()
                .WithMessage("Стоимость не должен быть null")
                .Must(x => x >= 0)
                .WithMessage("Стоимость не может быть отрицательной");

            RuleFor(x => x.Count)
                .NotNull()
                .WithMessage("Кол-во не должно быть null")
                .Must(x => x > 0)
                .WithMessage("Кол-во не может быть отрицательным или 0 !");
        }
    }
}
