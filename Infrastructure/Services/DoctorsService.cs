using Application.Interfaces;
using AutoMapper;
using Domain;
using Domain.Entities;
using Domain.Exceptions;
using Domain.RequestParameters;
using Infrastructure.Extensions;
using InnoClinic.SharedModels.DTOs.Profiles.Incoming;
using InnoClinic.SharedModels.DTOs.Profiles.Outgoing;
using InnoClinic.SharedModels.Messages;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class DoctorsService : IDoctorsService
    {
        private IRepositoryManager _repositoryManager;
        private readonly ISendEndpoint _sendEndpoint;
        private IMapper _mapper;

        public DoctorsService(IRepositoryManager repositoryManager, IMapper mapper, IBus bus, IConfiguration configuration) { 
            _repositoryManager = repositoryManager;
            var uri = configuration.GetSection("RabbitMq:Uri").Value;
            var doctorUpdatedQueue = configuration.GetSection("RabbitMq:QueueNames:DoctorUpdated").Value;
            _sendEndpoint = bus.GetSendEndpoint(new Uri(uri + doctorUpdatedQueue)).GetAwaiter().GetResult();
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

        public async Task UpdateDoctorAsync(Guid doctorId, UpdateDoctorIncomingDto incomingDto)
        {
            var existingDoctor = await _repositoryManager.Doctors.GetDoctorByIdAsync(doctorId, false);
            if (existingDoctor is null)
                throw new EntityNotFoundException();

            var doctor = _mapper.Map<Doctor>(incomingDto);
            doctor.Id = doctorId;
            doctor.AccountId = existingDoctor.AccountId;
            _repositoryManager.Doctors.UpdateDoctor(doctor);
            await _repositoryManager.SaveChangesAsync();

            await _sendEndpoint.Send(new DoctorProfileUpdatedMessage
            {
                Id = doctorId,
                DoctorFirstName = incomingDto.FirstName,
                DoctorLastName = incomingDto.LastName,
                DoctorMiddleName = incomingDto.MiddleName
            });
        }
    }
}
