using BelshefaaApp.Core.Entities;

namespace BelshefaaApp.Core.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
