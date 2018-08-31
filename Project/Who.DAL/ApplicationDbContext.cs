using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Who.Data;

namespace Who.DAL
{
    public class ApplicationDbContext : DbContext 
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Round> Rounds { get; set; }

        public ApplicationDbContext() : base("name=DefaultConnection")
        {
        }
        
    }
}
