namespace BelshefaaApp.Core.Entities
{
    public class Patient : EntityBase
    {
        public int AccountId { get; set; }

        public Account Account { get; set; } = null!;

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public string? NationalId { get; set; }

        public required string PhoneNumber { get; set; }

        public string? Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string? Address { get; set; }

        public string? Notes { get; set; }
    }
}
