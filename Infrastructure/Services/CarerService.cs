using AutoMapper;
using BelshefaaApp.Core.Dtos;
using BelshefaaApp.Core.Entities;
using BelshefaaApp.Core.Interfaces;

namespace BelshefaaApp.Infrastructure.Services
{
    public sealed class CarerService : ICarerService
    {
        private readonly ICarerRepository _carerRepository;
        private readonly IActivityLogService _activityLogService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CarerService(
            ICarerRepository carerRepository,
            IActivityLogService activityLogService,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _carerRepository = carerRepository;
            _activityLogService = activityLogService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CarerDto>> GetAllAsync(int accountId, CancellationToken cancellationToken = default)
        {
            var carers = await _carerRepository.ListByAccountAsync(accountId, cancellationToken);
            return carers.Select(c => _mapper.Map<CarerDto>(c)).ToList();
        }

        public async Task<CarerDto> CreateAsync(int accountId, CreateCarerDto dto, CancellationToken cancellationToken = default)
        {
            var carer = _mapper.Map<Carer>(dto);
            carer.AccountId = accountId;

            await _carerRepository.AddAsync(carer, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _activityLogService.LogAsync(
                accountId,
                LogAction.CarerAdded,
                carerId: carer.Id,
                details: $"Added carer '{carer.FullName}'",
                cancellationToken: cancellationToken);

            return _mapper.Map<CarerDto>(carer);
        }

        public async Task<CarerDto?> UpdateAsync(int accountId, int id, UpdateCarerDto dto, CancellationToken cancellationToken = default)
        {
            var carer = await _carerRepository.FindOwnedAsync(accountId, id, cancellationToken);
            if (carer is null)
            {
                return null;
            }

            _mapper.Map(dto, carer);

            _carerRepository.Update(carer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CarerDto>(carer);
        }

        public async Task<bool> DeleteAsync(int accountId, int id, CancellationToken cancellationToken = default)
        {
            var carer = await _carerRepository.FindOwnedAsync(accountId, id, cancellationToken);
            if (carer is null)
            {
                return false;
            }

            _carerRepository.Remove(carer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
