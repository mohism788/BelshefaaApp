namespace BelshefaaApp.Core.Entities
{
    public class Account : EntityBase
    {
        public required string Email { get; set; }

        public required string PasswordHash { get; set; }

        public required string FullName { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Patient> Patients { get; set; } = [];
    }
}
