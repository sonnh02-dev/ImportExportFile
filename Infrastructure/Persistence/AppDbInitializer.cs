using ImportExportFile.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ImportExportFile.Infrastructure.Persistence
{
    public static class AppDbInitializer
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            
            if (!context.Genres.Any())
            {
                var genres = new[]
                {
                   new Genre { Id = Guid.NewGuid(), Name = "Fiction", Description = "Fictional books" },
                   new Genre { Id = Guid.NewGuid(), Name = "Science", Description = "Scientific works" },
                   new Genre { Id = Guid.NewGuid(), Name = "History", Description = "Historical records" },
                   new Genre { Id = Guid.NewGuid(), Name = "Technology", Description = "Tech books" },
                   new Genre { Id = Guid.NewGuid(), Name = "Philosophy", Description = "Philosophical works" },
                };
                await context.Genres.AddRangeAsync(genres);
                await context.SaveChangesAsync();
            }

            if (!context.Users.Any())
            {
                var users = new[]
                {
                    new User { Id = Guid.NewGuid(), UserName = "alice", Email = "alice@example.com", CreatedAt = DateTime.UtcNow },
                    new User { Id = Guid.NewGuid(), UserName = "bob", Email = "bob@example.com", CreatedAt = DateTime.UtcNow },
                    new User { Id = Guid.NewGuid(), UserName = "charlie", Email = "charlie@example.com", CreatedAt = DateTime.UtcNow },
                    new User { Id = Guid.NewGuid(), UserName = "david", Email = "david@example.com", CreatedAt = DateTime.UtcNow },
                    new User { Id = Guid.NewGuid(), UserName = "eve", Email = "eve@example.com", CreatedAt = DateTime.UtcNow },
                };
                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();
            }

            if (!context.Books.Any())
            {
                var genres = await context.Genres.ToListAsync();

                var books = new[]
                  {
                    new Book { Id = Guid.NewGuid(), Title = "Book 1", Author = "Author A", GenreId = genres[0].Id, Price = 9.99, PublishDate = DateTime.UtcNow.AddYears(-1), Description = "Sample book 1" },
                    new Book { Id = Guid.NewGuid(), Title = "Book 2", Author = "Author B", GenreId = genres[1].Id, Price = 14.99, PublishDate = DateTime.UtcNow.AddYears(-2), Description = "Sample book 2" },
                    new Book { Id = Guid.NewGuid(), Title = "Book 3", Author = "Author C", GenreId = genres[2].Id, Price = 19.99, PublishDate = DateTime.UtcNow.AddYears(-3), Description = "Sample book 3" },
                    new Book { Id = Guid.NewGuid(), Title = "Book 4", Author = "Author D", GenreId = genres[3].Id, Price = 24.99, PublishDate = DateTime.UtcNow.AddYears(-4), Description = "Sample book 4" },
                    new Book { Id = Guid.NewGuid(), Title = "Book 5", Author = "Author E", GenreId = genres[4].Id, Price = 29.99, PublishDate = DateTime.UtcNow.AddYears(-5), Description = "Sample book 5" },
                };
                await context.Books.AddRangeAsync(books);
                await context.SaveChangesAsync();
            }

            if (!context.Feedbacks.Any())
            {
                var users = await context.Users.ToListAsync();
                var books = await context.Books.ToListAsync();

                var feedbacks = new[]
                {
                    new Feedback { UserId = users[0].Id, BookId = books[0].Id, Rating = 5, Content = "Great book!", CreatedAt = DateTime.UtcNow },
                    new Feedback { UserId = users[1].Id, BookId = books[1].Id, Rating = 4, Content = "Nice read", CreatedAt = DateTime.UtcNow },
                    new Feedback { UserId = users[2].Id, BookId = books[2].Id, Rating = 3, Content = "Average", CreatedAt = DateTime.UtcNow },
                    new Feedback { UserId = users[3].Id, BookId = books[3].Id, Rating = 5, Content = "Loved it!", CreatedAt = DateTime.UtcNow },
                    new Feedback { UserId = users[4].Id, BookId = books[4].Id, Rating = 4, Content = "Good!", CreatedAt = DateTime.UtcNow },
                };
                await context.Feedbacks.AddRangeAsync(feedbacks);
                await context.SaveChangesAsync();
            }

            if (!context.Histories.Any())
            {
                var users = await context.Users.ToListAsync();
                var books = await context.Books.ToListAsync();

                var histories = new[]
                {
                    new History { UserId = users[0].Id, BookId = books[0].Id, ViewCount = 3, LastViewedAt = DateTime.UtcNow },
                    new History { UserId = users[1].Id, BookId = books[1].Id, ViewCount = 1, LastViewedAt = DateTime.UtcNow },
                    new History { UserId = users[2].Id, BookId = books[2].Id, ViewCount = 2, LastViewedAt = DateTime.UtcNow },
                    new History { UserId = users[3].Id, BookId = books[3].Id, ViewCount = 5, LastViewedAt = DateTime.UtcNow },
                    new History { UserId = users[4].Id, BookId = books[4].Id, ViewCount = 4, LastViewedAt = DateTime.UtcNow },
                };
                await context.Histories.AddRangeAsync(histories);
                await context.SaveChangesAsync();
            }
        }
    }
}
