namespace BelshefaaApp.Core.Entities
{
    public class Carer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Relationship { get; set; } = string.Empty;

        public string Role { get; set; } = "Standard";
        public List<Patient> Patients { get; set; } = new List<Patient>();


    }
}
