using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserServiceHub.Models
{
    [Table("Books",Schema ="dbo")]
    public class BooksModel
    {
        [Key]        
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string Description { get; set; }

    }
}
