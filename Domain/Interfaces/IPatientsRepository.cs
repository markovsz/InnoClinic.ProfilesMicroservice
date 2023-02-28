using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPatientsRepository
    {
        Task CreatePatientAsync(Patient patient);
        Task<Patient> GetPatientByIdAsync(Guid patientId, bool trackChanges);
        Task<Patient> GetPatientByAccountIdAsync(string accountId, bool trackChanges);
        Task<IEnumerable<Patient>> GetPatientsAsync();
        void UpdatePatient(Patient patient);
        void DeletePatient(Patient patient);
    }
}
