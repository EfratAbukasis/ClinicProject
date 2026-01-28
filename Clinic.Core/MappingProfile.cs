using AutoMapper;
using Clinic.Core.DTOs;
using Clinic.Core.Entities;
using static Clinic.Core.DTOs.PatientDtos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Clinic.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // מיפוי עבור תורים
            CreateMap<Appointment, AppointmentResponseDto>().ReverseMap();
            CreateMap<AppointmentRequestDto, Appointment>();

            // מיפוי עבור רופאים
            CreateMap<Doctor, DoctorResponseDto>().ReverseMap();
            CreateMap<DoctorRequestDto, Doctor>();

            // מיפוי עבור מטופלים
            CreateMap<Patient, PatientResponseDto>().ReverseMap();
            CreateMap<PatientRequestDto, Patient>();
        }
    }
}
