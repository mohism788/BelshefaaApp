namespace BelshefaaApp.Core.Entities
{
    public class Allergy
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<Patient> Patients { get; set; } = new List<Patient>();
    }
}
