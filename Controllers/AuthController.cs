using BelshefaaApp.Core.Dtos;
using BelshefaaApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BelshefaaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register(RegisterDto dto, CancellationToken cancellationToken)
        {
            var response = await _authService.RegisterAsync(dto, cancellationToken);
            if (response is null)
            {
                return Conflict(new { message = "An account with this email already exists." });
            }

            return CreatedAtAction(nameof(Register), response);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(LoginDto dto, CancellationToken cancellationToken)
        {
            var response = await _authService.LoginAsync(dto, cancellationToken);
            if (response is null)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }

            return Ok(response);
        }
    }
}
