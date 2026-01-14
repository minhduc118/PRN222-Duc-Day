using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace project_2.Model
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public int Year { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public ICollection<Book_Authors> BookAuthors { get; set; }
    }
}
