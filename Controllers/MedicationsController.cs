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
        [RequireAdminCarer]
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

        [HttpPut("{id:int}")]
        [RequireAdminCarer]
        [ProducesResponseType(typeof(MedicationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MedicationDto>> Update(int id, UpdateMedicationDto dto, CancellationToken cancellationToken)
        {
            var accountId = GetAccountId();
            if (accountId is null)
            {
                return Unauthorized();
            }

            var medication = await _medicationService.UpdateAsync(accountId.Value, id, dto, cancellationToken);
            if (medication is null)
            {
                return NotFound(new { message = "Medication not found, or another medication with this name already exists." });
            }

            return Ok(medication);
        }

        [HttpDelete("{id:int}")]
        [RequireAdminCarer]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var accountId = GetAccountId();
            if (accountId is null)
            {
                return Unauthorized();
            }

            var deleted = await _medicationService.DeleteAsync(accountId.Value, id, cancellationToken);
            return deleted ? NoContent() : NotFound();
        }

        private int? GetAccountId()
        {
            var claim = User.FindFirstValue("sub");

            return int.TryParse(claim, out var accountId) ? accountId : null;
        }
    }
}
