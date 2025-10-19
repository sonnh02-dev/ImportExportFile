namespace ImportExportFile.Application.Abstractions.Files
{
    public interface IFileWriter<T>
    {
        bool CanWrite(string extension);
        void Write(string filePath, IEnumerable<T> data);
    }
}
