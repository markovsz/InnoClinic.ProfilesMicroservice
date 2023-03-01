using Application.DTOs.Incoming;
using FluentValidation;

namespace Infrastructure.Validators
{
    public class ReceptionistIncomingDtoValidator : AbstractValidator<ReceptionistIncomingDto>
    {
        public ReceptionistIncomingDtoValidator() 
        { 
        }
    }
}
