using System;
using System.Collections.Generic;
using System.Text;
using Who.BL.IServices;
using Who.Data;
using Who.DAL;

namespace Who.BL.Services
{
    public class UserService : IService<User>
    {
        public User Create(User model)
        {
            using (var context = new ApplicationDbContext())
            {
                throw new NotImplementedException();
            }
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public User Update(User model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
