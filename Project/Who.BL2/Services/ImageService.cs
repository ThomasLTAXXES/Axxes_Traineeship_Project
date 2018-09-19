using System.Collections.Generic;
using System.Linq;
using Who.BL.Domain;
using Who.BL.IRepositories;
using Who.BL.IServices;
using Who.Data;

namespace Who.BL.Services
{
    public class ImageService : IImageService
    {
        private IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public IEnumerable<Person> GetPeople()
        {
            return _imageRepository.GetAll().Select(i => new Person { Name = i.Name, Url = i.Url });
        }

        public IEnumerable<Person> GetPeople(string name)
        {
            return _imageRepository.Search(name).Select(i => new Person { Name = i.Name, Url = i.Url });
        }
    }
}
