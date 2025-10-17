using ImportExportService.Models;
using ImportExportService.Utils;

namespace ImportExportService.Services
{
    public class ImportService
    {
        public async Task<ImportResult> ImportBooksAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new InvalidOperationException("File is empty.");

            var tempPath = Path.GetTempFileName();

            await using (var stream = new FileStream(tempPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var books = ExcelHelper.ReadBooksFromExcel(tempPath);

         
            var result = new ImportResult
            {
                TotalRecords = books.Count,
                SuccessCount = books.Count
            };

            File.Delete(tempPath);
            return result;
        }
    }
}
