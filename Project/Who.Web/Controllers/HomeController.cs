using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Who.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (GetUserIdFromSessionStorage()==-1) {
                return View();
            }
            return RedirectToAction("index","game");
        }
    }
}