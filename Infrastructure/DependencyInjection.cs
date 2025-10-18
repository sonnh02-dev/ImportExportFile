using ImportExportFile.Application.Abstractions.Files;
using ImportExportFile.Application.Dtos;
using ImportExportFile.Application.Services;
using ImportExportFile.Domain.Entities;
using ImportExportFile.Infrastructure.Files.Readers;
using ImportExportFile.Infrastructure.Files.Readers.Books;
using ImportExportFile.Infrastructure.Files.Storage;
using ImportExportFile.Infrastructure.Files.Writers;
using ImportExportFile.Infrastructure.Files.Writers.Books;

namespace ImportExportFile.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Register file readers
            services.AddScoped<ImportService>();
            services.AddScoped<IFileStorage,LocalFileStorage>();
            services.AddScoped<FileReaderFactory<BookDto>>();
            services.AddScoped<IFileReader<BookDto>, ExcelBookFileReader>();
            services.AddScoped<IFileReader<BookDto>, JsonBookFileReader>();
            services.AddScoped<IFileReader<BookDto>, CsvBookFileReader>();
            services.AddScoped<IFileReader<BookDto>, TxtBookFileReader>();
            services.AddScoped<IFileReader<BookDto>, XmlBookFileReader>();


            services.AddScoped<ExportService>();
            services.AddScoped<FileWriterFactory<BookDto>>();
            services.AddScoped<IFileWriter<BookDto>, ExcelBookFileWriter>();
            services.AddScoped<IFileWriter<BookDto>, JsonBookFileWriter>();
            services.AddScoped<IFileWriter<BookDto>, TxtBookFileWriter>();

        }
    }
}
