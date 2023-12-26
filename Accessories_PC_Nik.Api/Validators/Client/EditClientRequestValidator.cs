using Accessories_PC_Nik.Api.ModelsRequest.Client;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using FluentValidation;

namespace Accessories_PC_Nik.Api.Validators.Client
{
    /// <summary>
    /// Валидатор класса <see cref="EditClientRequest"/>
    /// </summary>
    public class EditClientRequestValidator : AbstractValidator<EditClientRequest>
    {
        /// <summary>
        /// Инициализирую <see cref="CreateClientRequestValidator"/>
        /// </summary>
        public EditClientRequestValidator(IClientsReadRepository clientsReadRepository)
        {
            RuleFor(x => x.Id)
               .NotNull()
               .NotEmpty()
               .WithMessage("Id не должен быть пустым или null");

            RuleFor(x => x.Surname)
               .NotNull()
               .NotEmpty()
               .WithMessage("Фамилия не должна быть пустым или null!")
               .MaximumLength(80)
               .WithMessage("Фамилия должна быть не более 80 символов!");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Имя не должно быть пустым или null!")
                .MaximumLength(80)
                .WithMessage("Имя должно быть не более 80 символов!");

            RuleFor(x => x.Patronymic)
                .MaximumLength(80)
                .WithMessage("Отчество должно быть не более 80 символов!");

            RuleFor(x => x.Phone)
                .NotNull()
                .NotEmpty()
                .WithMessage("Телефон не должен быть пустым или null!")
                .MaximumLength(20)
                .WithMessage("Такого телефона не существует!")
                .MustAsync(async (phone, cancellationToken) =>
                {
                    var isPhoneExists = await clientsReadRepository.AnyByPhoneAsync(phone, cancellationToken);
                    return !isPhoneExists;
                })
                .WithMessage("Телефон должен быть уникальным!");

            RuleFor(x => x.Email)
               .NotNull()
               .NotEmpty()
               .WithMessage("Почта не должна быть пустой или null!")
               .EmailAddress()
               .WithMessage("Неправильный формат почты!")
               .MustAsync(async (email, cancellationToken) =>
               {
                   var isEmailExist = await clientsReadRepository.AnyByEmailAsync(email, cancellationToken);
                   return !isEmailExist;
               })
               .WithMessage("Почта должна быть уникальной!");

            RuleFor(x => x.Balance)
                .NotNull()
                .WithMessage("Баланс не должен быть null!");
        }
    }
}
