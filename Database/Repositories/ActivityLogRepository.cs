using BelshefaaApp.Core.Entities;
using BelshefaaApp.Core.Interfaces;
using BelshefaaApp.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace BelshefaaApp.Database.Repositories
{
    public class ActivityLogRepository : GenericRepository<ActivityLog>, IActivityLogRepository
    {
        public ActivityLogRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IReadOnlyList<ActivityLog>> ListByPatientAsync(int patientId, CancellationToken cancellationToken = default)
        {
            return await DbSet.AsNoTracking()
                .Include(l => l.Patient)
                .Include(l => l.Carer)
                .Include(l => l.Medication)
                .Where(l => l.PatientId == patientId)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<ActivityLog>> ListByAccountAsync(int accountId, CancellationToken cancellationToken = default)
        {
            return await DbSet.AsNoTracking()
                .Include(l => l.Patient)
                .Include(l => l.Carer)
                .Include(l => l.Medication)
                .Where(l => l.AccountId == accountId)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync(cancellationToken);
        }
    }
}
