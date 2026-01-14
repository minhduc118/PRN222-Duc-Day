using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project_2.Model;

namespace project_2.Service.Impl
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext _context;

        public AuthorService() {
            _context = new AppDbContext();
        }
        public List<Author> GetAllAuthors()
        {
            return _context.Authors.ToList();
        }

        public Author? GetAuthorById(int id)
        {
            return _context.Authors.FirstOrDefault(a => a.Id == id);
        }
    }
}
