using ImportExportFile.Application.Abstractions.Files;
using ImportExportFile.Application.Dtos;

namespace ImportExportFile.Infrastructure.Files.Readers.Books
{
    public class TxtBooksFileReader : IFileReader<BookDto>
    {
        public bool CanRead(string extension) =>
            extension.Equals(".txt", StringComparison.OrdinalIgnoreCase);

        public IEnumerable<BookDto> Read(Stream stream)
        {
            var books = new List<BookDto>();

            using var reader = new StreamReader(stream);
            string? line;
            bool isFirstLine = true;

            while ((line = reader.ReadLine()) != null)
            {
                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue; // bỏ header
                }

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
