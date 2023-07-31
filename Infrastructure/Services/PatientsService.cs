﻿using Application.Interfaces;
using AutoMapper;
using Domain;
using Domain.Entities;
using Domain.Exceptions;
using Domain.RequestParameters;
using InnoClinic.SharedModels.DTOs.Profiles.Incoming;
using InnoClinic.SharedModels.DTOs.Profiles.Outgoing;

namespace Infrastructure.Services
{
    public class PatientsService : IPatientsService
    {
        private IRepositoryManager _repositoryManager;
        private IMapper _mapper;

        public PatientsService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<Guid> CreatePatientAsync(PatientIncomingDto incomingDto, string accountId)
        {
            var patientForCheck = await _repositoryManager.Patients.GetPatientByAccountIdAsync(accountId, false);
            if (patientForCheck is not null)
                throw new EntityAlreadyExistsException();

            var patient = _mapper.Map<Patient>(incomingDto);
            await _repositoryManager.Patients.CreatePatientAsync(patient);
            await _repositoryManager.SaveChangesAsync();
            return patient.Id;
        }

        public async Task DeletePatientByIdAsync(Guid patientId)
        {
            var patient = await _repositoryManager.Patients.GetPatientByIdAsync(patientId, false);
            if (patient is null)
                throw new EntityNotFoundException();
            _repositoryManager.Patients.DeletePatient(patient);
            await _repositoryManager.SaveChangesAsync();
        }

        public async Task<PatientOutgoingDto> GetPatientByIdAsync(Guid patientId)
        {
            var patient = await _repositoryManager.Patients.GetPatientByIdAsync(patientId, false);
            if (patient is null)
                throw new EntityNotFoundException();
            var outgoingPatient = _mapper.Map<PatientOutgoingDto>(patient);
            return outgoingPatient;
        }

        public async Task<PatientOutgoingDto> GetPatientProfileAsync(string accountId)
        {
            var patient = await _repositoryManager.Patients.GetPatientByAccountIdAsync(accountId, false);
            if (patient is null)
                throw new EntityNotFoundException();
            var outgoingPatient = _mapper.Map<PatientOutgoingDto>(patient);
            return outgoingPatient;
        }

        public async Task<PatientsPaginationOutgoingDto> GetPatientsAsync(PatientParameters parameters)
        {
            var patients = await _repositoryManager.Patients.GetPatientsAsync(parameters);
            var outgoingPatients = _mapper.Map<IEnumerable<PatientOutgoingDto>>(patients);
            var patientsCount = await _repositoryManager.Patients.GetPatientsCountAsync(parameters);
            var paginationResult = new PatientsPaginationOutgoingDto
            {
                Entities = outgoingPatients,
                PagesCount = (patientsCount + parameters.Size - 1) / parameters.Size
            };
            return paginationResult;
        }

        public async Task<IEnumerable<PatientOutgoingDto>> GetPatientsByIdsAsync(IEnumerable<Guid> ids)
        {
            var patients = new List<PatientOutgoingDto>();
            foreach(var id in ids)
            {
                var patient = await _repositoryManager.Patients.GetPatientByIdAsync(id, false);
                var mappedPatient = _mapper.Map<PatientOutgoingDto>(patient);
                patients.Add(mappedPatient);
            }
            return patients;
        }

        public async Task UpdatePatientAsync(Guid patientId, UpdatePatientIncomingDto incomingDto)
        {
            var patientForCheck = await _repositoryManager.Patients.GetPatientByIdAsync(patientId, false);
            if (patientForCheck is null)
                throw new EntityNotFoundException();

            var patient = _mapper.Map<Patient>(incomingDto);
            patient.Id = patientId;
            patient.AccountId = patientForCheck.AccountId;
            _repositoryManager.Patients.UpdatePatient(patient);
            await _repositoryManager.SaveChangesAsync();
        }

        public async Task UpdatePatientProfileAsync(string accountId, UpdatePatientIncomingDto incomingDto)
        {
            var patientForCheck = await _repositoryManager.Patients.GetPatientByAccountIdAsync(accountId, false);
            if (patientForCheck is null)
                throw new EntityNotFoundException();

            var patient = _mapper.Map<Patient>(incomingDto);
            patient.Id = patientForCheck.Id;
            patient.AccountId = patientForCheck.AccountId;
            _repositoryManager.Patients.UpdatePatient(patient);
            await _repositoryManager.SaveChangesAsync();
        }
    }
}
