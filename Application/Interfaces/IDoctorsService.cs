using Application.DTOs.Incoming;
using Application.DTOs.Outgoing;

namespace Application.Interfaces
{
    public interface IDoctorsService
    {
        public Task<Guid> CreateDoctorAsync(DoctorIncomingDto incomingDto);
        public Task<DoctorOutgoingDto> GetDoctorByIdAsync(Guid doctorId);
        public Task<IEnumerable<DoctorOutgoingDto>> GetDoctorsAsync();
        public Task UpdateDoctorAsync(Guid doctorId, DoctorIncomingDto incomingDto);
        public Task DeleteDoctorByIdAsync(Guid doctorId);
    }
}
