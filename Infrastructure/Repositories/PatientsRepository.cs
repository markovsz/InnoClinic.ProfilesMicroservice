﻿using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PatientsRepository : RepositoryBase<Patient>, IPatientsRepository
    {
        public PatientsRepository(RepositoryContext context)
            : base(context)
        {
        }

        public async Task CreatePatientAsync(Patient patient) => await CreateAsync(patient);

        public void DeletePatient(Patient patient) => Delete(patient);

        public async Task<Patient> GetPatientByIdAsync(Guid patientId, bool trackChanges) =>
            await GetByCondition(e => e.Id.Equals(patientId), trackChanges)
            .FirstOrDefaultAsync();
        
        public async Task<Patient> GetPatientByAccountIdAsync(string accountId, bool trackChanges) =>
            await GetByCondition(e => e.AccountId.Equals(accountId), trackChanges)
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<Patient>> GetPatientsAsync() =>
            await GetAll()
            .ToListAsync();

        public void UpdatePatient(Patient patient) => Update(patient);
    }
}
