using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using project_2.Model;

namespace project_2.Service.Impl
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;

        public BookService() { 
           _context = new AppDbContext();
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public Book? GetBookById(int id)
        {
            return _context.Books.FirstOrDefault(b => b.Id == id);
        }

        public List<Book> GetBooksByAuthorId(int authorId)
        {
            return _context.BookAuthors.Where(ba => ba.AuthorId == authorId)
                .Include(ba => ba.Book)
                .Select(ba => ba.Book)
                .ToList();
        }
    }
}
