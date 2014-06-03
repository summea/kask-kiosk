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

        [Authorize(Roles = "Administrator, Applicant, HiringManager, StoreManager")]
        public ActionResult Backend()
        {
            string allowableActions = "";
            if (this.User.IsInRole("HiringManager"))
            {
                allowableActions += "<a href=\"/App/Index\">All Applications</a><br>" +
                                    "<a href=\"/Jobs/Index\">View Job Types</a> | " +
                                    "<a href=\"/Jobs/Create\">Create New Job Type</a> | " +
                                    "<a href=\"/JobOpenings/Index\">View Current Job Openings</a> | " +
                                    "<a href=\"/JobOpenings/Create\">Request Job Opening</a><br>" + 
                                    "<a href=\"/MCQuestions/Index\">View All Multiple Choice Questions</a> | " +
                                    "<a href=\"/MCQuestions/Create\">Create New Multiple Choice Questions</a><br>" +
                                    "<a href=\"/MCOptions/Index\">View All Multiple Choice Options</a> | " +
                                    "<a href=\"/MCOptions/Create\">Create Multiple Choice Options</a><br>" + 
                                    "<a href=\"/SAQuestions/Index\">View All Short Answer Questions</a> | " +
                                    "<a href=\"/SAQuestions/Create\">Create New Short Answer Questions</a><br>" +
                                    "<a href=\"/SAResponses/Index\">View All Short Answer Responses</a> | " +
                                    "<a href=\"/SAResponses/Create\">Create Short Answer Responses</a><br>" +
                                    "<a href=\"/Skills/Index\">View All Skills</a> | " +
                                    "<a href=\"/Skills/Create\">Create New Skill</a>";
            }
            else if (this.User.IsInRole("StoreManager"))
            {
                allowableActions += "<a href=\"/Jobs/Index\">View Job Types</a> | " +
                                    "<a href=\"/Jobs/Create\">Create New Job Type</a> | " +
                                    "<a href=\"/JobOpenings/Index\">View Current Job Openings</a> | ";
            }
            else
            {
                allowableActions += "<a href=\"/App/Status\">View Application Status</a>";
            }
            ViewBag.allowableActions = allowableActions;
            return View();
        }
    }
}
