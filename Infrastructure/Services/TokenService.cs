using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BelshefaaApp.Core.Dtos;
using BelshefaaApp.Core.Entities;
using BelshefaaApp.Core.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BelshefaaApp.Infrastructure.Services
{
    public sealed class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenResult CreateToken(Account account)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetRequiredSetting("Jwt:Key")));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiresAtUtc = DateTime.UtcNow.AddMinutes(
                int.Parse(GetRequiredSetting("Jwt:ExpiryMinutes")));

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, account.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, account.Email),
                new("name", account.FullName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: GetRequiredSetting("Jwt:Issuer"),
                audience: GetRequiredSetting("Jwt:Audience"),
                claims: claims,
                expires: expiresAtUtc,
                signingCredentials: credentials);

            return new TokenResult(
                new JwtSecurityTokenHandler().WriteToken(token),
                expiresAtUtc);
        }

        private string GetRequiredSetting(string key)
        {
            return _configuration[key]
                ?? throw new InvalidOperationException($"Configuration value '{key}' is missing.");
        }
    }
}
