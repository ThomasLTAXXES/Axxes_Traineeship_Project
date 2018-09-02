using System.Web.Mvc;
using Who.BL.IServices;
using Who.Data;

namespace Who.Web.Controllers
{
    public class GameController : Controller
    {
        private IRepository<UserEntity> _userService;
        

        public GameController(IRepository<UserEntity>userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}