using Accessories_PC_Nik.Api.ModelsRequest.AccessKey;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using FluentValidation;

namespace Accessories_PC_Nik.Api.Validators.AccessKey
{
    /// <summary>
    /// Валидатор класса <see cref="CreateAccessKeyRequest"/>
    /// </summary>
    public class CreateAccessKeyRequestValidator : AbstractValidator<CreateAccessKeyRequest>
    {
        /// <summary>
        /// Инициализирую <see cref="CreateAccessKeyRequestValidator"/>
        /// </summary>
        public CreateAccessKeyRequestValidator(IWorkersReadRepository workersReadRepository)
        {

            RuleFor(x => x.Types)
                .NotNull()
                .WithMessage("Уровень доступа не должен быть null");

            RuleFor(x => x.WorkerId)
                .NotNull()
                .WithMessage("Уровень доступа не должен быть null")
                 .MustAsync(async (id, CancellationToken) =>
                 {
                     var workerExists = await workersReadRepository.AnyByIdAsync(id, CancellationToken);
                     return workerExists;
                 })
                .WithMessage("Такого работника не существует!");
        }
    }
}
