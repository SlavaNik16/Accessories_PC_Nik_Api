using Accessories_PC_Nik.Api.ModelsRequest.Service;
using FluentValidation;

namespace Accessories_PC_Nik.Api.Validators.Service
{
    /// <summary>
    /// Валидатор класса <see cref="EditServiceRequest"/>
    /// </summary>
    public class EditServiceRequestValidator : AbstractValidator<EditServiceRequest>
    {
        /// <summary>
        /// Инициализирую <see cref="EditServiceRequestValidator"/>
        /// </summary>
        public EditServiceRequestValidator()
        {

            RuleFor(x => x.Id)
              .NotNull()
              .NotEmpty()
              .WithMessage("Id не должен быть пустым или null");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Имя не должно быть пустым или null")
                .MaximumLength(200)
                .WithMessage("Слишком больше имя. Оно должно быть не более 200 символов!");

            RuleFor(x => x.Description)
                .MaximumLength(300)
                .WithMessage("Слишком больше описание. Оно должно быть не более 300 символов!");

            RuleFor(x => x.Duration)
                .NotNull()
                .NotNull()
                .WithMessage("Продолжительность не должно быть пустым или null");

            RuleFor(x => x.Price)
                .NotNull()
                .WithMessage("Стоимость не должен быть null")
                .Must(x => x >= 0)
                .WithMessage("Стоимость не может быть отрицательной");


        }

    }
}
