using BelshefaaApp.Core.Dtos;
using BelshefaaApp.Core.Entities;

namespace BelshefaaApp.Core.Interfaces
{
    public interface ITokenService
    {
        TokenResult CreateToken(Account account);
    }
}
