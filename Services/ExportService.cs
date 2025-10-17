using ImportExportFile.Entities;
using ImportExportService.Utils;

namespace ImportExportService.Services
{
    public class ExportService
    {
        public async Task<string> ExportBooksAsync(List<Book> books)
        {
            var filePath = Path.Combine(Path.GetTempPath(), $"Books_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
            ExcelHelper.WriteBooksToExcel(filePath, books);
            return await Task.FromResult(filePath);
        }
    }
}
