using ImportExportFile.Application.Abstractions.Files;

namespace ImportExportFile.Infrastructure.Files.Storage
{
    public class LocalFileStorage : IFileStorage
    {
        private readonly string _uploadDirectory;

        public LocalFileStorage(IWebHostEnvironment env)
        {
            _uploadDirectory = Path.Combine(env.ContentRootPath, "Uploads");
            if (!Directory.Exists(_uploadDirectory))
                Directory.CreateDirectory(_uploadDirectory);
        }

        public async Task<string> SaveAsync(IFormFile file)
        {
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var path = Path.Combine(_uploadDirectory, fileName);
            await using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
            return path;
        }

        public void Delete(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }
    }

}
