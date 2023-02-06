using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPatientsRepository
    {
        Task CreatePatientAsync(Patient patient);
        Task<Patient> GetPatientByIdAsync(Guid patientId);
        Task<IEnumerable<Patient>> GetPatientsAsync();
        void UpdatePatient(Patient patient);
        void DeletePatient(Patient patient);
    }
}
