using System;
using System.Collections.Generic;
using System.Text;
using Who.BL.IServices;
using Who.Data;
using Who.DAL;
using System.Linq;
using System.Data.Entity;

namespace Who.DAL.Services
{
    public class Repository<T> : IRepository<T> where T: Entity
    {
        public T Create(T model)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Set<T>().Add(model);
                context.SaveChanges();
            }
            return model;
        }

        public void CreateAll(IEnumerable<T> models)
        {
            using (var context = new ApplicationDbContext())
            {
                foreach(T model in models)
                {
                    context.Set<T>().Add(model);
                }
                context.SaveChanges();
            }
        }

        public T Get(int id)
        {
            using (var context = new ApplicationDbContext()) {
                return context.Set<T>().FirstOrDefault(u => u.Id == id);
            }
        }

        public Dictionary<int, T> GetAll(IEnumerable<int> ids)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Set<T>().Where(u => ids.Contains(u.Id)).ToDictionary(u=>u.Id, u=>u);
            }
        }

        public T Update(T model)
        {
            using (var context = new ApplicationDbContext())
            {
                               context.Set<T>().Attach(model);
                context.Entry(model).State = EntityState.Modified;
                context.SaveChanges();
            }
            return model;
        }

        public IEnumerable<T> GetAll()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Set<T>().ToList();
            }
        }
    }
}
