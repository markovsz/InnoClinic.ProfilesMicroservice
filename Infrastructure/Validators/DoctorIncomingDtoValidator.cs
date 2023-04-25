using Application.DTOs.Incoming;
using Domain.Enums;
using FluentValidation;

namespace Infrastructure.Validators
{
    public class DoctorIncomingDtoValidator : AbstractValidator<DoctorIncomingDto>
    {
        public DoctorIncomingDtoValidator() 
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
}
