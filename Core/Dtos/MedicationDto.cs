namespace BelshefaaApp.Core.Dtos
{
    public record MedicationDto(
        int Id,
        string Name,
        string? Description,
        DateTime CreatedAt);
}
