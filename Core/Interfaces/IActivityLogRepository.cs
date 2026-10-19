using BelshefaaApp.Core.Entities;

namespace BelshefaaApp.Core.Interfaces
{
    public interface IActivityLogRepository : IGenericRepository<ActivityLog>
    {
        Task<IReadOnlyList<ActivityLog>> ListByPatientAsync(int patientId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ActivityLog>> ListByAccountAsync(int accountId, CancellationToken cancellationToken = default);
    }
}
