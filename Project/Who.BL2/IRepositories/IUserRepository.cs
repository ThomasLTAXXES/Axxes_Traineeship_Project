using Who.BL.IServices;
using Who.Data;

namespace Who.BL.IRepositories
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        int GetIdByAzureObjectIdentifier(string azureObjectIdentifier);
    }
}
