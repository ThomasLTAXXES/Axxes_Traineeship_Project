using System.Linq;
using System.Web.Mvc;
using Who.BL.IServices;
using Who.Web.Models;

namespace Who.Web.Controllers
{
    public class ImageController : BaseController
    {
        private IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        // GET: Person
        public ActionResult GetAllPersons()
        {
            return View(_imageService
                .GetPeople()
                .Select(p => new ImagePersonViewModel
                {
                    Name = p.Name,
                    Url = p.Url
                }).ToList());
        }

        public ActionResult SearchPersons(SearchPersonViewModel searchPersonViewModel)
        {
            return View("GetAllPersons", _imageService
                .GetPeople(searchPersonViewModel.Name)
                .Select(p => new ImagePersonViewModel
                {
                    Name = p.Name,
                    Url = p.Url
                }).ToList());
        }
    }
}