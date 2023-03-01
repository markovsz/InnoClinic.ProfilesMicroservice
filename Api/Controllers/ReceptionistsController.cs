using Application.DTOs.Incoming;
using Application.Interfaces;
using FluentValidation;
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

        [HttpPost]
        public async Task<IActionResult> CreateReceptionistAsync([FromBody] ReceptionistIncomingDto incomingDto)
        {
            var receptionistId = await _receptionistsService.CreateReceptionistAsync(incomingDto);
            return Created($"receptionist/{receptionistId}", receptionistId);
        }

        [HttpGet("receptionist/{receptionistId}")]
        public async Task<IActionResult> GetReceptionistByIdAsync(Guid receptionistId)
        {
            var receptionist = await _receptionistsService.GetReceptionistByIdAsync(receptionistId);
            return Ok(receptionist);
        }

        [HttpGet]
        public async Task<IActionResult> GetReceptionistsAsync()
        {
            var receptionist = await _receptionistsService.GetReceptionistsAsync();
            return Ok(receptionist);
        }

        [HttpPut("receptionist/{receptionistId}")]
        public async Task<IActionResult> UpdateReceptionistAsync(Guid receptionistId, [FromBody] ReceptionistIncomingDto incomingDto)
        {
            await _receptionistsService.UpdateReceptionistAsync(receptionistId, incomingDto);
            return NoContent();
        }

        [HttpDelete("receptionist/{receptionistId}")]
        public async Task<IActionResult> DeleteReceptionistAsync(Guid receptionistId)
        {
            await _receptionistsService.DeleteReceptionistByIdAsync(receptionistId);
            return NoContent();
        }
    }
}
