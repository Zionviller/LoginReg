using System;
using Microsoft.EntityFrameworkCore;

namespace LoginReg.Models
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }

        public Context(DbContextOptions<Context> options):base(options)
        {
        }
    }
}
