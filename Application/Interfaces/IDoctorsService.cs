using Application.DTOs.Incoming;
using Application.DTOs.Outgoing;
using Domain.RequestParameters;

namespace Application.Interfaces
{
    public interface IDoctorsService
    {
        public Task<Guid> CreateDoctorAsync(DoctorIncomingDto incomingDto);
        public Task<DoctorOutgoingDto> GetDoctorByIdAsync(Guid doctorId);
        public Task<DoctorsPaginationOutgoingDto> GetDoctorsAsync(DoctorParameters parameters);
        public Task<DoctorsPaginationOutgoingDto> GetDoctorsAtWorkAsync(DoctorParameters parameters);
        public Task UpdateDoctorAsync(Guid doctorId, DoctorIncomingDto incomingDto);
        public Task DeleteDoctorByIdAsync(Guid doctorId);
        public Task ChangeDoctorStatusAsync(Guid doctorId, string doctorStatus);
    }
}
