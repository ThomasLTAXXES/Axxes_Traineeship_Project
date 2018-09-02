﻿using System;
using System.Collections.Generic;
using System.Text;
using Who.BL.IServices;
using Who.Data;
using Who.DAL;
using System.Linq;

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

        public T Get(int id)
        {
            using (var context = new ApplicationDbContext()) {
                return context.Set<T>().FirstOrDefault(u => u.Id == id);
            }
        }

        public T Update(T model)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Set<T>().Attach(model);
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