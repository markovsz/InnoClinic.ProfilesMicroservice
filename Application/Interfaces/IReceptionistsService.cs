using Application.DTOs.Incoming;
using Application.DTOs.Outgoing;

namespace Application.Interfaces
{
    public interface IReceptionistsService
    {
        public Task<Guid> CreateReceptionistAsync(ReceptionistIncomingDto incomingDto);
        public Task<ReceptionistOutgoingDto> GetReceptionistByIdAsync(Guid receptionistId);
        public Task<IEnumerable<ReceptionistOutgoingDto>> GetReceptionistsAsync();
        public Task UpdateReceptionistAsync(Guid receptionistId, ReceptionistIncomingDto incomingDto);
        public Task DeleteReceptionistByIdAsync(Guid receptionistId);
    }
}
