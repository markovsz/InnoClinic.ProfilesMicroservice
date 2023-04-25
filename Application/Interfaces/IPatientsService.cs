using Application.DTOs.Incoming;
using Application.DTOs.Outgoing;
using Domain.RequestParameters;

namespace Application.Interfaces
{
    public interface IPatientsService
    {
        public Task<Guid> CreatePatientAsync(PatientIncomingDto incomingDto, string accountId);
        public Task<PatientOutgoingDto> GetPatientByIdAsync(Guid patientId);
        public Task<PatientsPaginationOutgoingDto> GetPatientsAsync(PatientParameters parameters);
        public Task<IEnumerable<PatientOutgoingDto>> GetPatientsByIdsAsync(IEnumerable<Guid> ids);
        public Task UpdatePatientAsync(Guid patientId, PatientIncomingDto incomingDto);
        public Task DeletePatientByIdAsync(Guid patientId);
    }
}
