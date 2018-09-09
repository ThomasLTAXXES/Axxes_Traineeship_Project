using System;
using System.Collections.Generic;
using System.Text;

namespace Who.BL.IRepositories
{
    public interface IRepository<T>
    {
        T Create(T model);
        void CreateAll(IEnumerable<T> models);
        T Get(int id);
        Dictionary<int, T> GetAll(IEnumerable<int> ids);
        T Update(T model);
        IEnumerable<T> GetAll();
    }
}
