using Accessories_PC_Nik.Api.ModelsRequest.Worker;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using FluentValidation;

namespace Accessories_PC_Nik.Api.Validators.Worker
{

    /// <summary>
    /// Валидатор класса <see cref="EditWorkerRequest"/>
    /// </summary>
    public class EditWorkerRequestValidator : AbstractValidator<EditWorkerRequest>
    {
        /// <summary>
        /// Инициализирую <see cref="EditWorkerRequestValidator"/>
        /// </summary>
        public EditWorkerRequestValidator(IClientsReadRepository clientsReadRepository,
            IWorkersReadRepository workersReadRepository)
        {

            RuleFor(x => x.Id)
              .NotNull()
              .NotEmpty()
              .WithMessage("Id не должен быть пустым или null");

            RuleFor(x => x.Number)
               .NotNull()
               .NotEmpty()
               .WithMessage("Номер не должен быть пустым или null")
               .MaximumLength(10)
               .WithMessage("Номер не может быть больше 10 символов")
               .MustAsync(async (number, CancellationToken) =>
               {
                   var workerExists = await workersReadRepository.AnyByNumberAsync(number, CancellationToken);
                   return workerExists;
               })
                .WithMessage("Номер должен быть уникальным!");

            RuleFor(x => x.Series)
                .NotNull()
                .NotEmpty()
                .WithMessage("Серия не должна быть пустым или null")
                .MaximumLength(12)
                .WithMessage("Серия не может быть больше 12 символов");

            RuleFor(x => x.IssuedBy)
                .NotNull()
                .NotEmpty()
                .WithMessage("Кем выдан не должно быть пустым или null")
                .MaximumLength(300)
                .WithMessage("Кем выдан не может быть больше 300 символов");

            RuleFor(x => x.DocumentType)
                .NotNull()
                .WithMessage("Тип документ не должен быть null");

            RuleFor(x => x.AccessLevel)
               .NotNull()
               .WithMessage("Уровень привилегий не должен быть null");

            RuleFor(x => x.ClientId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Клиент не должен быть пустым или null")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var clientExists = await clientsReadRepository.AnyByIdAsync(id, CancellationToken);
                    return clientExists;
                })
                .WithMessage("Такого клиента не существует!");
        }
    }


}
