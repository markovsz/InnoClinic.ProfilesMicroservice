using Domain.Entities;
using Domain.RequestParameters;

namespace Domain.Interfaces
{
    public interface IDoctorsRepository
    {
        Task CreateDoctorAsync(Doctor doctor);
        Task<Doctor> GetDoctorByIdAsync(Guid doctorId, bool trackChanges);
        Task<IEnumerable<Doctor>> GetDoctorsAsync(DoctorParameters parameters);
        Task<int> GetDoctorsCountAsync(DoctorParameters parameters);
        void UpdateDoctor(Doctor doctor);
        void DeleteDoctor(Doctor doctor);
    }
}
