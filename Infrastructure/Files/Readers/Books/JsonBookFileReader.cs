using ImportExportFile.Application.Abstractions.Files;
using ImportExportFile.Application.Dtos;
using ImportExportFile.Domain.Entities;
using Newtonsoft.Json;

namespace ImportExportFile.Infrastructure.Files.Readers.Books
{
    public class JsonBookFileReader : IFileReader<BookDto>
    {
        public bool CanRead(string extension) =>
            extension.Equals(".json", StringComparison.OrdinalIgnoreCase);

        public List<BookDto> Read(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var root = JsonConvert.DeserializeObject<List<BookDto>>(json);
            return  new List<BookDto>();
        }
    }

}
