using BelshefaaApp.Core.Dtos;

namespace BelshefaaApp.Core.Interfaces
{
    public interface ICarerService
    {
        Task<IReadOnlyList<CarerDto>> GetAllAsync(int accountId, CancellationToken cancellationToken = default);

        Task<CarerDto> CreateAsync(int accountId, CreateCarerDto dto, CancellationToken cancellationToken = default);
    }
}
