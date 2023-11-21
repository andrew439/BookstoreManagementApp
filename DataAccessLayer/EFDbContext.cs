using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DataObjects;

namespace DataAccessLayer
{
    public class EFDbContext : DbContext
    {
        public DbSet<BookVM> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
