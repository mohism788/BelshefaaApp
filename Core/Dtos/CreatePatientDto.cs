using System.ComponentModel.DataAnnotations;
using BelshefaaApp.Core.Entities;

namespace BelshefaaApp.Core.Dtos
{
    public record CreatePatientDto(
        [property: Required, StringLength(100)]
        string FirstName,
        [property: Required, StringLength(100)]
        string LastName,
        [property: StringLength(20)]
        string? NationalId,
        [property: Required, StringLength(20)]
        string PhoneNumber,
        [property: EmailAddress, StringLength(256)]
        string? Email,
        [property: DataType(DataType.Date)]
        DateTime? DateOfBirth,
        Gender Gender,
        [property: StringLength(500)]
        string? Address,
        [property: StringLength(2000)]
        string? Notes);
}
