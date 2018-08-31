using System;
using System.Collections.Generic;
using System.Text;
using Who.BL.IServices;
using Who.Data;
using Who.DAL;
using System.Linq;

namespace Who.DAL.Services
{
    public class UserService : IService<User>
    {
        public User Create(User model)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Users.Add(model);
                context.SaveChanges();
            }
            return model;
        }

        public User Get(int id)
        {
            using (var context = new ApplicationDbContext()) {
                return context.Users.FirstOrDefault(u => u.Id == id);
            }
        }

        public User Update(User model)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Users.Attach(model);
                context.SaveChanges();
            }
            return model;
        }

        public IEnumerable<User> GetAll()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Users.ToList();
            }
        }
    }
}
