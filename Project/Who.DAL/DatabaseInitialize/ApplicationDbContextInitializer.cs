using System.Data.Entity;
using Who.Data;

namespace Who.DAL.DatabaseInitialize
{
    public class ApplicationDbContextInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            context.Users.Add(new UserEntity
            {
                FirstName = "Tester",
                LastName = "Demo"
            });
        }
    }
}
