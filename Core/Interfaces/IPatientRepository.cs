using BelshefaaApp.Core.Entities;

namespace BelshefaaApp.Core.Interfaces
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Task<IReadOnlyList<Patient>> ListByAccountAsync(int accountId, CancellationToken cancellationToken = default);

        Task<Patient?> FindOwnedAsync(int accountId, int id, CancellationToken cancellationToken = default);
    }
}
