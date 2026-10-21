using BelshefaaApp.Core.Dtos;

namespace BelshefaaApp.Core.Interfaces
{
    public interface IMedicationService
    {
        Task<IReadOnlyList<MedicationDto>> GetAllAsync(int accountId, CancellationToken cancellationToken = default);

        Task<MedicationDto?> CreateAsync(int accountId, CreateMedicationDto dto, CancellationToken cancellationToken = default);

        Task<MedicationDto?> UpdateAsync(int accountId, int id, UpdateMedicationDto dto, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(int accountId, int id, CancellationToken cancellationToken = default);

        Task<bool> AdministerAsync(int accountId, int patientId, AdministerMedicationDto dto, CancellationToken cancellationToken = default);
    }
}
