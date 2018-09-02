using System.Data.Entity;
using Who.Data;

namespace Who.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<GameEntity> Games { get; set; }
        public DbSet<RoundEntity> Rounds { get; set; }

        public ApplicationDbContext() : base("name=DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Types().Configure(c =>
            {
                string tableName = c.ClrType.Name;
                tableName = tableName.Remove(tableName.LastIndexOf("Entity") + 1);
                c.ToTable(tableName);
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
