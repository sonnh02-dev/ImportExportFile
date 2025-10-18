namespace ImportExportFile.Application.Dtos.Responses
{
    public class ImportResult
    {
        public int TotalRecords { get; set; }
        public int SuccessCount { get; set; }
        public int FailCount => TotalRecords - SuccessCount;
        public List<string> Errors { get; set; } = new();
    }
}
