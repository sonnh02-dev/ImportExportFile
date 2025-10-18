using ImportExportFile.Application.Dtos;
using ImportExportFile.Domain.Entities;
using ImportExportFile.Infrastructure.Files.Writers;

namespace ImportExportFile.Application.Services
{
   
    public class ExportService
    {
        private readonly FileWriterFactory<BookDto> _fileWriterFactory;

        public ExportService(FileWriterFactory<BookDto> fileWriterFactory)
        {
            _fileWriterFactory = fileWriterFactory;
        }

        public void Export(string filePath, List<BookDto> books)
        {
            var extension = Path.GetExtension(filePath);
            var writer = _fileWriterFactory.GetWriter(extension);
            writer.Write(filePath, books);
        }
    }

}
