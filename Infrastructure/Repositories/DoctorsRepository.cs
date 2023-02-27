using Domain.Entities;
using Domain.Interfaces;
using Domain.RequestParameters;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DoctorsRepository : RepositoryBase<Doctor>, IDoctorsRepository
    {
        public DoctorsRepository(RepositoryContext context)
            : base(context)
        {
        }

        public async Task CreateDoctorAsync(Doctor doctor) => await CreateAsync(doctor);

        public void DeleteDoctor(Doctor doctor) => Delete(doctor);

        public async Task<Doctor> GetDoctorByIdAsync(Guid doctorId, bool trackChanges) =>
            await GetByCondition(e => e.Id.Equals(doctorId), trackChanges)
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<Doctor>> GetDoctorsAsync(DoctorParameters parameters) =>
            await GetAll()
            .ApplyPagination(parameters)
            .DoctorParametersHandler(parameters)
            .ToListAsync();

        public async Task<int> GetDoctorsCountAsync(DoctorParameters parameters) =>
            await GetAll()
            .DoctorParametersHandler(parameters)
            .CountAsync();

        public void UpdateDoctor(Doctor doctor) => Update(doctor);
    }
}
