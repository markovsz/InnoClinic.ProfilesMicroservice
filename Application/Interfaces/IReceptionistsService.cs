using InnoClinic.SharedModels.DTOs.Profiles.Incoming;
using InnoClinic.SharedModels.DTOs.Profiles.Outgoing;

namespace Application.Interfaces
{
    public interface IReceptionistsService
    {
        public Task<Guid> CreateReceptionistAsync(ReceptionistIncomingDto incomingDto, string accountId);
        public Task<ReceptionistOutgoingDto> GetReceptionistByIdAsync(Guid receptionistId);
        public Task<ReceptionistOutgoingDto> GetReceptionistProfileAsync(string accountId);
        public Task<IEnumerable<ReceptionistOutgoingDto>> GetReceptionistsAsync();
        public Task UpdateReceptionistAsync(Guid receptionistId, UpdateReceptionistIncomingDto incomingDto);
        public Task DeleteReceptionistByIdAsync(Guid receptionistId);
    }
}
