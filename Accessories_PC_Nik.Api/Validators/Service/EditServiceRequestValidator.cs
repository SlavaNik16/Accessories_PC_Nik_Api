using Accessories_PC_Nik.Api.ModelsRequest.Service;
using FluentValidation;

namespace Accessories_PC_Nik.Api.Validators.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class EditServiceRequestValidator : AbstractValidator<EditServiceRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public EditServiceRequestValidator()
        {

            RuleFor(x => x.Id)
              .NotNull()
              .NotEmpty()
              .WithMessage("Id не должен быть пустым или null");

          
        }

    }
}
