using ImportExportFile.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ImportExportFile.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<Feedback> Feedbacks => Set<Feedback>();
        public DbSet<History> Histories => Set<History>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===== User =====
            modelBuilder.Entity<User>(builder =>
            {
                builder.Property(u => u.UserName)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.HasIndex(u => u.Email).IsUnique();
            });

            // ===== Book =====
            modelBuilder.Entity<Book>(builder =>
            {
                builder.HasOne(b => b.Genre)
                    .WithMany(g => g.Books)
                    .HasForeignKey(b => b.GenreId);

                builder.HasIndex(b => b.GenreId);

                builder.Property(b => b.Title)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            // ===== Genre =====
            modelBuilder.Entity<Genre>(builder =>
            {
                builder.Property(g => g.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.HasIndex(g => g.Name).IsUnique();
            });

            // ===== Feedback =====
            modelBuilder.Entity<Feedback>(builder =>
            {
                builder.HasKey(f => new { f.UserId, f.BookId });

                builder.HasOne(f => f.User)
                    .WithMany(u => u.Feedbacks)
                    .HasForeignKey(f => f.UserId);

                builder.HasOne(f => f.Book)
                    .WithMany(b => b.Feedbacks)
                    .HasForeignKey(f => f.BookId);
            });

            // ===== History =====
            modelBuilder.Entity<History>(builder =>
            {
                builder.HasKey(f => new { f.UserId, f.BookId });

                builder.HasOne(h => h.User)
                    .WithMany(u => u.Histories)
                    .HasForeignKey(h => h.UserId);

                builder.HasOne(h => h.Book)
                    .WithMany(b => b.Histories)
                    .HasForeignKey(h => h.BookId);
            });
          
        }
    }
}
