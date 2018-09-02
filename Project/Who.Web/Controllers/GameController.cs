using System.Web.Mvc;
using Who.BL.IServices;
using Who.Data;

namespace Who.Web.Controllers
{
    public class GameController : Controller
    {
        private IRepository<User> _userService;
        

        public GameController(IRepository<User>userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}