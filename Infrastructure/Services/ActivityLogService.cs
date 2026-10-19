using AutoMapper;
using BelshefaaApp.Core.Dtos;
using BelshefaaApp.Core.Entities;
using BelshefaaApp.Core.Interfaces;

namespace BelshefaaApp.Infrastructure.Services
{
    public sealed class ActivityLogService : IActivityLogService
    {
        private readonly IActivityLogRepository _logRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ActivityLogService(
            IActivityLogRepository logRepository,
            IPatientRepository patientRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _logRepository = logRepository;
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task LogAsync(
            int accountId,
            LogAction action,
            int? patientId = null,
            int? carerId = null,
            int? medicationId = null,
            string? details = null,
            CancellationToken cancellationToken = default)
        {
            var log = new ActivityLog
            {
                AccountId = accountId,
                Action = action,
                PatientId = patientId,
                CarerId = carerId,
                MedicationId = medicationId,
                Details = details,
            };

            await _logRepository.AddAsync(log, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<ActivityLogDto>> GetPatientLogsAsync(int accountId, int patientId, CancellationToken cancellationToken = default)
        {
            var patient = await _patientRepository.FindOwnedAsync(accountId, patientId, cancellationToken);
            if (patient is null)
            {
                return [];
            }

            var logs = await _logRepository.ListByPatientAsync(patientId, cancellationToken);
            return logs.Select(l => _mapper.Map<ActivityLogDto>(l)).ToList();
        }

        public async Task<IReadOnlyList<ActivityLogDto>> GetAllAsync(int accountId, CancellationToken cancellationToken = default)
        {
            var logs = await _logRepository.ListByAccountAsync(accountId, cancellationToken);
            return logs.Select(l => _mapper.Map<ActivityLogDto>(l)).ToList();
        }
    }
}
