using Who.BL.IServices;
using Who.Data;

namespace Who.BL.IRepositories
{
    public interface IMetaDataRepository : IRepository<MetaDataEntity>
    {
        MetaDataEntity Get(string name);
    }
}
