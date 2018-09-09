using System.Linq;
using Who.BL.IRepositories;
using Who.Data;

namespace Who.DAL.Repositories
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        public int GetIdByAzureObjectIdentifier(string azureObjectIdentifier)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Users.FirstOrDefault(x => azureObjectIdentifier.Equals(x.AzureObjectIdentifier))?.Id ?? -1;
            }
        }
    }
}
