using ImportExportFile.Application.Abstractions.Files;
using ImportExportFile.Application.Dtos;
using ImportExportFile.Domain.Entities;
using OfficeOpenXml;

namespace ImportExportFile.Infrastructure.Files.Writers.Books
{
    public class ExcelBookFileWriter : IFileWriter<BookDto>
    {
        public ExcelBookFileWriter()
        {
            ExcelPackage.License.SetNonCommercialPersonal("ImportExportService");

        }
        public bool CanWrite(string extension) =>
            extension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase);

        public void Write(string filePath, List<BookDto> books)
        {

            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("Books");

            ws.Cells[1, 1].Value = "Id";
            ws.Cells[1, 2].Value = "Author";
            ws.Cells[1, 3].Value = "Title";
            ws.Cells[1, 4].Value = "Genre";
            ws.Cells[1, 5].Value = "Price";
            ws.Cells[1, 6].Value = "PublishDate";
            ws.Cells[1, 7].Value = "Description";

            int row = 2;
            foreach (var b in books)
            {
                ws.Cells[row, 1].Value = b.Id;
                ws.Cells[row, 2].Value = b.Author;
                ws.Cells[row, 3].Value = b.Title;
                ws.Cells[row, 4].Value = b.Genre;
                ws.Cells[row, 5].Value = b.Price;
                ws.Cells[row, 6].Value = b.PublishDate;
                ws.Cells[row, 7].Value = b.Description;
                row++;
            }

            package.SaveAs(new FileInfo(filePath));
        }
    }
}
