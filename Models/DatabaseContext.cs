using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserServiceHub.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<UserModel> UserModel { get; set; }
        public DbSet<EmployeemasterDMO> Usp_Login { get; set; }
        public DbSet<BooksModel> Books { get; set; }

        // public DbSet<EmployeeDMO> EmployeeDMO { get; set; }

    }
}
