namespace ImportExportFile.Entities
{
    public class Feedback
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public int Rating { get; set; } 
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual User User { get; set; } = default!;
        public virtual Book Book { get; set; } = default!;
    }
}
