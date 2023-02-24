﻿using Application.DTOs.Incoming;
using Application.DTOs.Outgoing;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Domain.Entities;
using Infrastructure.Extensions;

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
            doctor.Status = status;
            await _repositoryManager.SaveChangesAsync();
        }

        public async Task<Guid> CreateDoctorAsync(DoctorIncomingDto incomingDto)
        {
            var doctor = _mapper.Map<Doctor>(incomingDto);
            await _repositoryManager.Doctors.CreateDoctorAsync(doctor);
            await _repositoryManager.SaveChangesAsync();
            return doctor.Id;
        }

        public async Task DeleteDoctorByIdAsync(Guid doctorId)
        {
            var doctor = await _repositoryManager.Doctors.GetDoctorByIdAsync(doctorId, false);
            _repositoryManager.Doctors.DeleteDoctor(doctor);
            await _repositoryManager.SaveChangesAsync();
        }

        public async Task<DoctorOutgoingDto> GetDoctorByIdAsync(Guid doctorId)
        {
            var doctor = await _repositoryManager.Doctors.GetDoctorByIdAsync(doctorId, false);
            var outgoingDoctor = _mapper.Map<DoctorOutgoingDto>(doctor);
            return outgoingDoctor;
        }

        public async Task<IEnumerable<DoctorOutgoingDto>> GetDoctorsAsync()
        {
            var doctors = await _repositoryManager.Doctors.GetDoctorsAsync();
            var outgoingDoctors = _mapper.Map<IEnumerable<DoctorOutgoingDto>>(doctors);
            return outgoingDoctors;
        }

        public async Task UpdateDoctorAsync(Guid doctorId, DoctorIncomingDto incomingDto)
        {
            var doctor = _mapper.Map<Doctor>(incomingDto);
            doctor.Id = doctorId;
            _repositoryManager.Doctors.UpdateDoctor(doctor);
            await _repositoryManager.SaveChangesAsync();
        }
    }
}
