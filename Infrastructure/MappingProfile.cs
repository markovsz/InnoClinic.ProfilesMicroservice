using AutoMapper;
using Domain.Entities;
using Infrastructure.Extensions;
using InnoClinic.SharedModels.DTOs.Profiles.Incoming;
using InnoClinic.SharedModels.DTOs.Profiles.Outgoing;

namespace Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<UpdateDoctorIncomingDto, Doctor>()
                .ForMember(e => e.Status, options =>
                    options.MapFrom(src => src.Status.FromStringToDoctorStatusesEnum()));

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
