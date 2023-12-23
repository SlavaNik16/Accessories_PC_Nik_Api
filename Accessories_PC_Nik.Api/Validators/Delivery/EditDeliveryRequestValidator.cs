using FluentValidation;
using Accessories_PC_Nik.Api.ModelsRequest.Group;
using Accessories_PC_Nik.Repositories.Contracts;

namespace Accessories_PC_Nik.Api.Validators.Group
{
    /// <summary>
    /// 
    /// </summary>
    public class EditDeliveryRequestValidator : AbstractValidator<EditComponentRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public EditDeliveryRequestValidator(IEmployeeReadRepository employeeReadRepository)
        {
            RuleFor(x => x.Id)
              .NotNull()
              .NotEmpty()
              .WithMessage("Id не должен быть пустым или null");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Имя не должно быть пустым или null");

            RuleFor(x => x.ClassroomTeacher)
                .NotNull()
                .NotEmpty()
                .WithMessage("Клаасный руководитель не должен быть пустым или null")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var employeeExists = await employeeReadRepository.AnyByIdAsync(id!.Value, CancellationToken);
                    return employeeExists;
                })
                .WithMessage("Такого работника не существует!")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var employeeExistsWithTeacher = await employeeReadRepository.AnyByIdWithTeacherAsync(id!.Value, CancellationToken);
                    return employeeExistsWithTeacher;
                })
                 .WithMessage("Работник не соответствует категории учителя!");
        }
    }
}
