namespace BelshefaaApp.Core.Entities
{
    public class Medication
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Dosage { get; set; } 
        public string DosageUnit { get; set; } = string.Empty;
        public int Frequency { get; set; } 
        public string FrequencyUnit { get; set; } = string.Empty; 
        public DateTime AddedDate { get; set; } = DateTime.UtcNow;
        public List<Patient> Patients { get; set; } = new List<Patient>();
    }
}
