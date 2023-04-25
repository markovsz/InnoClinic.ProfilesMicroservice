using Api.Enums;
using Api.FilterAttributes;
using Application.DTOs.Incoming;
using Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceptionistsController : ControllerBase
    {
        private readonly IReceptionistsService _receptionistsService;
        private readonly IValidator<ReceptionistIncomingDto> _validator;

        public ReceptionistsController(IReceptionistsService receptionistsService, IValidator<ReceptionistIncomingDto> validator)
        {
            _receptionistsService = receptionistsService;
            _validator = validator;
        }

        [ServiceFilter(typeof(ExtractAccountIdAttribute))]
        [Authorize(Roles = nameof(UserRole.Receptionist))]
        [HttpPost]
        public async Task<IActionResult> CreateReceptionistAsync([FromBody] ReceptionistIncomingDto incomingDto, string? accountId)
        {
            var receptionistId = await _receptionistsService.CreateReceptionistAsync(incomingDto, accountId);
            return Created($"receptionist/{receptionistId}", receptionistId);
        }

        [Authorize(Roles = $"{nameof(UserRole.Receptionist)}")]
        [HttpGet("receptionist/{receptionistId}")]
        public async Task<IActionResult> GetReceptionistByIdAsync(Guid receptionistId)
        {
            var receptionist = await _receptionistsService.GetReceptionistByIdAsync(receptionistId);
            return Ok(receptionist);
        }

        [Authorize(Roles = $"{nameof(UserRole.Receptionist)}")]
        [HttpGet]
        public async Task<IActionResult> GetReceptionistsAsync()
        {
            var receptionist = await _receptionistsService.GetReceptionistsAsync();
            return Ok(receptionist);
        }

        [Authorize(Roles = $"{nameof(UserRole.Receptionist)}")]
        [HttpPut("receptionist/{receptionistId}")]
        public async Task<IActionResult> UpdateReceptionistAsync(Guid receptionistId, [FromBody] ReceptionistIncomingDto incomingDto)
        {
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
