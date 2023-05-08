using Domain.Entities;
using Domain.RequestParameters;

namespace Domain.Interfaces
{
    public interface IDoctorsRepository
    {
        Task CreateDoctorAsync(Doctor doctor);
        Task<Doctor> GetDoctorByIdAsync(Guid doctorId, bool trackChanges);
        Task<IEnumerable<Doctor>> GetDoctorsAtWorkAsync(DoctorParameters parameters);
        Task<IEnumerable<Doctor>> GetDoctorsAsync(DoctorParameters parameters);
        Task<int> GetDoctorsCountAsync(DoctorParameters parameters);
        Task<Doctor> GetDoctorByAccountIdAsync(string accountId, bool trackChanges);
        void UpdateDoctor(Doctor doctor);
        void DeleteDoctor(Doctor doctor);
    }
}
