using BelshefaaApp.Core.Dtos;
using BelshefaaApp.Core.Entities;
using BelshefaaApp.Core.Interfaces;

namespace BelshefaaApp.Infrastructure.Services
{
    public sealed class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IPatientRepository patientRepository, IUnitOfWork unitOfWork)
        {
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<PatientDto>> GetAllAsync(int accountId, CancellationToken cancellationToken = default)
        {
            var patients = await _patientRepository.ListByAccountAsync(accountId, cancellationToken);
            return patients.Select(Map).ToList();
        }

        public async Task<PatientDto?> GetByIdAsync(int accountId, int id, CancellationToken cancellationToken = default)
        {
            var patient = await _patientRepository.FindOwnedAsync(accountId, id, cancellationToken);
            return patient is null ? null : Map(patient);
        }

        public async Task<PatientDto> CreateAsync(int accountId, CreatePatientDto dto, CancellationToken cancellationToken = default)
        {
            var patient = new Patient
            {
                AccountId = accountId,
                FirstName = dto.FirstName.Trim(),
                LastName = dto.LastName.Trim(),
                NationalId = Normalize(dto.NationalId),
                PhoneNumber = dto.PhoneNumber.Trim(),
                Email = dto.Email?.Trim().ToLowerInvariant(),
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                Address = dto.Address?.Trim(),
                Notes = dto.Notes?.Trim(),
            };

            await _patientRepository.AddAsync(patient, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Map(patient);
        }

        public async Task<PatientDto?> UpdateAsync(int accountId, int id, UpdatePatientDto dto, CancellationToken cancellationToken = default)
        {
            var patient = await _patientRepository.FindOwnedAsync(accountId, id, cancellationToken);
            if (patient is null)
            {
                return null;
            }

            patient.FirstName = dto.FirstName.Trim();
            patient.LastName = dto.LastName.Trim();
            patient.NationalId = Normalize(dto.NationalId);
            patient.PhoneNumber = dto.PhoneNumber.Trim();
            patient.Email = dto.Email?.Trim().ToLowerInvariant();
            patient.DateOfBirth = dto.DateOfBirth;
            patient.Gender = dto.Gender;
            patient.Address = dto.Address?.Trim();
            patient.Notes = dto.Notes?.Trim();

            _patientRepository.Update(patient);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Map(patient);
        }

        public async Task<bool> DeleteAsync(int accountId, int id, CancellationToken cancellationToken = default)
        {
            var patient = await _patientRepository.FindOwnedAsync(accountId, id, cancellationToken);
            if (patient is null)
            {
                return false;
            }

            _patientRepository.Remove(patient);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        private static string? Normalize(string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        private static PatientDto Map(Patient patient)
        {
            return new PatientDto(
                patient.Id,
                patient.FirstName,
                patient.LastName,
                patient.NationalId,
                patient.PhoneNumber,
                patient.Email,
                patient.DateOfBirth,
                patient.Gender,
                patient.Address,
                patient.Notes,
                patient.CreatedAt);
        }
    }
}
