using BelshefaaApp.Core.Entities;
using BelshefaaApp.Core.Interfaces;
using BelshefaaApp.Database.Data;

namespace BelshefaaApp.Database.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(AppDbContext context)
            : base(context)
        {
        }

        public Task<Account?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return FindAsync(a => a.Email == email, cancellationToken);
        }
    }
}
