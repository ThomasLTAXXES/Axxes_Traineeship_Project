using System;
using System.Linq;
using Who.BL.IRepositories;
using Who.Data;
using Who.Data.Enums;

namespace Who.DAL.Repositories
{
    public class MetaDataRepository : Repository<MetaDataEntity>, IMetaDataRepository
    {
        public T GetByName<T>(string name)
        {
            using (var context = new ApplicationDbContext())
            {
                return (T)Convert.ChangeType(context.MetaDataEntities.FirstOrDefault(m => m.Name == name).Value, typeof(T));
            }
        }

        public T GetByName<T>(MetaDataEnum name)
        {
            return GetByName<T>(name.ToString());
        }
    }
}
