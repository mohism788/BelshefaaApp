using BelshefaaApp.Core.Dtos;

namespace BelshefaaApp.Core.Interfaces
{
    public interface IPatientService
    {
        Task<IReadOnlyList<PatientDto>> GetAllAsync(int accountId, CancellationToken cancellationToken = default);

        Task<PatientDto?> GetByIdAsync(int accountId, int id, CancellationToken cancellationToken = default);

        Task<PatientDto> CreateAsync(int accountId, CreatePatientDto dto, CancellationToken cancellationToken = default);

        Task<PatientDto?> UpdateAsync(int accountId, int id, UpdatePatientDto dto, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(int accountId, int id, CancellationToken cancellationToken = default);
    }
}
