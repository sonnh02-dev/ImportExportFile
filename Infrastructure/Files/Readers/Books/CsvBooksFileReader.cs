using CsvHelper;
using ImportExportFile.Application.Abstractions.Files;
using ImportExportFile.Application.Dtos;
using System.Formats.Asn1;
using System.Globalization;

namespace ImportExportFile.Infrastructure.Files.Readers.Books
{
    public class CsvBooksFileReader : IFileReader<BookDto>
    {
        public bool CanRead(string extension) =>
            extension.Equals(".csv", StringComparison.OrdinalIgnoreCase);

        public IEnumerable<BookDto> Read(Stream stream)
        {
            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<BookDto>().ToList();
            return records;
        }
    }
}
