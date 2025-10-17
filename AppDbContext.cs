using BookRecommendationSystem.Domain.Entities;
using ImportExportFile.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ImportExportFile.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<UserRating> UserRatings => Set<UserRating>();
        public DbSet<BookSimilarity> BookSimilarities => Set<BookSimilarity>();
        public DbSet<UserBookHistory> UserBookHistories => Set<UserBookHistory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // BookSimilarity has composite key
            modelBuilder.Entity<BookSimilarity>()
                .HasKey(bs => new { bs.BookId, bs.SimilarBookId });

            // Relationships
            modelBuilder.Entity<BookSimilarity>()
                .HasOne(bs => bs.Book)
                .WithMany(b => b.SimilarBooks)
                .HasForeignKey(bs => bs.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRating>()
                .HasOne(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<UserRating>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Ratings)
                .HasForeignKey(r => r.BookId);

            modelBuilder.Entity<UserBookHistory>()
                .HasOne(h => h.User)
                .WithMany(u => u.BookHistories)
                .HasForeignKey(h => h.UserId);

            modelBuilder.Entity<UserBookHistory>()
                .HasOne(h => h.Book)
                .WithMany(b => b.BookHistories)
                .HasForeignKey(h => h.BookId);
        }
    }
}
