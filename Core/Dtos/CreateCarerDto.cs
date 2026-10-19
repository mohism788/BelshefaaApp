using System.ComponentModel.DataAnnotations;

namespace BelshefaaApp.Core.Dtos
{
    public record CreateCarerDto(
        [property: Required, StringLength(100)]
        string FullName,
        [property: StringLength(20)]
        string? PhoneNumber);
}
