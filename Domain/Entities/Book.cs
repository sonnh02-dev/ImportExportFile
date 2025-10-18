namespace ImportExportFile.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Author { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public Guid GenreId { get; set; }
        public double Price { get; set; }
        public DateTime PublishDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public Genre Genre { get; set; } = default!;
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
        public ICollection<History> Histories { get; set; } = new List<History>();

    }
}
