namespace ImportExportFile.Entities
{
    public class History
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime LastViewedAt { get; set; } = DateTime.UtcNow;
        public int ViewCount { get; set; } = 0;

        public virtual User User { get; set; } = default!;
        public virtual Book Book { get; set; } = default!;
    }
}
