using System.ComponentModel.DataAnnotations;
using BelshefaaApp.Core.Entities;

namespace BelshefaaApp.Core.Dtos
{
    public record CreateCarerDto(
        [property: Required, StringLength(100)]
        string FullName,
        [property: StringLength(20)]
        string? PhoneNumber,
        [property: Required, RegularExpression(@"^\d{4}(\d{2})?$", ErrorMessage = "Passcode must be exactly 4 or 6 digits.")]
        string Passcode,
        [property: EnumDataType(typeof(CarerRelationship))]
        CarerRelationship Relationship = CarerRelationship.FamilyMember);
}
