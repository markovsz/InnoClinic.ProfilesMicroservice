using Domain.Interfaces;

namespace Domain
{
    public interface IRepositoryManager
    {
        IDoctorsRepository Doctors { get; }
        IPatientsRepository Patients { get; }
        IReceptionistsRepository Receptionists { get; }
        Task SaveChangesAsync();
    }
}
