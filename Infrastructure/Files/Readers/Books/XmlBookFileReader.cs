using System.Xml.Serialization;
using ImportExportFile.Application.Abstractions.Files;
using ImportExportFile.Application.Dtos;

namespace ImportExportFile.Infrastructure.Files.Readers.Books
{
    public class XmlBookFileReader : IFileReader<BookDto>
    {
        public bool CanRead(string extension) =>
            extension.Equals(".xml", StringComparison.OrdinalIgnoreCase);

        public List<BookDto> Read(string filePath)
        {
            using var stream = File.Open(filePath, FileMode.Open);
            var serializer = new XmlSerializer(typeof(Catalog));
            var catalog = (Catalog)serializer.Deserialize(stream);
            return catalog.Book ?? new List<BookDto>();
        }

        [XmlRoot("Catalog")]
        public class Catalog
        {
            [XmlElement("Book")]
            public List<BookDto> Book { get; set; } = new();
        }
    }
}
