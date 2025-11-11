namespace ImportExportFile.Application.Abstractions.Files
{
    public interface IFileStorage
    {
        Task< string> SaveAsync(IFormFile file, string? folder = null);
    }

}
