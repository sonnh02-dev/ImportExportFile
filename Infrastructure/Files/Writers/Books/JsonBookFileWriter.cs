using ImportExportFile.Application.Abstractions.Files;
using ImportExportFile.Application.Dtos;
using ImportExportFile.Domain.Entities;
using Newtonsoft.Json;

namespace ImportExportFile.Infrastructure.Files.Writers.Books
{
    public class JsonBookFileWriter : IFileWriter<BookDto>
    {
        public bool CanWrite(string extension) =>
            extension.Equals(".json", StringComparison.OrdinalIgnoreCase);

        public void Write(string filePath, List<BookDto> books)
        {
            var json = JsonConvert.SerializeObject(books, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

      
    }
}
