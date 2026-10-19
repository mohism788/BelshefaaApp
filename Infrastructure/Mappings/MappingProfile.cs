using AutoMapper;
using BelshefaaApp.Core.Dtos;
using BelshefaaApp.Core.Entities;

namespace BelshefaaApp.Infrastructure.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientDto>();

            CreateMap<CreatePatientDto, Patient>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.Account, o => o.Ignore())
                .ForMember(d => d.AccountId, o => o.Ignore())
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName.Trim()))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName.Trim()))
                .ForMember(d => d.NationalId, o => o.MapFrom(s => string.IsNullOrWhiteSpace(s.NationalId) ? null : s.NationalId.Trim()))
                .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.PhoneNumber.Trim()))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email == null ? null : s.Email.Trim().ToLowerInvariant()))
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Address == null ? null : s.Address.Trim()))
                .ForMember(d => d.Notes, o => o.MapFrom(s => s.Notes == null ? null : s.Notes.Trim()));

            CreateMap<UpdatePatientDto, Patient>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.Account, o => o.Ignore())
                .ForMember(d => d.AccountId, o => o.Ignore())
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName.Trim()))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName.Trim()))
                .ForMember(d => d.NationalId, o => o.MapFrom(s => string.IsNullOrWhiteSpace(s.NationalId) ? null : s.NationalId.Trim()))
                .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.PhoneNumber.Trim()))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email == null ? null : s.Email.Trim().ToLowerInvariant()))
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Address == null ? null : s.Address.Trim()))
                .ForMember(d => d.Notes, o => o.MapFrom(s => s.Notes == null ? null : s.Notes.Trim()));

            CreateMap<Carer, CarerDto>();

            CreateMap<CreateCarerDto, Carer>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.Account, o => o.Ignore())
                .ForMember(d => d.AccountId, o => o.Ignore())
                .ForMember(d => d.FullName, o => o.MapFrom(s => s.FullName.Trim()))
                .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => string.IsNullOrWhiteSpace(s.PhoneNumber) ? null : s.PhoneNumber.Trim()));

            CreateMap<Medication, MedicationDto>();

            CreateMap<CreateMedicationDto, Medication>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.Account, o => o.Ignore())
                .ForMember(d => d.AccountId, o => o.Ignore())
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name.Trim()))
                .ForMember(d => d.Description, o => o.MapFrom(s => string.IsNullOrWhiteSpace(s.Description) ? null : s.Description.Trim()));

            CreateMap<ActivityLog, ActivityLogDto>()
                .ForMember(d => d.PatientName, o => o.MapFrom(s => s.Patient == null ? null : $"{s.Patient.FirstName} {s.Patient.LastName}"))
                .ForMember(d => d.CarerName, o => o.MapFrom(s => s.Carer == null ? null : s.Carer.FullName))
                .ForMember(d => d.MedicationName, o => o.MapFrom(s => s.Medication == null ? null : s.Medication.Name));
        }
    }
}
