namespace ImportExportFile.Application.Dtos.Reports
{
    public class BookReportDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Author { get; set; } = default!;
        public double Price { get; set; }
        public string? GenreName { get; set; }

        // Tổng hợp từ Feedbacks
        public int FeedbackCount { get; set; }
        public double AverageRating { get; set; }

        // Tổng hợp từ Histories
        public int TotalViews { get; set; }
        public DateTime? LastViewedAt { get; set; }
    }
}
