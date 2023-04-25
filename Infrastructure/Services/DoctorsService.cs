using Application.DTOs.Incoming;
using Application.DTOs.Outgoing;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Domain.Entities;
using Domain.RequestParameters;
using Domain.Exceptions;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore.Query;

namespace Infrastructure.Services
{
    public class DoctorsService : IDoctorsService
    {
        private IRepositoryManager _repositoryManager;
        private IMapper _mapper;

        public DoctorsService(IRepositoryManager repositoryManager, IMapper mapper) { 
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task ChangeDoctorStatusAsync(Guid doctorId, string doctorStatus)
        {
            var status = doctorStatus.FromStringToDoctorStatusesEnum();
            var doctor = await _repositoryManager.Doctors.GetDoctorByIdAsync(doctorId, true);
            if (doctor is null)
                throw new EntityNotFoundException();
            doctor.Status = status;
            await _repositoryManager.SaveChangesAsync();
        }

        public async Task<Guid> CreateDoctorAsync(DoctorIncomingDto incomingDto, string accountId)
        {
            var doctorForCheck = await _repositoryManager.Doctors.GetDoctorByAccountIdAsync(accountId, false);
            if (doctorForCheck is not null)
                throw new EntityAlreadyExistsException();

            var doctor = _mapper.Map<Doctor>(incomingDto);
            await _repositoryManager.Doctors.CreateDoctorAsync(doctor);
            await _repositoryManager.SaveChangesAsync();
            return doctor.Id;
        }

        public async Task DeleteDoctorByIdAsync(Guid doctorId)
        {
            var doctor = await _repositoryManager.Doctors.GetDoctorByIdAsync(doctorId, false);
            if (doctor is null)
                throw new EntityNotFoundException();
            _repositoryManager.Doctors.DeleteDoctor(doctor);
            await _repositoryManager.SaveChangesAsync();
        }

        public async Task<DoctorOutgoingDto> GetDoctorByIdAsync(Guid doctorId)
        {
            var doctor = await _repositoryManager.Doctors.GetDoctorByIdAsync(doctorId, false);
            var outgoingDoctor = _mapper.Map<DoctorOutgoingDto>(doctor);
            return outgoingDoctor;
        }

        public async Task<DoctorsPaginationOutgoingDto> GetDoctorsAsync(DoctorParameters parameters)
        {
            var doctors = await _repositoryManager.Doctors.GetDoctorsAsync(parameters);
            var outgoingDoctors = _mapper.Map<IEnumerable<DoctorOutgoingDto>>(doctors);
            var doctorsIterator = doctors.GetEnumerator();
            foreach(var outgoingDoctor in outgoingDoctors)
            {
                var doctor = doctorsIterator.Current;
                outgoingDoctor.Experience = DateTime.Now.Year - doctor.CareerStartYear + 1;
                doctorsIterator.MoveNext();
            }
            var doctorsCount = await _repositoryManager.Doctors.GetDoctorsCountAsync(parameters);
            var paginationResult = new DoctorsPaginationOutgoingDto
            {
                Entities = outgoingDoctors,
                PagesCount = (doctorsCount + parameters.Size - 1) / parameters.Size
            };
            return paginationResult;
        }

        public async Task<DoctorsPaginationOutgoingDto> GetDoctorsAtWorkAsync(DoctorParameters parameters)
        {
            var doctors = await _repositoryManager.Doctors.GetDoctorsAtWorkAsync(parameters);
            var outgoingDoctors = _mapper.Map<IEnumerable<DoctorOutgoingDto>>(doctors);
            var doctorsIterator = doctors.GetEnumerator();
            foreach (var outgoingDoctor in outgoingDoctors)
            {
                var doctor = doctorsIterator.Current;
                outgoingDoctor.Experience = DateTime.Now.Year - doctor.CareerStartYear + 1;
                doctorsIterator.MoveNext();
            }
            var doctorsCount = await _repositoryManager.Doctors.GetDoctorsCountAsync(parameters);
            var paginationResult = new DoctorsPaginationOutgoingDto
            {
                Entities = outgoingDoctors,
                PagesCount = (doctorsCount + parameters.Size - 1) / parameters.Size
            };
            return paginationResult;
        }

        public async Task UpdateDoctorAsync(Guid doctorId, DoctorIncomingDto incomingDto)
        {
            var doctorForCheck = await _repositoryManager.Doctors.GetDoctorByIdAsync(doctorId, false);
            if (doctorForCheck is null)
                throw new EntityNotFoundException();

            var doctor = _mapper.Map<Doctor>(incomingDto);
            doctor.Id = doctorId;
            _repositoryManager.Doctors.UpdateDoctor(doctor);
            await _repositoryManager.SaveChangesAsync();
        }
    }
}
