using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project_2.Model;

namespace project_2.Service
{
    public interface IAuthorService
    {
        List<Author> GetAllAuthors();
        Author? GetAuthorById(int id);
    }
}
