using ImportExportFile.Application.Abstractions.Files;
using ImportExportFile.Application.Dtos.Reports;
using ImportExportFile.Domain.Entities;
using System.Text;

namespace ImportExportFile.Infrastructure.Files.Writers.BooksReport
{
    public class TxtBooksReportFileWriter : IFileWriter<BookReportDto>
    {
        public bool CanWrite(string extension) =>
            extension.Equals(".txt", StringComparison.OrdinalIgnoreCase);

        public void Write(string filePath, IEnumerable<BookReportDto> books)
        {
            var sb = new StringBuilder();

            sb.AppendLine("===== Books Report =====");
            sb.AppendLine($"Generated At: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine(new string('=', 50));

            foreach (var b in books)
            {
                sb.AppendLine($"ID              : {b.Id}");
                sb.AppendLine($"Title           : {b.Title}");
                sb.AppendLine($"Author          : {b.Author}");
                sb.AppendLine($"Genre           : {b.GenreName}");
                sb.AppendLine($"Price           : {b.Price:C}");
                sb.AppendLine($"Feedback Count  : {b.FeedbackCount}");
                sb.AppendLine($"Average Rating  : {b.AverageRating:F1}");
                sb.AppendLine($"Total Views     : {b.TotalViews}");
                sb.AppendLine($"Last Viewed At  : {(b.LastViewedAt.HasValue ? b.LastViewedAt.Value.ToString("yyyy-MM-dd HH:mm:ss") : "N/A")}");
                sb.AppendLine(new string('-', 50));
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);


            //===== Books Report =====
            //Generated At: 2025 - 10 - 19 05:12:33
            //==================================================

            //ID              : e7c84d8a-8e91-4f42-b53b-3a4f40b77c21
            //Title           : Clean Code
            //Author          : Robert C. Martin
            //Genre           : Technology
            //Price           : $39.99
            //Feedback Count  : 15
            //Average Rating  : 4.8
            //Total Views     : 245
            //Last Viewed At  : 2025-10-19 10:45:00
            //--------------------------------------------------
        }
    }
}
