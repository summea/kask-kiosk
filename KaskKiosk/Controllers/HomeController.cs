using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KaskKiosk.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/Welcome

        [AllowAnonymous]
        public ActionResult Welcome()
        {
            return View();
        }

        //
        // GET: /Home/Backend

        [Authorize(Roles = "Administrator")]
        public ActionResult Backend()
        {
            return View();
        }
    }
}
