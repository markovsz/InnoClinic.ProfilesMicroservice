using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IReceptionistsRepository
    {
        Task CreateReceptionistAsync(Receptionist receptionist);
        Task<Receptionist> GetReceptionistByIdAsync(int receptionistId);
        Task<IEnumerable<Receptionist>> GetReceptionistsAsync();
        void UpdateReceptionist(Receptionist receptionist);
        void DeleteReceptionist(Receptionist receptionist);
    }
}
