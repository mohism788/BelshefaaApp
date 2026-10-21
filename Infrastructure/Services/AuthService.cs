using BelshefaaApp.Core.Dtos;
using BelshefaaApp.Core.Entities;
using BelshefaaApp.Core.Interfaces;

namespace BelshefaaApp.Infrastructure.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public AuthService(
            IAccountRepository accountRepository,
            IUnitOfWork unitOfWork,
            ITokenService tokenService)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto?> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken = default)
        {
            var email = NormalizeEmail(dto.Email);

            var existingAccount = await _accountRepository.GetByEmailAsync(email, cancellationToken);
            if (existingAccount is not null)
            {
                return null;
            }

            var isFirstAccount = !await _accountRepository.AnyAsync(a => true, cancellationToken);

            var account = new Account
            {
                Email = email,
                PasswordHash = PasswordHasher.Hash(dto.Password),
                FullName = dto.FullName.Trim(),
                Role = isFirstAccount ? AccountRole.Admin : AccountRole.Member,
            };

            await _accountRepository.AddAsync(account, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return BuildResponse(account);
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default)
        {
            var email = NormalizeEmail(dto.Email);

            var account = await _accountRepository.GetByEmailAsync(email, cancellationToken);
            if (account is null || !account.IsActive || !PasswordHasher.Verify(dto.Password, account.PasswordHash))
            {
                return null;
            }

            return BuildResponse(account);
        }

        private AuthResponseDto BuildResponse(Account account)
        {
            var token = _tokenService.CreateToken(account);

            return new AuthResponseDto(
                account.Id,
                account.Email,
                account.FullName,
                account.Role.ToString(),
                token.Token,
                token.ExpiresAtUtc);
        }

        private static string NormalizeEmail(string email)
        {
            return email.Trim().ToLowerInvariant();
        }
    }
}
