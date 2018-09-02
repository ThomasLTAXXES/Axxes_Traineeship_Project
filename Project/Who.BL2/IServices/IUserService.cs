using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Who.Data;

namespace Who.BL.IServices
{
    public interface IUserService
    {
        UserEntity Register(UserEntity user);

        bool Login(string userName, string password);

        void Logout(string userName);
    }
}
