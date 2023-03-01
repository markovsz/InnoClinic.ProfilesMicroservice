using Application.DTOs.Incoming;
using Application.DTOs.Outgoing;
using Domain.RequestParameters;

namespace Application.Interfaces
{
    public interface IPatientsService
    {
        public Task<Guid> CreatePatientAsync(PatientIncomingDto incomingDto);
        public Task<PatientOutgoingDto> GetPatientByIdAsync(Guid patientId);
        public Task<PatientsPaginationOutgoingDto> GetPatientsAsync(PatientParameters parameters);
        public Task UpdatePatientAsync(Guid patientId, PatientIncomingDto incomingDto);
        public Task DeletePatientByIdAsync(Guid patientId);
    }
}
