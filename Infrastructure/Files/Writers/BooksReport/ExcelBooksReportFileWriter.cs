using ImportExportFile.Application.Abstractions.Files;
using ImportExportFile.Application.Dtos.Reports;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ImportExportFile.Infrastructure.Files.Writers.BooksReport
{
    public class ExcelBooksReportFileWriter : IFileWriter<BookReportDto>
    {
        public ExcelBooksReportFileWriter()
        {
            // Bắt buộc khi dùng EPPlus trong môi trường phi thương mại
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public bool CanWrite(string extension) =>
            extension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase);

        public void Write(string filePath, IEnumerable<BookReportDto> books)
        {
            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("Book Report");

            //Headers
            var headers = new[]
            {
                "Id", "Title", "Author", "Genre", "Price",
                "Feedback Count", "Average Rating",
                "Total Views", "Last Viewed At"
            };

            for (int i = 0; i < headers.Length; i++)
            {
                ws.Cells[1, i + 1].Value = headers[i];
                ws.Cells[1, i + 1].Style.Font.Bold = true;
                ws.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                ws.Cells[1, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            //Data Rows
            int row = 2;
            foreach (var b in books)
            {
                ws.Cells[row, 1].Value = b.Id.ToString();
                ws.Cells[row, 2].Value = b.Title;
                ws.Cells[row, 3].Value = b.Author;
                ws.Cells[row, 4].Value = b.GenreName;
                ws.Cells[row, 5].Value = b.Price;
                ws.Cells[row, 6].Value = b.FeedbackCount;
                ws.Cells[row, 7].Value = b.AverageRating;
                ws.Cells[row, 8].Value = b.TotalViews;
                ws.Cells[row, 9].Value = b.LastViewedAt?.ToString("yyyy-MM-dd HH:mm:ss");
                row++;
            }

            //Formatting the worksheet 
            ws.Cells.AutoFitColumns();
            ws.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            ws.View.FreezePanes(2, 1); // Giữ cố định header

            package.SaveAs(new FileInfo(filePath));
        }
    }
}
