using BelshefaaApp.Core.Entities;

namespace BelshefaaApp.Core.Dtos
{
    public record ActivityLogDto(
        int Id,
        LogAction Action,
        int? PatientId,
        string? PatientName,
        int? CarerId,
        string? CarerName,
        int? MedicationId,
        string? MedicationName,
        string? Details,
        DateTime CreatedAt);
}
