using Api.Enums;
using Application.DTOs.Incoming;
using Application.Interfaces;
using Domain.RequestParameters;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorsService _doctorsService;
        private readonly IValidator<DoctorIncomingDto> _doctorIncomingDtoValidator;

        public DoctorsController(IDoctorsService doctorsService, IValidator<DoctorIncomingDto> doctorIncomingDtoValidator) {
            _doctorsService = doctorsService;
            _doctorIncomingDtoValidator = doctorIncomingDtoValidator;
        }

        [ServiceFilter(typeof(ExtractAccountIdAttribute))]
        [Authorize(Roles = nameof(UserRole.Receptionist))]
        [HttpPost]
        public async Task<IActionResult> CreateDoctorAsync([FromBody] DoctorIncomingDto incomingDto, string? accountId)
        {
            _
            var doctorId = await _doctorsService.CreateDoctorAsync(incomingDto, accountId);
            return Created($"doctor/{doctorId}", doctorId);
        }

        [Authorize(Roles = $"{nameof(UserRole.Patient)},{nameof(UserRole.Doctor)},{nameof(UserRole.Receptionist)}")]
        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetDoctorByIdAsync(Guid doctorId)
        {
            var doctor = await _doctorsService.GetDoctorByIdAsync(doctorId);
            return Ok(doctor);
        }

        [ServiceFilter(typeof(ExtractRoleAttribute))]
        [Authorize(Roles = $"{nameof(UserRole.Patient)},{nameof(UserRole.Receptionist)}")]
        [HttpGet]
        public async Task<IActionResult> GetDoctorsAsync([FromQuery] DoctorParameters parameters, string? roleName)
        {
            if (roleName.Equals(UserRole.Patient))
            {
                var doctor = await _doctorsService.GetDoctorsAtWorkAsync(parameters);
                return Ok(doctor);
            }
            else if(roleName.Equals(UserRole.Receptionist))
            {
                var doctor = await _doctorsService.GetDoctorsAsync(parameters);
                return Ok(doctor);
            }
            return Forbid();
        }

        [Authorize(Roles = $"{nameof(UserRole.Patient)},{nameof(UserRole.Receptionist)}")]
        [HttpPut("doctor/{doctorId}")]
        public async Task<IActionResult> UpdateDoctorAsync(Guid doctorId, [FromBody] DoctorIncomingDto incomingDto)
        {
            await _doctorsService.UpdateDoctorAsync(doctorId, incomingDto);
            return NoContent();
        }

        [Authorize(Roles = $"{nameof(UserRole.Receptionist)}")]
        [HttpDelete("doctor/{doctorId}")]
        public async Task<IActionResult> DeleteDoctorAsync(Guid doctorId)
        {
            await _doctorsService.DeleteDoctorByIdAsync(doctorId);
            return NoContent();
        }

        [Authorize(Roles = $"{nameof(UserRole.Receptionist)}")]
        [HttpPut("doctor/{doctorId}/status")]
        public async Task<IActionResult> ChangeDoctorStatusAsync(Guid doctorId, [FromBody] string statusName)
        {
            await _doctorsService.ChangeDoctorStatusAsync(doctorId, statusName);
            return NoContent();
        }
    }
}
