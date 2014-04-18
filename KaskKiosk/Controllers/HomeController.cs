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

        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public ActionResult Backend()
        {
            string allowableActions = "";
            if (this.User.IsInRole("HiringManager"))
            {
                allowableActions += "<a href=\"/Jobs/Index\">View Job Types</a> | " + 
                                    "<a href=\"/Jobs/Create\">Create New Job Type</a> | " +
                                    "<a href=\"/JobOpenings/Index\">View Current Job Openings</a> | " +
                                    "<a href=\"/JobOpenings/Create\">Request Job Opening</a>";
            }
            else if (this.User.IsInRole("StoreManager"))
            {
                allowableActions += "<a href=\"/Jobs/Index\">View Job Types</a> | " +
                                    "<a href=\"/Jobs/Create\">Create New Job Type</a> | " + 
                                    "<a href=\"/JobOpenings/Index\">View Current Job Openings</a> | ";
            }
            ViewBag.allowableActions = allowableActions;
            return View();
        }
    }
}
