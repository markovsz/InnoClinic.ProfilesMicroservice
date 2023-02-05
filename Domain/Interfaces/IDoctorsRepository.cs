﻿using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IDoctorsRepository
    {
        Task CreateDoctorAsync(Doctor doctor);
        Task<Doctor> GetDoctorByIdAsync(int doctorId);
        Task<IEnumerable<Doctor>> GetDoctorsAsync();
        void UpdateDoctor(Doctor doctor);
        void DeleteDoctor(Doctor doctor);
    }
}
