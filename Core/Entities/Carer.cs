namespace BelshefaaApp.Core.Entities
{
    public class Carer : EntityBase
    {
        public int AccountId { get; set; }

        public Account Account { get; set; } = null!;

        public required string FullName { get; set; }

        public string? PhoneNumber { get; set; }

        public required string PasscodeHash { get; set; }

        public CarerRelationship Relationship { get; set; } = CarerRelationship.FamilyMember;
    }
}
