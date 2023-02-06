using Application.DTOs.Incoming;
using Application.DTOs.Outgoing;

namespace Application.Interfaces
{
    public interface IPatientsService
    {
        public Task<Guid> CreatePatientAsync(PatientIncomingDto incomingDto);
        public Task<PatientOutgoingDto> GetPatientByIdAsync(Guid patientId);
        public Task<IEnumerable<PatientOutgoingDto>> GetPatientsAsync();
        public Task UpdatePatientAsync(Guid patientId, PatientIncomingDto incomingDto);
        public Task DeletePatientByIdAsync(Guid patientId);
    }
}
