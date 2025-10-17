using ImportExportFile.Entities;
using OfficeOpenXml;
using System.ComponentModel;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace ImportExportService.Utils
{
    public static class ExcelHelper
    {
        static ExcelHelper()
        {
            ExcelPackage.License.SetNonCommercialPersonal("ImportExportService");
        }

        public static List<Book> ReadBooksFromExcel(string filePath)
        {
            var books = new List<Book>();

            using var package = new ExcelPackage(new FileInfo(filePath));
            var worksheet = package.Workbook.Worksheets[0];

            var rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++) // bỏ header
            {
                books.Add(new Book
                {
                    Id = worksheet.Cells[row, 1].Text,
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

        public static void WriteBooksToExcel(string filePath, List<Book> books)
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Books");

            worksheet.Cells[1, 1].Value = "Id";
            worksheet.Cells[1, 2].Value = "Author";
            worksheet.Cells[1, 3].Value = "Title";
            worksheet.Cells[1, 4].Value = "Genre";
            worksheet.Cells[1, 5].Value = "Price";
            worksheet.Cells[1, 6].Value = "PublishDate";
            worksheet.Cells[1, 7].Value = "Description";

            int row = 2;
            foreach (var book in books)
            {
                worksheet.Cells[row, 1].Value = book.Id;
                worksheet.Cells[row, 2].Value = book.Author;
                worksheet.Cells[row, 3].Value = book.Title;
                worksheet.Cells[row, 4].Value = book.Genre;
                worksheet.Cells[row, 5].Value = book.Price;
                worksheet.Cells[row, 6].Value = book.PublishDate;
                worksheet.Cells[row, 7].Value = book.Description;
                row++;
            }

            package.SaveAs(new FileInfo(filePath));
        }
    }
}
