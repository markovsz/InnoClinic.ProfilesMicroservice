using FluentValidation;
using InnoClinic.SharedModels.DTOs.Profiles.Incoming;

namespace Infrastructure.Validators
{
    public class ReceptionistIncomingDtoValidator : AbstractValidator<ReceptionistIncomingDto>
    {
        public ReceptionistIncomingDtoValidator() 
        {
            RuleFor(e => e.FirstName).NotNull().NotEmpty();
            RuleFor(e => e.LastName).NotNull().NotEmpty();
            RuleFor(e => e.MiddleName).NotNull().NotEmpty();
        }
    }
}
