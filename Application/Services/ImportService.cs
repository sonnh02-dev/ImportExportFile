using ImportExportFile.Application.Dtos.Responses;
using ImportExportFile.Application.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using ImportExportFile.Infrastructure.Files.Readers;
using ImportExportFile.Application.Abstractions.Files;

namespace ImportExportFile.Application.Services
{
    public class ImportService
    {
        private readonly IFileStorage _fileStorage;
        private readonly FileReaderFactory<BookDto> _bookReaderFactory;

        public ImportService(IFileStorage fileStorage, FileReaderFactory<BookDto> bookReaderFactory)
        {
            _fileStorage = fileStorage;
            _bookReaderFactory = bookReaderFactory;
        }

        public async Task<ImportResult> ImportBooksAsync(IFormFile file)
        {
            var filePath = await _fileStorage.SaveAsync(file);
            var extension = Path.GetExtension(filePath);
            var reader = _bookReaderFactory.GetReader(extension);
            var books = reader.Read(filePath);

           // _fileStorage.Delete(filePath);

            return new ImportResult
            {
                TotalRecords = books.Count,
                SuccessCount = books.Count
            };
        }
    }
}
