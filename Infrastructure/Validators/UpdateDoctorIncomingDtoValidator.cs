using Domain.Enums;
using FluentValidation;
using InnoClinic.SharedModels.DTOs.Profiles.Incoming;

namespace Infrastructure.Validators;

public class UpdateDoctorIncomingDtoValidator : AbstractValidator<UpdateDoctorIncomingDto>
{
    public UpdateDoctorIncomingDtoValidator()
    {
        RuleFor(e => e.FirstName).NotNull().NotEmpty();
        RuleFor(e => e.LastName).NotNull().NotEmpty();
        RuleFor(e => e.MiddleName).NotNull().NotEmpty();
        RuleFor(e => e.Status).IsEnumName(typeof(DoctorStatuses));
        RuleFor(e => e.CareerStartYear)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(DateTime.Now.Year);
        RuleFor(e => e.BirthDate)
            .GreaterThanOrEqualTo(DateTime.MinValue)
            .LessThanOrEqualTo(DateTime.Now.AddYears(-18));
    }
}
