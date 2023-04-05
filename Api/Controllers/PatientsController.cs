using Application.DTOs.Incoming;
using Application.DTOs.Outgoing;
using Application.Interfaces;
using Domain.RequestParameters;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsService _patientsService;
        private readonly IValidator<PatientIncomingDto> _validator;

        public PatientsController(IPatientsService patientsService, IValidator<PatientIncomingDto> validator)
        {
            _patientsService = patientsService;
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatientAsync([FromBody] PatientIncomingDto incomingDto)
        {
            var patientId = await _patientsService.CreatePatientAsync(incomingDto);
            return Created($"patient/{patientId}", patientId);
        }

        [HttpPost("ids")]
        public async Task<IActionResult> GetPatientsByIdsAsync([FromBody] IEnumerable<Guid> ids)
        {
            var patients = await _patientsService.GetPatientsByIdsAsync(ids);
            return Ok(patients);
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetPatientByIdAsync(Guid patientId)
        {
            var patient = await _patientsService.GetPatientByIdAsync(patientId);
            return Ok(patient);
        }

        [HttpGet]
        public async Task<IActionResult> GetPatientsAsync([FromQuery] PatientParameters parameters)
        {
            var patient = await _patientsService.GetPatientsAsync(parameters);
            return Ok(patient);
        }

        [HttpPut("patient/{patientId}")]
        public async Task<IActionResult> UpdatePatientAsync(Guid patientId, [FromBody] PatientIncomingDto incomingDto)
        {
            await _patientsService.UpdatePatientAsync(patientId, incomingDto);
            return NoContent();
        }

        [HttpDelete("patient/{patientId}")]
        public async Task<IActionResult> DeletePatientAsync(Guid patientId)
        {
            await _patientsService.DeletePatientByIdAsync(patientId);
            return NoContent();
        }
    }
}
