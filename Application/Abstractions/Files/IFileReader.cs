namespace ImportExportFile.Application.Abstractions.Files
{
    public interface IFileReader<T>
    {
        bool CanRead(string extension);
        IEnumerable<T> Read(string filePath);
    }
}
