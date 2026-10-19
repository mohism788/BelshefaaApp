using BelshefaaApp.Core.Entities;
using BelshefaaApp.Core.Interfaces;
using BelshefaaApp.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace BelshefaaApp.Database.Repositories
{
    public class MedicationRepository : GenericRepository<Medication>, IMedicationRepository
    {
        public MedicationRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IReadOnlyList<Medication>> ListByAccountAsync(int accountId, CancellationToken cancellationToken = default)
        {
            return await DbSet.AsNoTracking()
                .Where(m => m.AccountId == accountId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Medication?> GetByNameAsync(int accountId, string name, CancellationToken cancellationToken = default)
        {
            return await DbSet.FirstOrDefaultAsync(
                m => m.AccountId == accountId && m.Name == name,
                cancellationToken);
        }
    }
}
