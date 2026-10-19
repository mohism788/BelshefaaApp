using System.ComponentModel.DataAnnotations;

namespace BelshefaaApp.Core.Dtos
{
    public record LoginDto(
        [property: Required, EmailAddress]
        string Email,
        [property: Required]
        string Password);
}
