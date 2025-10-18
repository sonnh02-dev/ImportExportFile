namespace ImportExportFile.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
        public ICollection<History> Histories { get; set; } = new List<History>();
    }
}
