using Domain.Entities;
using Domain.RequestParameters;

namespace Domain.Interfaces
{
    public interface IPatientsRepository
    {
        Task CreatePatientAsync(Patient patient);
        Task<Patient> GetPatientByIdAsync(Guid patientId, bool trackChanges);
        Task<IEnumerable<Patient>> GetPatientsAsync(PatientParameters parameters);
        Task<int> GetPatientsCountAsync(PatientParameters parameters);
        Task<Patient> GetPatientByAccountIdAsync(string accountId, bool trackChanges);
        void UpdatePatient(Patient patient);
        void DeletePatient(Patient patient);
    }
}
