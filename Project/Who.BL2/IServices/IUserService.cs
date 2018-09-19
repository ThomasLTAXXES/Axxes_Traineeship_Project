using Who.Data;

namespace Who.BL.IServices
{
    public interface IUserService
    {
        int Register(string userName, string azureObjectIdentifier);

        int GetUser(string azureObjectIdentifier);
    }
}
