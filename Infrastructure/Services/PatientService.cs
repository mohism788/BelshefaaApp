using AutoMapper;
using BelshefaaApp.Core.Dtos;
using BelshefaaApp.Core.Entities;
using BelshefaaApp.Core.Interfaces;

namespace BelshefaaApp.Infrastructure.Services
{
    public sealed class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActivityLogService _activityLogService;
        private readonly IMapper _mapper;

        public PatientService(
            IUnitOfWork unitOfWork,
            IActivityLogService activityLogService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _activityLogService = activityLogService;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<PatientDto>> GetAllAsync(int accountId, CancellationToken cancellationToken = default)
        {
            var patients = await _unitOfWork.Patients.ListByAccountAsync(accountId, cancellationToken);
            return patients.Select(p => _mapper.Map<PatientDto>(p)).ToList();
        }

        public async Task<PatientDto?> GetByIdAsync(int accountId, int id, CancellationToken cancellationToken = default)
        {
            var patient = await _unitOfWork.Patients.FindOwnedAsync(accountId, id, cancellationToken);
            return patient is null ? null : _mapper.Map<PatientDto>(patient);
        }

        public async Task<PatientDto> CreateAsync(int accountId, CreatePatientDto dto, CancellationToken cancellationToken = default)
        {
            var patient = _mapper.Map<Patient>(dto);
            patient.AccountId = accountId;

            await _unitOfWork.Patients.AddAsync(patient, cancellationToken);
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
            var patient = await _unitOfWork.Patients.FindOwnedAsync(accountId, id, cancellationToken);
            if (patient is null)
            {
                return null;
            }

            _mapper.Map(dto, patient);

            _unitOfWork.Patients.Update(patient);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<PatientDto>(patient);
        }

        public async Task<bool> DeleteAsync(int accountId, int id, CancellationToken cancellationToken = default)
        {
            var patient = await _unitOfWork.Patients.FindOwnedAsync(accountId, id, cancellationToken);
            if (patient is null)
            {
                return false;
            }

            _unitOfWork.Patients.Remove(patient);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
