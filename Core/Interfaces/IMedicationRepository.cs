using BelshefaaApp.Core.Entities;

namespace BelshefaaApp.Core.Interfaces
{
    public interface IMedicationRepository : IGenericRepository<Medication>
    {
        Task<IReadOnlyList<Medication>> ListByAccountAsync(int accountId, CancellationToken cancellationToken = default);

        Task<Medication?> GetByNameAsync(int accountId, string name, CancellationToken cancellationToken = default);
    }
}
