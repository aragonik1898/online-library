using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Library.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookLoan> BookLoans { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Настройка связей для Book
            builder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Book>()
                .HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreId)
                .OnDelete(DeleteBehavior.Restrict);


            // Настройка связей для BookLoan
            builder.Entity<BookLoan>()
                .HasOne(l => l.Book)
                .WithMany(b => b.Loans)
                .HasForeignKey(l => l.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<BookLoan>()
                .HasOne(l => l.User)
                .WithMany(u => u.Loans)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            // Настройка связей для Bookmark
            builder.Entity<Bookmark>()
                .HasOne(bm => bm.Book)
                .WithMany()
                .HasForeignKey(bm => bm.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Bookmark>()
                .HasOne(bm => bm.User)
                .WithMany()
                .HasForeignKey(bm => bm.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Настройка индексов
            builder.Entity<Book>()
                .HasIndex(b => b.ISBN)
                .IsUnique();

            builder.Entity<Book>()
                .HasIndex(b => b.Title);

            builder.Entity<Author>()
                .HasIndex(a => new { a.FirstName, a.LastName });

            // Настройка ограничений

            builder.Entity<BookLoan>()
                .ToTable(t => t.HasCheckConstraint("CK_BookLoan_DueDate", "DueDate > LoanDate"));
        }
    }
}
