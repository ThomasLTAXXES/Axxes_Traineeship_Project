using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Who.BL.IServices;
using Who.Data;

namespace Who.BL.IRepositories
{
    public interface IImageRepository : IRepository<ImageEntity>
    {
        IEnumerable<ImageEntity> Search(string name);
    }
}
