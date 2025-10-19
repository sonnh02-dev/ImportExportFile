using AutoMapper;
using ImportExportFile.Application.Dtos;
using ImportExportFile.Application.Dtos.Reports;
using ImportExportFile.Domain.Entities;

namespace ImportExportFile.Application.Mappings
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookDto, Book>()
                .ForMember(dest => dest.GenreId, opt => opt.Ignore())
                .ForMember(dest => dest.Genre, opt => opt.Ignore());

            CreateMap<Book, BookReportDto>()
                .ForMember(dest => dest.GenreName,
                    opt => opt.MapFrom(src => src.Genre != null ? src.Genre.Name : null))
                .ForMember(dest => dest.FeedbackCount,
                    opt => opt.MapFrom(src => src.Feedbacks.Count))

                .ForMember(dest => dest.AverageRating,
                    opt => opt.MapFrom(src => src.Feedbacks.Any()
                        ? src.Feedbacks.Average(f => f.Rating)
                        : 0))

                .ForMember(dest => dest.TotalViews,
                    opt => opt.MapFrom(src => src.Histories.Sum(h => h.ViewCount)))

                .ForMember(dest => dest.LastViewedAt,
                    opt => opt.MapFrom(src => src.Histories
                        .OrderByDescending(h => h.LastViewedAt)
                        .Select(h => (DateTime?)h.LastViewedAt)
                        .FirstOrDefault()));
        }
    }
}
