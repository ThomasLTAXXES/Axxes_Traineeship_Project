using Who.Data;

namespace Who.BL.IServices
{
    public interface IUserService
    {
        int Register(string userName, string tenantId);

        int GetUser(string tenantId);
    }
}
