using BelshefaaApp.Core.Entities;
using BelshefaaApp.Core.Interfaces;
using BelshefaaApp.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace BelshefaaApp.Database.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IReadOnlyList<Patient>> ListByAccountAsync(int accountId, CancellationToken cancellationToken = default)
        {
            return await DbSet.AsNoTracking()
                .Where(p => p.AccountId == accountId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Patient?> FindOwnedAsync(int accountId, int id, CancellationToken cancellationToken = default)
        {
            return await DbSet.FirstOrDefaultAsync(
                p => p.AccountId == accountId && p.Id == id,
                cancellationToken);
        }
    }
}
