using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCC.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DCC.Data
{
    public class DCCDbContext : DbContext
    {
        public DCCDbContext(DbContextOptions<DCCDbContext> options) : base(options)
        {
            
        }

        public DbSet<Instructor> Instructors { get; set; }
    }
}
