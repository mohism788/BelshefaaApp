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
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IActivityLogService _activityLogService;
        private readonly IMedicationService _medicationService;

        public PatientsController(
            IPatientService patientService,
            IActivityLogService activityLogService,
            IMedicationService medicationService)
        {
            _patientService = patientService;
            _activityLogService = activityLogService;
            _medicationService = medicationService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<PatientDto>>> GetAll(CancellationToken cancellationToken)
        {
            var accountId = GetAccountId();
            if (accountId is null)
            {
                return Unauthorized();
            }

            var patients = await _patientService.GetAllAsync(accountId.Value, cancellationToken);
            return Ok(patients);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PatientDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var accountId = GetAccountId();
            if (accountId is null)
            {
                return Unauthorized();
            }

            var patient = await _patientService.GetByIdAsync(accountId.Value, id, cancellationToken);
            return patient is null ? NotFound() : Ok(patient);
        }

        [HttpPost]
        [RequireAdminCarer]
        public async Task<ActionResult<PatientDto>> Create(CreatePatientDto dto, CancellationToken cancellationToken)
        {
            var accountId = GetAccountId();
            if (accountId is null)
            {
                return Unauthorized();
            }

            var patient = await _patientService.CreateAsync(accountId.Value, dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient);
        }

        [HttpPut("{id:int}")]
        [RequireAdminCarer]
        public async Task<ActionResult<PatientDto>> Update(int id, UpdatePatientDto dto, CancellationToken cancellationToken)
        {
            var accountId = GetAccountId();
            if (accountId is null)
            {
                return Unauthorized();
            }

            var patient = await _patientService.UpdateAsync(accountId.Value, id, dto, cancellationToken);
            return patient is null ? NotFound() : Ok(patient);
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

            var deleted = await _patientService.DeleteAsync(accountId.Value, id, cancellationToken);
            return deleted ? NoContent() : NotFound();
        }

        [HttpGet("{id:int}/logs")]
        public async Task<ActionResult<IReadOnlyList<ActivityLogDto>>> GetLogs(int id, CancellationToken cancellationToken)
        {
            var accountId = GetAccountId();
            if (accountId is null)
            {
                return Unauthorized();
            }

            var logs = await _activityLogService.GetPatientLogsAsync(accountId.Value, id, cancellationToken);
            return Ok(logs);
        }

        [HttpPost("{id:int}/medication-given")]
        [RequireAdminCarer]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AdministerMedication(int id, AdministerMedicationDto dto, CancellationToken cancellationToken)
        {
            var accountId = GetAccountId();
            if (accountId is null)
            {
                return Unauthorized();
            }

            var administered = await _medicationService.AdministerAsync(accountId.Value, id, dto, cancellationToken);
            return administered ? StatusCode(StatusCodes.Status201Created) : NotFound();
        }

        private int? GetAccountId()
        {
            var claim = User.FindFirstValue("sub");

            return int.TryParse(claim, out var accountId) ? accountId : null;
        }
    }
}
