using ImportExportFile.Application.Abstractions.Files;
using ImportExportFile.Application.Dtos;
using ImportExportFile.Domain.Entities;
using OfficeOpenXml;

namespace ImportExportFile.Infrastructure.Files.Readers.Books
{

    public class ExcelBooksFileReader : IFileReader<BookDto>
    {
        public ExcelBooksFileReader()
        {
            ExcelPackage.License.SetNonCommercialPersonal("ImportExportService");

        }
        public bool CanRead(string extension) =>
            extension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase);

        public IEnumerable<BookDto> Read(Stream stream)
        {
            var books = new List<BookDto>();
        
            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets["Books"];

            var rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++) // bỏ header 
            {
                books.Add(new BookDto
                {
                    Author = worksheet.Cells[row, 2].Text,
                    Title = worksheet.Cells[row, 3].Text,
                    Genre = worksheet.Cells[row, 4].Text,
                    Price = double.TryParse(worksheet.Cells[row, 5].Text, out var p) ? p : 0,
                    PublishDate = DateTime.TryParse(worksheet.Cells[row, 6].Text, out var d) ? d : DateTime.MinValue,
                    Description = worksheet.Cells[row, 7].Text
                });
            }

            return books;
        }
       
    }

}
