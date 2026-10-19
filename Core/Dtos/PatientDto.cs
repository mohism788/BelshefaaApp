using BelshefaaApp.Core.Entities;

namespace BelshefaaApp.Core.Dtos
{
    public record PatientDto(
        int Id,
        string FirstName,
        string LastName,
        string? NationalId,
        string PhoneNumber,
        string? Email,
        DateTime? DateOfBirth,
        Gender Gender,
        string? Address,
        string? Notes,
        DateTime CreatedAt);
}
