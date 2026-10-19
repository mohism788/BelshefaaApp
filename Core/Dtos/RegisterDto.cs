using System.ComponentModel.DataAnnotations;

namespace BelshefaaApp.Core.Dtos
{
    public record RegisterDto(
        [property: Required, EmailAddress, StringLength(256)]
        string Email,
        [property: Required, MinLength(8), StringLength(128)]
        string Password,
        [property: Required, StringLength(100)]
        string FullName);
}
