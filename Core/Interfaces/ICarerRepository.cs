using BelshefaaApp.Core.Entities;

namespace BelshefaaApp.Core.Interfaces
{
    public interface ICarerRepository : IGenericRepository<Carer>
    {
        Task<IReadOnlyList<Carer>> ListByAccountAsync(int accountId, CancellationToken cancellationToken = default);
    }
}
