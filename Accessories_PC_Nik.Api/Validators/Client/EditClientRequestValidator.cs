using Accessories_PC_Nik.Api.ModelsRequest.Document;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using FluentValidation;

namespace Accessories_PC_Nik.Api.Validators.Document
{
    /// <summary>
    /// 
    /// </summary>
    public class EditClientRequestValidator : AbstractValidator<EditClientRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public EditClientRequestValidator(IClientsReadRepository personReadRepository)
        {
            RuleFor(x => x.Id)
               .NotNull()
               .NotEmpty()
               .WithMessage("Id не должен быть пустым или null");

            RuleFor(x => x.Number)
               .NotNull()
               .NotEmpty()
               .WithMessage("Номер не должен быть пустым или null")
               .MaximumLength(8)
               .WithMessage("Номер больше 8 символов");

            RuleFor(x => x.Series)
                .NotNull()
                .NotEmpty()
                .WithMessage("Серия не должна быть пустым или null")
                .MaximumLength(12)
                .WithMessage("Серия больше 12 символов");

            RuleFor(x => x.IssuedAt)
                .NotNull()
                .NotEmpty()
                .WithMessage("Дата выдачи не должна быть пустым или null");

            RuleFor(x => x.DocumentType)
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
