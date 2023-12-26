using Accessories_PC_Nik.Api.ModelsRequest.Service;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using FluentValidation;

namespace Accessories_PC_Nik.Api.Validators.Service
{
    /// <summary>
    /// Валидатор класса <see cref="CreateServiceRequest"/>
    /// </summary>
    public class CreateServiceRequestValidator : AbstractValidator<CreateServiceRequest>
    {
        /// <summary>
        /// Инициализирую <see cref="CreateServiceRequestValidator"/>
        /// </summary>
        public CreateServiceRequestValidator(IServicesReadRepository servicesReadRepository)
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Имя не должно быть пустым или null")
                .MaximumLength(200)
                .WithMessage("Слишком больше имя. Оно должно быть не более 200 символов!")
                .MustAsync(async (name, cancellationToken) =>
                 {
                     var isNameExists = await servicesReadRepository.AnyByNameAsync(name, cancellationToken);
                     return !isNameExists;
                 })
                .WithMessage("Имя должно быть уникальным!");


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
