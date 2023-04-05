using Application.DTOs.Incoming;
using Application.DTOs.Outgoing;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Extensions;

namespace Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<DoctorIncomingDto, Doctor>()
                .ForMember(e => e.Status, options => 
                    options.MapFrom(src => src.Status.FromStringToDoctorStatusesEnum()));
            CreateMap<Doctor, DoctorOutgoingDto>()
                .ForMember(e => e.Status, options =>
                    options.MapFrom(src => src.Status.ToString()));

            CreateMap<PatientIncomingDto, Patient>();
            CreateMap<Patient, PatientOutgoingDto>();

            CreateMap<ReceptionistIncomingDto, Receptionist>();
            CreateMap<Receptionist, ReceptionistOutgoingDto>();
        }

        
    }
}
