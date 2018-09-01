using System;
using System.Collections.Generic;
using System.Text;

namespace Who.BL.IServices
{
    public interface IRepository<T>
    {
        T Create(T model);
        T Get(int id);
        T Update(T model);
        IEnumerable<T> GetAll();
    }
}
