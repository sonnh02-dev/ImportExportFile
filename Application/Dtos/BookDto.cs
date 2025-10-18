
namespace ImportExportFile.Application.Dtos
{
    public sealed class BookDto
    {
        public Guid Id { get; init; }
        public string Author { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public string Genre { get; init; }  = string.Empty;
        public double Price { get; init; }
        public DateTime PublishDate { get; init; }
        public string Description { get; init; } = string.Empty;
    }
}
