namespace BelshefaaApp.Core.Entities
{
    public abstract class EntityBase
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
