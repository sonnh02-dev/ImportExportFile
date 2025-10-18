namespace ImportExportFile.Application.Abstractions.Files
{
    public interface IFileReader<T>
    {
        bool CanRead(string extension);
        List<T> Read(string filePath);
    }
}
