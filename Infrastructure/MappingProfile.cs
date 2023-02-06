using Application.DTOs.Incoming;
using Application.DTOs.Outgoing;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<DoctorIncomingDto, Doctor>();
            CreateMap<Doctor, DoctorOutgoingDto>();

            CreateMap<PatientIncomingDto, Patient>();
            CreateMap<Patient, PatientOutgoingDto>();

            CreateMap<ReceptionistIncomingDto, Receptionist>();
            CreateMap<Receptionist, ReceptionistOutgoingDto>();
        }
    }
}
