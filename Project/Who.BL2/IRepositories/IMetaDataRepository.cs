using Who.BL.IServices;
using Who.Data;
using Who.Data.Enums;

namespace Who.BL.IRepositories
{
    public interface IMetaDataRepository : IRepository<MetaDataEntity>
    {
        T GetByName<T>(string name);
        T GetByName<T>(MetaDataEnum name);
    }
}
