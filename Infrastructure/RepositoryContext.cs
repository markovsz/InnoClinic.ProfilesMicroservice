using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Receptionist> Receptionists { get; set; }

        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Doctor>().HasKey(e => e.Id);
            builder.Entity<Doctor>().Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Entity<Patient>().HasKey(e => e.Id);
            builder.Entity<Patient>().Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Entity<Receptionist>().HasKey(e => e.Id);
            builder.Entity<Receptionist>().Property(e => e.Id).ValueGeneratedOnAdd();
        }
    }
}
