using ImportExportFile.Domain.Entities;
using ImportExportFile.Models;
using ImportExportService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImportExportService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImportExportController : ControllerBase
    {
        private readonly ImportService _importService;
        private readonly ExportService _exportService;

        public ImportExportController(ImportService importService, ExportService exportService)
        {
            _importService = importService;
            _exportService = exportService;
        }

        [HttpPost("import")]
        [Consumes("multipart/form-data")] 
        public async Task<IActionResult> ImportBooks([FromForm] ImportBookRequest request)
        {
            var result = await _importService.ImportBooksAsync(request.File);
            return Ok(result);
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportBooks()
        {
            var books = new List<Book>
            {
                new() { Id = "1", Author = "Alice", Title = "C# Guide", Genre = "Tech", Price = 9.99, PublishDate = DateTime.Now },
                new() { Id = "2", Author = "Bob", Title = "ASP.NET 8", Genre = "Tech", Price = 12.50, PublishDate = DateTime.Now }
            };

            var filePath = await _exportService.ExportBooksAsync(books);
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Path.GetFileName(filePath));
        }
    }
}
