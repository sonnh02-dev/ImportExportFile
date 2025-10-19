using ImportExportFile.Application.Abstractions.Files;
using ImportExportFile.Application.Dtos.Reports;
using System.Globalization;
using System.Text;

namespace ImportExportFile.Infrastructure.Files.Writers.BooksReport
{
    public class CsvBooksReportFileWriter : IFileWriter<BookReportDto>
    {
        public bool CanWrite(string extension) =>
            extension.Equals(".csv", StringComparison.OrdinalIgnoreCase);

        public void Write(string filePath, IEnumerable<BookReportDto> books)
        {
            var sb = new StringBuilder();

            // Header
            sb.AppendLine("Id,Title,Author,Genre,Price,FeedbackCount,AverageRating,TotalViews,LastViewedAt");

            // Data
            foreach (var b in books)
            {
                sb.AppendLine($"{b.Id}," +
                              $"{Escape(b.Title)}," +
                              $"{Escape(b.Author)}," +
                              $"{Escape(b.GenreName)}," +
                              $"{b.Price.ToString(CultureInfo.InvariantCulture)}," +
                              $"{b.FeedbackCount}," +
                              $"{b.AverageRating.ToString(CultureInfo.InvariantCulture)}," +
                              $"{b.TotalViews}," +
                              $"{b.LastViewedAt?.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}");
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        private static string Escape(string? value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            // Đảm bảo dữ liệu không phá vỡ định dạng CSV
            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
            {
                value = value.Replace("\"", "\"\"");
                return $"\"{value}\"";
            }

            return value;
        }
    }
}
