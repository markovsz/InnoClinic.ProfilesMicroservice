using Application.DTOs.Incoming;
using FluentValidation;

namespace Infrastructure.Validators
{
    public class PatientIncomingDtoValidator : AbstractValidator<PatientIncomingDto>
    {
        public PatientIncomingDtoValidator() 
        {
            RuleFor(e => e.BirthDate)
                .GreaterThanOrEqualTo(DateTime.MinValue)
                .LessThanOrEqualTo(DateTime.Now);
        }
    }
}
