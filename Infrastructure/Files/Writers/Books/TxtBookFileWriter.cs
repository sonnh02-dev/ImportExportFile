using ImportExportFile.Application.Abstractions.Files;
using ImportExportFile.Application.Dtos;
using ImportExportFile.Domain.Entities;
using System.Text;

namespace ImportExportFile.Infrastructure.Files.Writers.Books
{
    public class TxtBookFileWriter : IFileWriter<BookDto>
    {
        public bool CanWrite(string extension) =>
            extension.Equals(".txt", StringComparison.OrdinalIgnoreCase);

        public void Write(string filePath, List<BookDto> books)
        {
            var sb = new StringBuilder();
            foreach (var b in books)
            {
                sb.AppendLine($"Id: {b.Id}");
                sb.AppendLine($"Author: {b.Author}");
                sb.AppendLine($"Title: {b.Title}");
                sb.AppendLine($"Genre: {b.Genre}");
                sb.AppendLine($"Price: {b.Price}");
                sb.AppendLine($"Publish Date: {b.PublishDate:yyyy-MM-dd}");
                sb.AppendLine($"Description: {b.Description}");
                sb.AppendLine("----------------------");
            }
            File.WriteAllText(filePath, sb.ToString());
        }
    }
}
