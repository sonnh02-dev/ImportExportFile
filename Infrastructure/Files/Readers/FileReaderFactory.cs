using ImportExportFile.Application.Abstractions.Files;

namespace ImportExportFile.Infrastructure.Files.Readers
{
    public class FileReaderFactory<T>
    {
        private readonly IEnumerable<IFileReader<T>> _readers;

        public FileReaderFactory(IEnumerable<IFileReader<T>> readers)
        {
            _readers = readers;
        }

        public IFileReader<T> GetReader(string extension)
        {

            var reader = _readers.FirstOrDefault(r => r.CanRead(extension));
            if (reader == null)
                throw new NotSupportedException($"Unsupported file format: {extension}");
            return reader;
        }
    }
}
