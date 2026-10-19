using AutoMapper;
using BelshefaaApp.Core.Dtos;
using BelshefaaApp.Core.Entities;
using BelshefaaApp.Core.Interfaces;

namespace BelshefaaApp.Infrastructure.Services
{
    public sealed class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IActivityLogService _activityLogService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PatientService(
            IPatientRepository patientRepository,
            IActivityLogService activityLogService,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _patientRepository = patientRepository;
            _activityLogService = activityLogService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<PatientDto>> GetAllAsync(int accountId, CancellationToken cancellationToken = default)
        {
            var patients = await _patientRepository.ListByAccountAsync(accountId, cancellationToken);
            return patients.Select(p => _mapper.Map<PatientDto>(p)).ToList();
        }

        public async Task<PatientDto?> GetByIdAsync(int accountId, int id, CancellationToken cancellationToken = default)
        {
            var patient = await _patientRepository.FindOwnedAsync(accountId, id, cancellationToken);
            return patient is null ? null : _mapper.Map<PatientDto>(patient);
        }

        public async Task<PatientDto> CreateAsync(int accountId, CreatePatientDto dto, CancellationToken cancellationToken = default)
        {
            var patient = _mapper.Map<Patient>(dto);
            patient.AccountId = accountId;

            await _patientRepository.AddAsync(patient, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _activityLogService.LogAsync(
                accountId,
                LogAction.PatientRegistered,
                patientId: patient.Id,
                details: $"Registered patient '{patient.FirstName} {patient.LastName}'",
                cancellationToken: cancellationToken);

            return _mapper.Map<PatientDto>(patient);
        }

        public async Task<PatientDto?> UpdateAsync(int accountId, int id, UpdatePatientDto dto, CancellationToken cancellationToken = default)
        {
            var patient = await _patientRepository.FindOwnedAsync(accountId, id, cancellationToken);
            if (patient is null)
            {
                return null;
            }

            _mapper.Map(dto, patient);

            _patientRepository.Update(patient);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<PatientDto>(patient);
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
    }
}
