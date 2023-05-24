﻿using Api.Enums;
using Api.Extensions;
using Api.FilterAttributes;
using Application.Interfaces;
using Domain.RequestParameters;
using FluentValidation;
using Infrastructure.Services;
using InnoClinic.SharedModels.DTOs.Profiles.Incoming;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsService _patientsService;
        private readonly IValidator<PatientIncomingDto> _patientIncomingDtoValidator;
        private readonly IValidator<UpdatePatientIncomingDto> _updatePatientIncomingDtoValidator;

        public PatientsController(IPatientsService patientsService, IValidator<PatientIncomingDto> patientIncomingDtoValidator, IValidator<UpdatePatientIncomingDto> updatePatientIncomingDtoValidator)
        {
            _patientsService = patientsService;
            _patientIncomingDtoValidator = patientIncomingDtoValidator;
            _updatePatientIncomingDtoValidator = updatePatientIncomingDtoValidator;
        }

        [ServiceFilter(typeof(ExtractAccountIdAttribute))]
        [Authorize(Roles = $"{nameof(UserRole.Patient)},{nameof(UserRole.Receptionist)}")]
        [HttpPost]
        public async Task<IActionResult> CreatePatientAsync([FromBody] PatientIncomingDto incomingDto, string? accountId)
        {
            var result = _patientIncomingDtoValidator.Validate(incomingDto);
            result.HandleValidationResult();
            var patientId = await _patientsService.CreatePatientAsync(incomingDto, accountId);
            return Created($"patient/{patientId}", patientId);
        }

        [Authorize(Roles = $"{nameof(UserRole.Patient)},{nameof(UserRole.Doctor)},{nameof(UserRole.Receptionist)}")]
        [HttpPost("ids")]
        public async Task<IActionResult> GetPatientsByIdsAsync([FromBody] IEnumerable<Guid> ids)
        {
            var patients = await _patientsService.GetPatientsByIdsAsync(ids);
            return Ok(patients);
        }


        [Authorize(Roles = $"{nameof(UserRole.Patient)},{nameof(UserRole.Doctor)},{nameof(UserRole.Receptionist)}")]
        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetPatientByIdAsync(Guid patientId)
        {
            var patient = await _patientsService.GetPatientByIdAsync(patientId);
            return Ok(patient);
        }

        [ServiceFilter(typeof(ExtractAccountIdAttribute))]
        [Authorize(Roles = $"{nameof(UserRole.Patient)}")]
        [HttpGet("profile")]
        public async Task<IActionResult> GetPatientProfileAsync(string accountId)
        {
            var doctor = await _patientsService.GetPatientProfileAsync(accountId);
            return Ok(doctor);
        }

        [Authorize(Roles = $"{nameof(UserRole.Receptionist)}")]
        [HttpGet("list")]
        public async Task<IActionResult> GetPatientsAsync([FromQuery] PatientParameters parameters)
        {
            var patient = await _patientsService.GetPatientsAsync(parameters);
            return Ok(patient);
        }

        [Authorize(Roles = $"{nameof(UserRole.Patient)},{nameof(UserRole.Receptionist)}")]
        [HttpPut("patient/{patientId}")]
        public async Task<IActionResult> UpdatePatientAsync(Guid patientId, [FromBody] UpdatePatientIncomingDto incomingDto)
        {
            var result = _updatePatientIncomingDtoValidator.Validate(incomingDto);
            result.HandleValidationResult();
            await _patientsService.UpdatePatientAsync(patientId, incomingDto);
            return NoContent();
        }

        [Authorize(Roles = $"{nameof(UserRole.Receptionist)}")]
        [HttpDelete("patient/{patientId}")]
        public async Task<IActionResult> DeletePatientAsync(Guid patientId)
        {
            await _patientsService.DeletePatientByIdAsync(patientId);
            return NoContent();
        }
    }
}
