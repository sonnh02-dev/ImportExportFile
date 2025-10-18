using ImportExportFile.Application.Abstractions.Files;

namespace ImportExportFile.Infrastructure.Files.Writers
{
    public class FileWriterFactory<T>
    {
        private readonly IEnumerable<IFileWriter<T>> _writers;

        public FileWriterFactory(IEnumerable<IFileWriter<T>> writers)
        {
            _writers = writers;
        }

        public IFileWriter<T> GetWriter(string extension)
        {
            var writer = _writers.FirstOrDefault(w => w.CanWrite(extension));
            if (writer == null)
                throw new NotSupportedException($"Unsupported file format: {extension}");
            return writer;
        }
    }
}
