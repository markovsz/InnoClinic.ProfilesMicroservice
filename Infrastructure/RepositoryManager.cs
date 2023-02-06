using Domain;
using Domain.Interfaces;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public class RepositoryManager : IRepositoryManager
    {
        private IDoctorsRepository _doctorsRepository;
        private IPatientsRepository _patientsRepository;
        private IReceptionistsRepository _receptionistsRepository;
        private RepositoryContext _context;

        public RepositoryManager(RepositoryContext context) {
            _context = context;
        }

        public IDoctorsRepository Doctors
        {
            get
            {
                if(_doctorsRepository is null) 
                    _doctorsRepository = new DoctorsRepository(_context);
                return _doctorsRepository;
            }
        }

        public IPatientsRepository Patients
        {
            get
            {
                if (_patientsRepository is null)
                    _patientsRepository = new PatientsRepository(_context);
                return _patientsRepository;
            }
        }

        public IReceptionistsRepository Receptionists
        {
            get
            {
                if (_receptionistsRepository is null)
                    _receptionistsRepository = new ReceptionistsRepository(_context);
                return _receptionistsRepository;
            }
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
