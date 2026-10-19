using System.ComponentModel.DataAnnotations;

namespace BelshefaaApp.Core.Dtos
{
    public record CreateMedicationDto(
        [property: Required, StringLength(200)]
        string Name,
        [property: StringLength(1000)]
        string? Description);
}
