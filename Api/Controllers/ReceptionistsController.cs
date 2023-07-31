using Api.Enums;
using Api.Extensions;
using Api.FilterAttributes;
using Application.Interfaces;
using FluentValidation;
using Infrastructure.Services;
using InnoClinic.SharedModels.DTOs.Profiles.Incoming;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceptionistsController : ControllerBase
    {
        private readonly IReceptionistsService _receptionistsService;
        private readonly IValidator<ReceptionistIncomingDto> _receptionistIncomingDtoValidator;
        private readonly IValidator<UpdateReceptionistIncomingDto> _updateReceptionistIncomingDtoValidator;

        public ReceptionistsController(IReceptionistsService receptionistsService, IValidator<ReceptionistIncomingDto> receptionistIncomingDtoValidator, IValidator<UpdateReceptionistIncomingDto> updateReceptionistIncomingDtoValidator)
        {
            _receptionistsService = receptionistsService;
            _receptionistIncomingDtoValidator = receptionistIncomingDtoValidator;
            _updateReceptionistIncomingDtoValidator = updateReceptionistIncomingDtoValidator;
        }

        [Authorize(Roles = nameof(UserRole.Receptionist))]
        [HttpPost]
        public async Task<IActionResult> CreateReceptionistAsync([FromBody] ReceptionistIncomingDto incomingDto)
        {
            var result = _receptionistIncomingDtoValidator.Validate(incomingDto);
            result.HandleValidationResult();
            var receptionistId = await _receptionistsService.CreateReceptionistAsync(incomingDto);
            return Created($"receptionist/{receptionistId}", receptionistId);
        }

        [Authorize(Roles = $"{nameof(UserRole.Receptionist)}")]
        [HttpGet("receptionist/{id}")]
        public async Task<IActionResult> GetReceptionistByIdAsync(Guid id)
        {
            var receptionist = await _receptionistsService.GetReceptionistByIdAsync(id);
            return Ok(receptionist);
        }

        [ServiceFilter(typeof(ExtractAccountIdAttribute))]
        [Authorize(Roles = $"{nameof(UserRole.Receptionist)}")]
        [HttpGet("profile")]
        public async Task<IActionResult> GetReceptionistProfileAsync(string? accountId)
        {
            var receptionist = await _receptionistsService.GetReceptionistProfileAsync(accountId);
            return Ok(receptionist);
        }

        [Authorize(Roles = $"{nameof(UserRole.Receptionist)}")]
        [HttpGet("list")]
        public async Task<IActionResult> GetReceptionistsAsync()
        {
            var receptionist = await _receptionistsService.GetReceptionistsAsync();
            return Ok(receptionist);
        }

        [Authorize(Roles = $"{nameof(UserRole.Receptionist)}")]
        [HttpPut("receptionist/{receptionistId}")]
        public async Task<IActionResult> UpdateReceptionistAsync(Guid receptionistId, [FromBody] UpdateReceptionistIncomingDto incomingDto)
        {
            var result = _updateReceptionistIncomingDtoValidator.Validate(incomingDto);
            result.HandleValidationResult();
            await _receptionistsService.UpdateReceptionistAsync(receptionistId, incomingDto);
            return NoContent();
        }

        [Authorize(Roles = $"{nameof(UserRole.Receptionist)}")]
        [HttpDelete("receptionist/{receptionistId}")]
        public async Task<IActionResult> DeleteReceptionistAsync(Guid receptionistId)
        {
            await _receptionistsService.DeleteReceptionistByIdAsync(receptionistId);
            return NoContent();
        }
    }
}
