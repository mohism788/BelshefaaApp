using System.ComponentModel.DataAnnotations;

namespace BelshefaaApp.Core.Dtos
{
    public record AdministerMedicationDto(
        int CarerId,
        int MedicationId,
        [property: StringLength(500)]
        string? Notes);
}
