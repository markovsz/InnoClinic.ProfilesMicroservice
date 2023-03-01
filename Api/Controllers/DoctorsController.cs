using Application.DTOs.Incoming;
using Application.Interfaces;
using Domain.RequestParameters;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorsService _doctorsService;
        private readonly IValidator<DoctorIncomingDto> _validator;

        public DoctorsController(IDoctorsService doctorsService, IValidator<DoctorIncomingDto> validator) {
            _doctorsService = doctorsService;
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctorAsync([FromBody] DoctorIncomingDto incomingDto)
        {
            var doctorId = await _doctorsService.CreateDoctorAsync(incomingDto);
            return Created($"doctor/{doctorId}", doctorId);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetDoctorByIdAsync(Guid doctorId)
        {
            var doctor = await _doctorsService.GetDoctorByIdAsync(doctorId);
            return Ok(doctor);
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctorsAsync([FromQuery] DoctorParameters parameters)
        {
            var doctor = await _doctorsService.GetDoctorsAsync(parameters);
            return Ok(doctor);
        }

        [HttpPut("doctor/{doctorId}")]
        public async Task<IActionResult> UpdateDoctorAsync(Guid doctorId, [FromBody] DoctorIncomingDto incomingDto)
        {
            await _doctorsService.UpdateDoctorAsync(doctorId, incomingDto);
            return NoContent();
        }

        [HttpDelete("doctor/{doctorId}")]
        public async Task<IActionResult> DeleteDoctorAsync(Guid doctorId)
        {
            await _doctorsService.DeleteDoctorByIdAsync(doctorId);
            return NoContent();
        }

        [HttpPut("doctor/{doctorId}/status")]
        public async Task<IActionResult> ChangeDoctorStatusAsync(Guid doctorId, [FromBody] string statusName)
        {
            await _doctorsService.ChangeDoctorStatusAsync(doctorId, statusName);
            return NoContent();
        }
    }
}
