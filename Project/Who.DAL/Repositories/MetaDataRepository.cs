using System.Linq;
using Who.BL.IRepositories;
using Who.DAL.Services;
using Who.Data;

namespace Who.DAL.Repositories
{
    public class MetaDataRepository : Repository<MetaDataEntity>, IMetaDataRepository
    {

        public MetaDataEntity Get(string name)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.MetaDataEntities.FirstOrDefault(m => m.Name == name);
            }
        }

    }
}
