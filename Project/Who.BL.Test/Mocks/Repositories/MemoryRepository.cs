﻿using System.Collections.Generic;
using System.Linq;
using Who.BL.IRepositories;
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

        public void CreateAll(IEnumerable<T> models)
        {
            foreach (T model in models)
            {
                Create(model);
            }
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

        public Dictionary<int, T> GetAll(IEnumerable<int> ids)
        {
            return _entities.Where(x => ids.Contains(x.Id)).ToDictionary(x => x.Id, x => x);
        }

        public T Update(T model)
        {
            // Using own indexOf method as we are doing an id-compare (PK compare) here and don't know the implementation of the Equals-method of the object
            int indexOfModel = IndexOf(model.Id);
            if (indexOfModel != -1)
            {
                _entities[indexOfModel] = model;
            }
            return model;
        }

        private int IndexOf(int id)
        {
            for (int i = 0; i < _entities.Count; i++)
            {
                if (_entities[i].Id == id)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
