using System.Xml.Serialization;
using ImportExportFile.Application.Abstractions.Files;
using ImportExportFile.Application.Dtos;
using ImportExportFile.Domain.Entities;

namespace ImportExportFile.Infrastructure.Files.Readers.Books
{
    public class XmlBooksFileReader : IFileReader<BookDto>
    {
        public bool CanRead(string extension) =>
            extension.Equals(".xml", StringComparison.OrdinalIgnoreCase);

        public IEnumerable<BookDto> Read(Stream stream)
        {
            if (stream == null || !stream.CanRead)
                throw new ArgumentException("Invalid stream.", nameof(stream));

            var serializer = new XmlSerializer(typeof(Catalog));

            if (serializer.Deserialize(stream) is Catalog catalog && catalog.Book != null)
                return catalog.Book;

            return Enumerable.Empty<BookDto>();
        }
    }

    // Giả định rằng class Catalog là lớp bao ngoài trong XML file
    [XmlRoot("Catalog")]
    public class Catalog
    {
        [XmlElement("Book")]
        public List<BookDto>? Book { get; set; }
    }


//  <Catalog>

//  <Book>
//    <Title>Clean Code</Title>
//    <Author>Robert C. Martin</Author>
//    <Price>35.99</Price>
//  </Book>

//  <Book>
//    <Title>The Pragmatic Programmer</Title>
//    <Author>Andrew Hunt</Author>
//    <Price>29.99</Price>
//  </Book>

//</Catalog>

}
