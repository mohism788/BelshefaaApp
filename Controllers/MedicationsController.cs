using System.Security.Claims;
using BelshefaaApp.Core.Dtos;
using BelshefaaApp.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BelshefaaApp.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MedicationsController : ControllerBase
    {
        private readonly IMedicationService _medicationService;

        public MedicationsController(IMedicationService medicationService)
        {
            _medicationService = medicationService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<MedicationDto>>> GetAll(CancellationToken cancellationToken)
        {
            var accountId = GetAccountId();
            if (accountId is null)
            {
                return Unauthorized();
            }

            var medications = await _medicationService.GetAllAsync(accountId.Value, cancellationToken);
            return Ok(medications);
        }

        [HttpPost]
        [ProducesResponseType(typeof(MedicationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<MedicationDto>> Create(CreateMedicationDto dto, CancellationToken cancellationToken)
        {
            var accountId = GetAccountId();
            if (accountId is null)
            {
                return Unauthorized();
            }

            var medication = await _medicationService.CreateAsync(accountId.Value, dto, cancellationToken);
            if (medication is null)
            {
                return Conflict(new { message = "A medication with this name already exists." });
            }

            return CreatedAtAction(nameof(GetAll), medication);
        }

        private int? GetAccountId()
        {
            var claim = User.FindFirstValue("sub");

            return int.TryParse(claim, out var accountId) ? accountId : null;
        }
    }
}
