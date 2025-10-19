using ImportExportFile.Domain.Entities;

namespace ImportExportFile.Domain.IRepositories
{


    public interface IBookRepository
    {
        Task AddRangeAsync(IEnumerable<Book> books);
        Task SaveChangesAsync();
         IQueryable<Book> GetBooksWithDetails();

    }


}
