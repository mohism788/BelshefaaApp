namespace BelshefaaApp.Core.Entities
{
    public class Medication : EntityBase
    {
        public int AccountId { get; set; }

        public Account Account { get; set; } = null!;

        public required string Name { get; set; }

        public string? Description { get; set; }
    }
}
