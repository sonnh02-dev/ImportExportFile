using ImportExportFile.Application.Abstractions.Files;
using ImportExportFile.Application.Dtos;
using System.Text.Json;

namespace ImportExportFile.Infrastructure.Files.Readers.Books
{
    public class JsonBooksFileReader : IFileReader<BookDto>
    {
        public bool CanRead(string extension) =>
            extension.Equals(".json", StringComparison.OrdinalIgnoreCase);

        public IEnumerable<BookDto> Read(Stream stream)
        {
            var root = JsonSerializer.Deserialize<List<BookDto>>(stream);
            return root ?? new List<BookDto>();
        }

    }

}
