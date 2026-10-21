using AutoMapper;
using BelshefaaApp.Core.Dtos;
using BelshefaaApp.Core.Entities;
using BelshefaaApp.Core.Interfaces;

namespace BelshefaaApp.Infrastructure.Services
{
    public sealed class MedicationService : IMedicationService
    {
        private readonly IMedicationRepository _medicationRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly ICarerRepository _carerRepository;
        private readonly IActivityLogService _activityLogService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MedicationService(
            IMedicationRepository medicationRepository,
            IPatientRepository patientRepository,
            ICarerRepository carerRepository,
            IActivityLogService activityLogService,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _medicationRepository = medicationRepository;
            _patientRepository = patientRepository;
            _carerRepository = carerRepository;
            _activityLogService = activityLogService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<MedicationDto>> GetAllAsync(int accountId, CancellationToken cancellationToken = default)
        {
            var medications = await _medicationRepository.ListByAccountAsync(accountId, cancellationToken);
            return medications.Select(m => _mapper.Map<MedicationDto>(m)).ToList();
        }

        public async Task<MedicationDto?> CreateAsync(int accountId, CreateMedicationDto dto, CancellationToken cancellationToken = default)
        {
            var medication = _mapper.Map<Medication>(dto);

            var existing = await _medicationRepository.GetByNameAsync(accountId, medication.Name, cancellationToken);
            if (existing is not null)
            {
                return null;
            }

            medication.AccountId = accountId;

            await _medicationRepository.AddAsync(medication, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _activityLogService.LogAsync(
                accountId,
                LogAction.MedicationAdded,
                medicationId: medication.Id,
                details: $"Added medication '{medication.Name}'",
                cancellationToken: cancellationToken);

            return _mapper.Map<MedicationDto>(medication);
        }

        public async Task<MedicationDto?> UpdateAsync(int accountId, int id, UpdateMedicationDto dto, CancellationToken cancellationToken = default)
        {
            var medication = await _medicationRepository.FindOwnedAsync(accountId, id, cancellationToken);
            if (medication is null)
            {
                return null;
            }

            var duplicate = await _medicationRepository.GetByNameAsync(accountId, dto.Name.Trim(), cancellationToken);
            if (duplicate is not null && duplicate.Id != medication.Id)
            {
                return null;
            }

            _mapper.Map(dto, medication);

            _medicationRepository.Update(medication);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<MedicationDto>(medication);
        }

        public async Task<bool> DeleteAsync(int accountId, int id, CancellationToken cancellationToken = default)
        {
            var medication = await _medicationRepository.FindOwnedAsync(accountId, id, cancellationToken);
            if (medication is null)
            {
                return false;
            }

            _medicationRepository.Remove(medication);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> AdministerAsync(int accountId, int patientId, AdministerMedicationDto dto, CancellationToken cancellationToken = default)
        {
            var patient = await _patientRepository.FindOwnedAsync(accountId, patientId, cancellationToken);
            var carer = await _carerRepository.FindAsync(c => c.AccountId == accountId && c.Id == dto.CarerId, cancellationToken);
            var medication = await _medicationRepository.FindAsync(m => m.AccountId == accountId && m.Id == dto.MedicationId, cancellationToken);

            if (patient is null || carer is null || medication is null)
            {
                return false;
            }

            await _activityLogService.LogAsync(
                accountId,
                LogAction.MedicationGiven,
                patientId: patient.Id,
                carerId: carer.Id,
                medicationId: medication.Id,
                details: string.IsNullOrWhiteSpace(dto.Notes) ? null : dto.Notes.Trim(),
                cancellationToken: cancellationToken);

            return true;
        }
    }
}
