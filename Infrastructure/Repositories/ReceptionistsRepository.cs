using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReceptionistsRepository : RepositoryBase<Receptionist>, IReceptionistsRepository
    {
        public ReceptionistsRepository(RepositoryContext context)
            : base(context)
        {
        }

        public async Task CreateReceptionistAsync(Receptionist receptionist) => await CreateAsync(receptionist);

        public void DeleteReceptionist(Receptionist receptionist) => Delete(receptionist);

        public async Task<Receptionist> GetReceptionistByIdAsync(Guid receptionistId, bool trackChanges) =>
            await GetByCondition(e => e.Id.Equals(receptionistId), trackChanges)
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<Receptionist>> GetReceptionistsAsync() =>
            await GetAll()
            .ToListAsync();

        public void UpdateReceptionist(Receptionist receptionist) => Update(receptionist);
    }
}
