namespace ImportExportFile.Domain.Entities
{
    public class Genre
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
