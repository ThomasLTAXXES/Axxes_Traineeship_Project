using System;
using System.Collections.Generic;
using System.Linq;
using Who.BL.IRepositories;
using Who.DAL.Services;
using Who.Data;
using System.Data.Entity;

namespace Who.DAL.Repositories
{
    public class ImageRepository : Repository<ImageEntity>, IImageRepository
    {
        private Random _random = new Random();
        
        public IEnumerable<ImageEntity> GetRandomImages(int amount)
        {
            using (var context = new ApplicationDbContext())
            {
               return context.Images.OrderBy(r => Guid.NewGuid()).Take(amount).ToList();
            }
        }

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
