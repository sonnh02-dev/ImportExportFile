using ImportExportFile.Application.Dtos;
using ImportExportFile.Application.Dtos.Request;
using ImportExportFile.Application.Services;
using ImportExportFile.Domain.Entities;
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
            var filePath = await _exportService.ExportBooksReportAsync();
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Path.GetFileName(filePath));
        }
    }
}
