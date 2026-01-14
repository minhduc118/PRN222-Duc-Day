using System;
using Microsoft.EntityFrameworkCore;

namespace project_2.Model
{
    public class AppDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Book_Authors> BookAuthors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=BookStoreDB;Trusted_Connection=True;TrustServerCertificate=True;"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình Composite Primary Key cho Book_Authors
            modelBuilder.Entity<Book_Authors>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });

            // Cấu hình quan hệ
            modelBuilder.Entity<Book_Authors>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookId);

            modelBuilder.Entity<Book_Authors>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);
        }
    }
}
