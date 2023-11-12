using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RegLog.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}
