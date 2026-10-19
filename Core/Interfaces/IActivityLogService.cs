using BelshefaaApp.Core.Dtos;
using BelshefaaApp.Core.Entities;

namespace BelshefaaApp.Core.Interfaces
{
    public interface IActivityLogService
    {
        Task LogAsync(
            int accountId,
            LogAction action,
            int? patientId = null,
            int? carerId = null,
            int? medicationId = null,
            string? details = null,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ActivityLogDto>> GetPatientLogsAsync(int accountId, int patientId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ActivityLogDto>> GetAllAsync(int accountId, CancellationToken cancellationToken = default);
    }
}
