namespace Core.Entities
{
    public class BaseClass
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool Deleted { get; set; }
    }
}
