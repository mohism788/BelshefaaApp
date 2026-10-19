using BelshefaaApp.Core.Entities;

namespace BelshefaaApp.Core.Interfaces
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
