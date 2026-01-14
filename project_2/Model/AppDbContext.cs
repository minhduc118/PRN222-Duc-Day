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

            modelBuilder.Entity<Author>().HasData(
               new Author { Id = 1, Name = "Nguyễn Nhật Ánh", Gender = true, Dob = new DateTime(1955, 5, 7) },
               new Author { Id = 2, Name = "Tô Hoài", Gender = true, Dob = new DateTime(1920, 9, 27) },
               new Author { Id = 3, Name = "Nam Cao", Gender = true, Dob = new DateTime(1915, 10, 29) }
           );
            // Seed Books
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Mắt Biếc", Year = 1990, Description = "Tiểu thuyết tình cảm" },
                new Book { Id = 2, Title = "Dế Mèn Phiêu Lưu Ký", Year = 1941, Description = "Truyện thiếu nhi" },
                new Book { Id = 3, Title = "Chí Phèo", Year = 1941, Description = "Truyện ngắn" },
                new Book { Id = 4, Title = "Tôi Thấy Hoa Vàng Trên Cỏ Xanh", Year = 2010, Description = "Tiểu thuyết" }
            );
            // Seed Book_Authors (liên kết Author - Book)
            modelBuilder.Entity<Book_Authors>().HasData(
                new Book_Authors { BookId = 1, AuthorId = 1 },  // Mắt Biếc - Nguyễn Nhật Ánh
                new Book_Authors { BookId = 4, AuthorId = 1 },  // Tôi Thấy Hoa Vàng - Nguyễn Nhật Ánh
                new Book_Authors { BookId = 2, AuthorId = 2 },  // Dế Mèn - Tô Hoài
                new Book_Authors { BookId = 3, AuthorId = 3 }   // Chí Phèo - Nam Cao
            );
        }

      
    }
}
