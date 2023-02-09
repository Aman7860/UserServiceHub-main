using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserServiceHub.Models
{
    [Table("Users",Schema ="dbo")]
    public class UserModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public long User_Id { get; set; }
        //public string User_Name { get; set; }
        //public string Password { get; set; }
        //public long Contact_No { get; set; }
        //public string Email_Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Passwords { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public bool IsApporved { get; set; }
        public bool Status { get; set; }
    }
}
