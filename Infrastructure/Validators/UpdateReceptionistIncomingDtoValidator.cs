using Domain.Enums;
using FluentValidation;
using InnoClinic.SharedModels.DTOs.Profiles.Incoming;

namespace Infrastructure.Validators;

public class UpdateReceptionistIncomingDtoValidator : AbstractValidator<UpdateReceptionistIncomingDto>
{
    public UpdateReceptionistIncomingDtoValidator()
    {
        RuleFor(e => e.FirstName).NotNull().NotEmpty();
        RuleFor(e => e.LastName).NotNull().NotEmpty();
        RuleFor(e => e.MiddleName).NotNull().NotEmpty();
        RuleFor(e => e.OfficeId).NotNull().NotEmpty();
    }
}
