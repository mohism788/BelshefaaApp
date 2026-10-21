using AutoMapper;
using BelshefaaApp.Core.Dtos;
using BelshefaaApp.Core.Entities;
using BelshefaaApp.Infrastructure.Services;

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
                .ForMember(d => d.Address, o => o.MapFrom(s => string.IsNullOrWhiteSpace(s.Address) ? null : s.Address.Trim()))
                .ForMember(d => d.Notes, o => o.MapFrom(s => string.IsNullOrWhiteSpace(s.Notes) ? null : s.Notes.Trim()));

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
                .ForMember(d => d.Address, o => o.MapFrom(s => string.IsNullOrWhiteSpace(s.Address) ? null : s.Address.Trim()))
                .ForMember(d => d.Notes, o => o.MapFrom(s => string.IsNullOrWhiteSpace(s.Notes) ? null : s.Notes.Trim()));

            CreateMap<Carer, CarerDto>();

            CreateMap<CreateCarerDto, Carer>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.Account, o => o.Ignore())
                .ForMember(d => d.AccountId, o => o.Ignore())
                .ForMember(d => d.FullName, o => o.MapFrom(s => s.FullName.Trim()))
                .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => string.IsNullOrWhiteSpace(s.PhoneNumber) ? null : s.PhoneNumber.Trim()))
                .ForMember(d => d.PasscodeHash, o => o.MapFrom(s => PasswordHasher.Hash(s.Passcode)));

            CreateMap<UpdateCarerDto, Carer>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.Account, o => o.Ignore())
                .ForMember(d => d.AccountId, o => o.Ignore())
                .ForMember(d => d.FullName, o => o.MapFrom(s => s.FullName.Trim()))
                .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => string.IsNullOrWhiteSpace(s.PhoneNumber) ? null : s.PhoneNumber.Trim()))
                .ForMember(d => d.PasscodeHash, o =>
                {
                    o.PreCondition(s => s.Passcode != null);
                    o.MapFrom(s => PasswordHasher.Hash(s.Passcode!));
                });

            CreateMap<Medication, MedicationDto>();

            CreateMap<CreateMedicationDto, Medication>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.Account, o => o.Ignore())
                .ForMember(d => d.AccountId, o => o.Ignore())
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name.Trim()))
                .ForMember(d => d.Description, o => o.MapFrom(s => string.IsNullOrWhiteSpace(s.Description) ? null : s.Description.Trim()));

            CreateMap<UpdateMedicationDto, Medication>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.Account, o => o.Ignore())
                .ForMember(d => d.AccountId, o => o.Ignore())
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name.Trim()))
                .ForMember(d => d.Description, o => o.MapFrom(s => string.IsNullOrWhiteSpace(s.Description) ? null : s.Description.Trim()));

            CreateMap<ActivityLog, ActivityLogDto>()
                .ConstructUsing(log => new ActivityLogDto(
                    log.Id,
                    log.Action,
                    log.PatientId,
                    log.Patient == null ? null : $"{log.Patient.FirstName} {log.Patient.LastName}",
                    log.CarerId,
                    log.Carer == null ? null : log.Carer.FullName,
                    log.MedicationId,
                    log.Medication == null ? null : log.Medication.Name,
                    log.Details,
                    log.CreatedAt))
                .ForMember(d => d.PatientName, o => o.Ignore())
                .ForMember(d => d.CarerName, o => o.Ignore());
        }
    }
}
