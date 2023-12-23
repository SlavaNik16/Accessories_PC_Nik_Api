using FluentValidation;
using Accessories_PC_Nik.Api.ModelsRequest.Employee;
using Accessories_PC_Nik.Repositories.Contracts;

namespace Accessories_PC_Nik.Api.Validators.Employee
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateComponentRequestValidator : AbstractValidator<CreateWorkerRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public CreateComponentRequestValidator(IPersonReadRepository personReadRepository)
        {

            RuleFor(x => x.EmployeeType)
                .NotNull()
                .WithMessage("Тип документа не должен быть null");

            RuleFor(x => x.PersonId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Персона не должна быть пустым или null")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var personExists = await personReadRepository.AnyByIdAsync(id, CancellationToken);
                    return personExists;
                })
                .WithMessage("Такой персоны не существует!");
        }
    }
}
