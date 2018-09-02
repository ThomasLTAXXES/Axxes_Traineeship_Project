using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Who.BL.IServices;
using Who.Data;

namespace Who.BL.Test.Mocks.Repositories
{
    public class MemoryRepository<T> : IRepository<T> where T : Entity
    {
        private List<T> _entities;

        public MemoryRepository()
        {
            _entities = new List<T>();
        }

        public T Create(T model)
        {
            _entities.Add(model);
            model.Id = _entities.Count;

            return model;
        }

        public T Get(int id)
        {
            return _entities.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            //TODO: this should be a deepclone rather than returning references (out of scope)
            return _entities;
        }

        public T Update(T model)
        {
            // Using own indexOf method as we are doing an id-compare (PK compare) here and don't know the implementation of the Equals-method of the object
            int indexOfModel = IndexOf(model.Id);  
            if(indexOfModel != -1)
            {
                _entities[indexOfModel] = model;
            }
            return model;
        }

        private int IndexOf(int id)
        {
            for(int i=0; i<_entities.Count; i++)
            {
                if(_entities[i].Id == id)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
