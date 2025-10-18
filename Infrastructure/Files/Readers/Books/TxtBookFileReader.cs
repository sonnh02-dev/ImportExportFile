using ImportExportFile.Application.Abstractions.Files;
using ImportExportFile.Application.Dtos;

namespace ImportExportFile.Infrastructure.Files.Readers.Books
{
    public class TxtBookFileReader : IFileReader<BookDto>
    {
        public bool CanRead(string extension) =>
            extension.Equals(".txt", StringComparison.OrdinalIgnoreCase);

        public List<BookDto> Read(string filePath)
        {
            var books = new List<BookDto>();
            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines.Skip(1)) // bỏ header
            {
                var parts = line.Split('|');
                if (parts.Length < 6) continue;

                books.Add(new BookDto
                {
                    Author = parts[0],
                    Title = parts[1],
                    Genre = parts[2],
                    Price = double.TryParse(parts[3], out var p) ? p : 0,
                    PublishDate = DateTime.TryParse(parts[4], out var d) ? d : DateTime.MinValue,
                    Description = parts[5]
                });
            }

            return books;
        }
    }
}
