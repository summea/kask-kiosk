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
                allowableActions += "<h3>Applications</h3>\n" +
                                    "<ul>\n" +
                                    "<li><a href=\"/App/Index\">View All Applications</a></li>\n" +
                                    "</ul>\n" +
                                    "<h3>Jobs</h3>\n" +
                                    "<ul>\n" +
                                    "<li><a href=\"/Jobs/Index\">View Job Types</a></li>\n" +
                                    "<li><a href=\"/Jobs/Create\">Create New Job Type</a></li>\n" +
                                    "</ul>\n" +
                                    "<h3>Job Openings</h3>" +
                                    "<ul>\n" +
                                    "<li><a href=\"/JobOpenings/Index\">View Current Job Openings</a></li>\n" +
                                    "<li><a href=\"/JobOpenings/Create\">Request Job Opening</a></li>\n" +
                                    "</ul>\n" +
                                    "<h3>Multiple Choice Questions</h3>\n" +
                                    "<ul>\n" +
                                    "<li><a href=\"/MCQuestions/Index\">View All Multiple Choice Questions</a></li>\n" +
                                    "<li><a href=\"/MCQuestions/Create\">Create New Multiple Choice Questions</a></li>\n" +
                                    "<li><a href=\"/MCOptions/Index\">View All Multiple Choice Options</a></li>\n" +
                                    "<li><a href=\"/MCOptions/Create\">Create Multiple Choice Options</a></li>\n" +
                                    "</ul>\n" +
                                    "<h3>Short Answer Questions</h3>" +
                                    "<ul>\n" +
                                    "<li><a href=\"/SAQuestions/Index\">View All Short Answer Questions</a></li>\n" +
                                    "<li><a href=\"/SAQuestions/Create\">Create New Short Answer Questions</a></li>\n" +
                                    "<li><a href=\"/SAResponses/Index\">View All Short Answer Responses</a></li>\n" +
                                    "<li><a href=\"/SAResponses/Create\">Create Short Answer Responses</a></li>\n" +
                                    "</ul>\n" +
                                    "<h3>Skills</h3>" +
                                    "<ul>\n" +
                                    "<li><a href=\"/Skills/Index\">View All Skills</a></li>\n" +
                                    "<li><a href=\"/Skills/Create\">Create New Skill</li>\n" +
                                    "</ul>\n";
            }
            else if (this.User.IsInRole("StoreManager"))
            {
                allowableActions += "<h3>Job Openings</h3>" +
                                    "<ul>\n" +
                                    "<li><a href=\"/JobOpenings/Index\">View Current Job Openings</a></li>\n" +
                                    "</ul>\n";
            }
            else
            {
                allowableActions += "<h3>Applications</h3>" +
                                    "<ul>\n" +
                                    "<li><a href=\"/App/Status\">View Application Status</a></li>\n" +
                                    "</ul>\n";
            }
            ViewBag.allowableActions = allowableActions;
            return View();
        }
    }
}
