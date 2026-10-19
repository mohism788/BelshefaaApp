namespace BelshefaaApp.Core.Entities
{
    public class ActivityLog : EntityBase
    {
        public int AccountId { get; set; }

        public Account Account { get; set; } = null!;

        public int? PatientId { get; set; }

        public Patient? Patient { get; set; }

        public int? CarerId { get; set; }

        public Carer? Carer { get; set; }

        public int? MedicationId { get; set; }

        public Medication? Medication { get; set; }

        public LogAction Action { get; set; }

        public string? Details { get; set; }
    }
}
