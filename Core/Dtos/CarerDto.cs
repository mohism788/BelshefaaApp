namespace BelshefaaApp.Core.Dtos
{
    public record CarerDto(
        int Id,
        string FullName,
        string? PhoneNumber,
        DateTime CreatedAt);
}
