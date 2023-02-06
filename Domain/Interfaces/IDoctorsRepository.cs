using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IDoctorsRepository
    {
        Task CreateDoctorAsync(Doctor doctor);
        Task<Doctor> GetDoctorByIdAsync(Guid doctorId, bool trackChanges);
        Task<IEnumerable<Doctor>> GetDoctorsAsync();
        void UpdateDoctor(Doctor doctor);
        void DeleteDoctor(Doctor doctor);
    }
}
