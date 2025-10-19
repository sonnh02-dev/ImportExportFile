using ImportExportFile.Application.Abstractions.Files;
using ImportExportFile.Application.Dtos;
using ImportExportFile.Application.Dtos.Reports;
using ImportExportFile.Domain.Entities;
using System.Text.Json;

namespace ImportExportFile.Infrastructure.Files.Writers.BooksReport
{
    public class JsonBooksReportFileWriter : IFileWriter<BookReportDto>
    {
        public bool CanWrite(string extension) =>
            extension.Equals(".json", StringComparison.OrdinalIgnoreCase);

        public void Write(string filePath, IEnumerable<BookReportDto> books)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(books, options);
            File.WriteAllText(filePath, json);
        }

      
    }
}
