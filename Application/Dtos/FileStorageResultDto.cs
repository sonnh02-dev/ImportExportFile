namespace ImportExportFile.Application.Dtos
{
    public class FileStorageResultDto
    {
        public string FileName { get; set; } = default!;
        public string RelativePath { get; set; } = default!;
        public string AbsolutePath { get; set; } = default!;
    
        public string? Url { get; set; } // Nếu có base URL để build
    }

}
