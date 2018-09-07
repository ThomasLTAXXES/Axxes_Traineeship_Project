using System.Linq;
using Who.BL.IServices;
using Who.Data;

namespace Who.BL.Services
{
    public class UserService : IUserService
    {
        private IRepository<UserEntity> _userRepository;

        public UserService(IRepository<UserEntity> userRepository)
        {
            _userRepository = userRepository;
        }

        public int GetUser(string tenantId)
        {
            return _userRepository.GetAll().FirstOrDefault(x => tenantId.Equals(x.TenantId))?.Id ?? -1;
        }

        public int Register(string userName, string tenantId)
        {
            return _userRepository.Create(new UserEntity { FullName = userName, TenantId = tenantId }).Id;
        }
    }
}
