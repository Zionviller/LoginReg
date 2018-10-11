using System;
using Microsoft.EntityFrameworkCore;

namespace LoginReg.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options):base(options)
        {
        }
    }
}
