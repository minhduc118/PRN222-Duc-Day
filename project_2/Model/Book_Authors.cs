using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_2.Model
{
    public class Book_Authors
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }

        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
    }
}
