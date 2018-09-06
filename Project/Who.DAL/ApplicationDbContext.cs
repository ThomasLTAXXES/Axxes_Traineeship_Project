using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Who.Data;

namespace Who.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<GameEntity> Games { get; set; }
        public DbSet<RoundEntity> Rounds { get; set; }
        public DbSet<ImageEntity> Images { get; set; }
        public DbSet<ImageInRoundEntity> ImagesInRound { get; set; }
        public DbSet<MetaDataEntity> MetaDataEntities { get; set; }

        public ApplicationDbContext() : base("name=DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Types().Configure(c =>
            {
                string tableName = c.ClrType.Name;
                tableName = tableName.Remove(tableName.LastIndexOf("Entity"));
                c.ToTable(tableName);
            });
            
            base.OnModelCreating(modelBuilder);
        }

    }
}
