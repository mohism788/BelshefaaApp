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
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
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

        private int? GetAccountId()
        {
            var claim = User.FindFirstValue("sub");

            return int.TryParse(claim, out var accountId) ? accountId : null;
        }
    }
}
