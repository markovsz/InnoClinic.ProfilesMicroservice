using Domain.Entities;
using Domain.RequestParameters;

namespace Infrastructure.Extensions
{
    public static class RequestParametersHandlerExtensions
    {
        public static IQueryable<Doctor> DoctorParametersHandler(this IQueryable<Doctor> doctors, DoctorParameters parameters)
        {
            if(parameters.FirstNameSearch is not null)
                doctors = doctors.Where(e => e.FirstName.Contains(parameters.FirstNameSearch));
            if (parameters.LastNameSearch is not null)
                doctors = doctors.Where(e => e.LastName.Contains(parameters.LastNameSearch));
            if (parameters.MiddleNameSearch is not null)
                doctors = doctors.Where(e => e.MiddleName.Contains(parameters.MiddleNameSearch));

            if (parameters.OfficeId is not null)
                doctors = doctors.Where(e => e.OfficeId.Equals(parameters.OfficeId));
            if (parameters.SpectializationId is not null)
                doctors = doctors.Where(e => e.SpectializationId.Equals(parameters.SpectializationId));

            return doctors;
        }

        public static IQueryable<Patient> PatientParametersHandler(this IQueryable<Patient> patients, PatientParameters parameters)
        {
            if (parameters.FirstNameSearch is not null)
                patients = patients.Where(e => e.FirstName.Contains(parameters.FirstNameSearch));
            if (parameters.LastNameSearch is not null)
                patients = patients.Where(e => e.LastName.Contains(parameters.LastNameSearch));
            if (parameters.MiddleNameSearch is not null)
                patients = patients.Where(e => e.MiddleName.Contains(parameters.MiddleNameSearch));

            return patients;
        }
    }
}
