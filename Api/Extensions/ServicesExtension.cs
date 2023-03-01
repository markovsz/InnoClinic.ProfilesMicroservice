using Application.DTOs.Incoming;
using Application.Interfaces;
using Domain;
using FluentValidation;
using Infrastructure;
using Infrastructure.Services;
using Infrastructure.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Api.Extensions
{
    public static class ServicesExtension
    {
        public static void ConfigureDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("dbConnection"),
                b => b.MigrationsAssembly("Infrastructure")));
        }
        
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IDoctorsService, DoctorsService>();
            services.AddScoped<IPatientsService, PatientsService>();
            services.AddScoped<IReceptionistsService, ReceptionistsService>();
        }
        
        public static void ConfigureValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<DoctorIncomingDto>, DoctorIncomingDtoValidator>();
            services.AddScoped<IValidator<PatientIncomingDto>, PatientIncomingDtoValidator>();
            services.AddScoped<IValidator<ReceptionistIncomingDto>, ReceptionistIncomingDtoValidator>();
        }
    }
}
