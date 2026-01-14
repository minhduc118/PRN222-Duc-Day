using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_2.Model
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public bool Gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime Dob { get; set; }

        public ICollection<Book_Authors> BookAuthors { get; set; }
    }
}
