using Domain.RequestParameters;
using InnoClinic.SharedModels.DTOs.Profiles.Incoming;
using InnoClinic.SharedModels.DTOs.Profiles.Outgoing;

namespace Application.Interfaces
{
    public interface IDoctorsService
    {
        public Task<Guid> CreateDoctorAsync(DoctorIncomingDto incomingDto, string accountId);
        public Task<DoctorOutgoingDto> GetDoctorByIdAsync(Guid doctorId);
        public Task<DoctorsPaginationOutgoingDto> GetDoctorsAsync(DoctorParameters parameters);
        public Task<DoctorsPaginationOutgoingDto> GetDoctorsAtWorkAsync(DoctorParameters parameters);
        public Task UpdateDoctorAsync(Guid doctorId, UpdateDoctorIncomingDto incomingDto);
        public Task DeleteDoctorByIdAsync(Guid doctorId);
        public Task ChangeDoctorStatusAsync(Guid doctorId, string doctorStatus);
    }
}
