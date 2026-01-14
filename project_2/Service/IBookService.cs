using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project_2.Model;

namespace project_2.Service
{
    public interface IBookService
    {
        List<Book> GetAllBooks();
        List<Book> GetBooksByAuthorId(int authorId);

        Book? GetBookById(int id);
    }
}
