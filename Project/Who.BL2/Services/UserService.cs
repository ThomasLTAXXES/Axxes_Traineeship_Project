using System.Linq;
using Who.BL.IRepositories;
using Who.BL.IServices;
using Who.Data;

namespace Who.BL.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int GetUser(string azureObjectIdentifier)
        {
            return _userRepository.GetIdByAzureObjectIdentifier(azureObjectIdentifier);
        }

        public int Register(string userName, string azureObjectIdentifier)
        {
            return _userRepository.Create(new UserEntity { FullName = userName, AzureObjectIdentifier = azureObjectIdentifier }).Id;
        }
    }
}
