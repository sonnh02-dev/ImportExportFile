using AutoMapper;
using AutoMapper.QueryableExtensions;
using ImportExportFile.Application.Dtos;
using ImportExportFile.Application.Dtos.Reports;
using ImportExportFile.Domain.Entities;
using ImportExportFile.Domain.IRepositories;
using ImportExportFile.Infrastructure.Files.Writers;
using Microsoft.EntityFrameworkCore;

namespace ImportExportFile.Application.Services
{
   
    public class ExportService
    {
        private readonly FileWriterFactory<BookReportDto> _fileWriterFactory;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public ExportService(
            FileWriterFactory<BookReportDto> fileWriterFactory,
            IBookRepository bookRepository, IMapper mapper)
        {
            _fileWriterFactory = fileWriterFactory;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }



        public async Task<string> ExportBooksReportAsync()
        {
            var fileName = $"BookReport_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
            var folder = Path.Combine(Path.GetTempPath(), "BookReports");
            Directory.CreateDirectory(folder);
            var filePath = Path.Combine(folder, fileName);

            var books = _bookRepository.GetBooksWithDetails()
                        .ProjectTo<BookReportDto>(_mapper.ConfigurationProvider);

            var extension = Path.GetExtension(filePath);
            var fileWriter = _fileWriterFactory.GetWriter(extension);
            fileWriter.Write(filePath, books);

            return filePath;

        }
    }

}
