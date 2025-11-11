using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using ImportExportFile.Application.Abstractions.Files;
using ImportExportFile.Application.Dtos;
using ImportExportFile.Application.Dtos.Reports;
using ImportExportFile.Application.Services;
using ImportExportFile.Domain.Entities;
using ImportExportFile.Domain.IRepositories;
using ImportExportFile.Infrastructure.Files.Readers;
using ImportExportFile.Infrastructure.Files.Readers.Books;
using ImportExportFile.Infrastructure.Files.Storages;
using ImportExportFile.Infrastructure.Files.Writers;
using ImportExportFile.Infrastructure.Files.Writers.BooksReport;
using ImportExportFile.Infrastructure.Persistence;
using ImportExportFile.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ImportExportFile.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Register file readers

            services.AddScoped<IFileStorage, S3FileStorage>();
            services.AddScoped<FileReaderFactory<BookDto>>();
            services.AddScoped<IFileReader<BookDto>, ExcelBooksFileReader>();
            services.AddScoped<IFileReader<BookDto>, JsonBooksFileReader>();
            services.AddScoped<IFileReader<BookDto>, CsvBooksFileReader>();
            services.AddScoped<IFileReader<BookDto>, TxtBooksFileReader>();
            services.AddScoped<IFileReader<BookDto>, XmlBooksFileReader>();


            services.AddScoped<FileWriterFactory<BookReportDto>>();
            services.AddScoped<IFileWriter<BookReportDto>, ExcelBooksReportFileWriter>();
            services.AddScoped<IFileWriter<BookReportDto>, JsonBooksReportFileWriter>();
            services.AddScoped<IFileWriter<BookReportDto>, TxtBooksReportFileWriter>();


            services.AddScoped<IBookRepository, BookRepository>();


            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );

            services.Configure<AwsSettings>(configuration.GetSection("Aws"));
            services.AddScoped<IAmazonS3>(sp =>
            {
                var awsOptions = sp.GetRequiredService<IOptions<AwsSettings>>().Value;

                var credentials = new BasicAWSCredentials(
                    awsOptions.AccessKey,
                    awsOptions.SecretKey
                );

                return new AmazonS3Client(
                    credentials,
                    RegionEndpoint.GetBySystemName(awsOptions.Region)
                );
            });

        }
    }
}
