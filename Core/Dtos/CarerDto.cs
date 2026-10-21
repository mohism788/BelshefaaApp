using BelshefaaApp.Core.Entities;

namespace BelshefaaApp.Core.Dtos
{
    public record CarerDto(
        int Id,
        string FullName,
        string? PhoneNumber,
        CarerRelationship Relationship,
        DateTime CreatedAt);
}
