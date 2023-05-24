using Domain.Enums;
using FluentValidation;
using InnoClinic.SharedModels.DTOs.Profiles.Incoming;

namespace Infrastructure.Validators;

public class UpdatePatientIncomingDtoValidator : AbstractValidator<UpdatePatientIncomingDto>
{
    public UpdatePatientIncomingDtoValidator()
    {
        RuleFor(e => e.FirstName).NotNull().NotEmpty();
        RuleFor(e => e.LastName).NotNull().NotEmpty();
        RuleFor(e => e.MiddleName).NotNull().NotEmpty();
        RuleFor(e => e.PhoneNumber).NotNull().NotEmpty();
        RuleFor(e => e.BirthDate)
            .GreaterThanOrEqualTo(DateTime.MinValue)
            .LessThanOrEqualTo(DateTime.Now.AddYears(-18));
    }
}
