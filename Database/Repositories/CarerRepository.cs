using BelshefaaApp.Core.Entities;
using BelshefaaApp.Core.Interfaces;
using BelshefaaApp.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace BelshefaaApp.Database.Repositories
{
    public class CarerRepository : GenericRepository<Carer>, ICarerRepository
    {
        public CarerRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IReadOnlyList<Carer>> ListByAccountAsync(int accountId, CancellationToken cancellationToken = default)
        {
            return await DbSet.AsNoTracking()
                .Where(c => c.AccountId == accountId)
                .ToListAsync(cancellationToken);
        }
    }
}
