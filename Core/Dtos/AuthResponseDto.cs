namespace BelshefaaApp.Core.Dtos
{
    public record AuthResponseDto(
        int AccountId,
        string Email,
        string FullName,
        string Token,
        DateTime ExpiresAtUtc);
}
