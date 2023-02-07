using Application.DTOs.Incoming;
using Application.DTOs.Outgoing;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Domain.Entities;

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

        public async Task<Guid> CreatePatientAsync(PatientIncomingDto incomingDto)
        {
            var patient = _mapper.Map<Patient>(incomingDto);
            await _repositoryManager.Patients.CreatePatientAsync(patient);
            await _repositoryManager.SaveChangesAsync();
            return patient.Id;
        }

        public async Task DeletePatientByIdAsync(Guid patientId)
        {
            var patient = await _repositoryManager.Patients.GetPatientByIdAsync(patientId, false);
            _repositoryManager.Patients.DeletePatient(patient);
            await _repositoryManager.SaveChangesAsync();
        }

        public async Task<PatientOutgoingDto> GetPatientByIdAsync(Guid patientId)
        {
            var patient = await _repositoryManager.Patients.GetPatientByIdAsync(patientId, false);
            var outgoingPatient = _mapper.Map<PatientOutgoingDto>(patient);
            return outgoingPatient;
        }

        public async Task<IEnumerable<PatientOutgoingDto>> GetPatientsAsync()
        {
            var patients = await _repositoryManager.Patients.GetPatientsAsync();
            var outgoingPatients = _mapper.Map<IEnumerable<PatientOutgoingDto>>(patients);
            return outgoingPatients;
        }

        public async Task UpdatePatientAsync(Guid PatientId, PatientIncomingDto incomingDto)
        {
            var patient = _mapper.Map<Patient>(incomingDto);
            patient.Id = PatientId;
            _repositoryManager.Patients.UpdatePatient(patient);
            await _repositoryManager.SaveChangesAsync();
        }
    }
}
