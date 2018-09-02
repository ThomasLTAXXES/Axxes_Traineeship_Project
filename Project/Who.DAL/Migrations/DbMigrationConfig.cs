using Who.Data;

namespace Who.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class DbMigrationConfig : DbMigrationsConfiguration<Who.DAL.ApplicationDbContext>
    {
        public DbMigrationConfig()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Who.DAL.ApplicationDbContext context)
        {
            var user = new UserEntity { FirstName = "Tester", LastName = "Demo" };
            context.Users.Add(user);
            base.Seed(context);
        }
    }
}
