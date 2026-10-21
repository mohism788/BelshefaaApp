namespace BelshefaaApp.Core.Dtos
{
    public record AuthResponseDto(
        int AccountId,
        string Email,
        string FullName,
        string Role,
        string Token,
        DateTime ExpiresAtUtc);
}
