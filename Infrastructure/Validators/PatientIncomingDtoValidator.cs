﻿using FluentValidation;
using InnoClinic.SharedModels.DTOs.Profiles.Incoming;

namespace Infrastructure.Validators
{
    public class PatientIncomingDtoValidator : AbstractValidator<PatientIncomingDto>
    {
        public PatientIncomingDtoValidator() 
        {
            RuleFor(e => e.FirstName).NotNull().NotEmpty();
            RuleFor(e => e.LastName).NotNull().NotEmpty();
            RuleFor(e => e.MiddleName).NotNull().NotEmpty();
            RuleFor(e => e.BirthDate)
                .GreaterThanOrEqualTo(DateTime.MinValue)
                .LessThanOrEqualTo(DateTime.Now);
        }
    }
}
