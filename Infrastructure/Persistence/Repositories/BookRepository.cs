using ImportExportFile.Application.Dtos;
using ImportExportFile.Domain.Entities;
using ImportExportFile.Domain.IRepositories;
using ImportExportFile.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace ImportExportFile.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<Book> GetBooksWithDetails()
        {
            return _context.Books
                .Include(b => b.Genre)
                .Include(b => b.Feedbacks)
                 .Include(b => b.Histories);
        }

        public async Task AddRangeAsync(IEnumerable<Book> books)
        {
            await _context.Books.AddRangeAsync(books);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
