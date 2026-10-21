using System.Security.Claims;
using BelshefaaApp.Core.Dtos;
using BelshefaaApp.Core.Interfaces;
using BelshefaaApp.Infrastructure.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BelshefaaApp.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CarersController : ControllerBase
    {
        private readonly ICarerService _carerService;

        public CarersController(ICarerService carerService)
        {
            _carerService = carerService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CarerDto>>> GetAll(CancellationToken cancellationToken)
        {
            var accountId = GetAccountId();
            if (accountId is null)
            {
                return Unauthorized();
            }

            var carers = await _carerService.GetAllAsync(accountId.Value, cancellationToken);
            return Ok(carers);
        }

        [HttpPost]
        [RequireAdminCarer(AllowWhenNoCarers = true)]
        public async Task<ActionResult<CarerDto>> Create(CreateCarerDto dto, CancellationToken cancellationToken)
        {
            var accountId = GetAccountId();
            if (accountId is null)
            {
                return Unauthorized();
            }

            var carer = await _carerService.CreateAsync(accountId.Value, dto, cancellationToken);
            return CreatedAtAction(nameof(GetAll), carer);
        }

        [HttpPut("{id:int}")]
        [RequireAdminCarer]
        public async Task<ActionResult<CarerDto>> Update(int id, UpdateCarerDto dto, CancellationToken cancellationToken)
        {
            var accountId = GetAccountId();
            if (accountId is null)
            {
                return Unauthorized();
            }

            var carer = await _carerService.UpdateAsync(accountId.Value, id, dto, cancellationToken);
            return carer is null ? NotFound() : Ok(carer);
        }

        [HttpDelete("{id:int}")]
        [RequireAdminCarer]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var accountId = GetAccountId();
            if (accountId is null)
            {
                return Unauthorized();
            }

            var deleted = await _carerService.DeleteAsync(accountId.Value, id, cancellationToken);
            return deleted ? NoContent() : NotFound();
        }

        private int? GetAccountId()
        {
            var claim = User.FindFirstValue("sub");

            return int.TryParse(claim, out var accountId) ? accountId : null;
        }
    }
}
