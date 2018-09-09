using System.Collections.Generic;
using System.Linq;
using Who.BL.IRepositories;
using Who.DAL.Services;
using Who.Data;

namespace Who.DAL.Repositories
{
    public class ImageRepository : Repository<ImageEntity>, IImageRepository
    {
        public IEnumerable<ImageEntity> Search(string name)
        {
            if (name == null)
            {
                return GetAll();
            }
            using (var context = new ApplicationDbContext())
            {
                return context.Images.Where(i => i.Name.Contains(name)).ToList();
            }
        }
    }
}
