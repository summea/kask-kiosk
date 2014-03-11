using KaskKiosk.AESApplicationServiceRef;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KaskKiosk.Controllers
{
    public class AppController : Controller
    {
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        /// TODO: MAKE THIS INTO A WRAPPER
        /// THIS WILL ALLOW MULTI THREADING AS WELL
        readonly string uriApplication = "http://localhost:51309/api/Application";
        readonly string uriApplicant = "http://localhost:51309/api/Applicant";
        readonly string uriApplied = "http://localhost:51309/api/Applied";

        private async Task<List<ApplicationDAO>> GetApplicationsAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(uriApplication);
                return JsonConvert.DeserializeObjectAsync<List<ApplicationDAO>>(response).Result;
            }
        }
        
        private async Task<List<ApplicantDAO>> GetApplicantsAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(uriApplicant);
                return JsonConvert.DeserializeObjectAsync<List<ApplicantDAO>>(response).Result;
            }
        }

        public async Task<ActionResult> Index()
        {
            var apps = await GetApplicationsAsync();
            ViewBag.applications = apps;
            return View();
        }

        public ActionResult Create()
        {
            List<String> timePickerList = new List<String>();

            // create time picker range (in hours)
            for (int i = 0; i < 24; i++)
            {
                timePickerList.Add(i.ToString() + ":00");
            }

            ViewBag.timePickerList = timePickerList;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                // save application form data back to database through service
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("http://localhost:51309");
                    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage result = new HttpResponseMessage();
                    string resultContent = "";

                    // gather Applicant form data
                    ApplicantDAO applicant = new ApplicantDAO();
                    applicant.FirstName = Request.Form["FirstName"];
                    applicant.MiddleName = Request.Form["MiddleName"];
                    applicant.LastName = Request.Form["LastName"];
                    applicant.SSN = Request.Form["SSN"];
                    applicant.ApplicantAddress = Request.Form["ApplicantAddress"];
                    applicant.Phone = Request.Form["ApplicantPhone"];
                    applicant.NameAlias = Request.Form["NameAlias"];

                    // post (save) applicant data
                    result = httpClient.PostAsJsonAsync(uriApplicant, applicant).Result;
                    resultContent = result.Content.ReadAsStringAsync().Result;

                    // gather Application form data
                    ApplicationDAO application = new ApplicationDAO();
                    application.ApplicationStatus = "Submitted";
                    application.SalaryExpectation = Request.Form["SalaryExpectation"];
                    application.FullTime = Convert.ToByte(Request.Form["FullTime"]);
                    application.AvailableForDays = Convert.ToByte(Request.Form["AvailableForDays"]);
                    application.AvailableForEvenings = Convert.ToByte(Request.Form["AvailableForEvenings"]);
                    application.AvailableForWeekends = Convert.ToByte(Request.Form["AvailableForWeekends"]);
                    application.MondayFrom = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["MondayFrom"]));
                    application.TuesdayFrom = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["TuesdayFrom"]));
                    application.WednesdayFrom = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["WednesdayFrom"]));
                    application.ThursdayFrom = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["ThursdayFrom"]));
                    application.FridayFrom = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["FridayFrom"]));
                    application.SaturdayFrom = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["SaturdayFrom"]));
                    application.SundayFrom = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["SundayFrom"]));
                    application.MondayTo = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["MondayTo"]));
                    application.TuesdayTo = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["TuesdayTo"]));
                    application.WednesdayTo = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["WednesdayTo"]));
                    application.ThursdayTo = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["ThursdayTo"]));
                    application.FridayTo = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["FridayTo"]));
                    application.SaturdayTo = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["SaturdayTo"]));
                    application.SundayTo = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["SundayTo"]));

                    // post (save) application data
                    result = httpClient.PostAsJsonAsync(uriApplication, application).Result;
                    resultContent = result.Content.ReadAsStringAsync().Result;

                    // get correct applicant id
                    var applicants = await GetApplicantsAsync();
                    applicant.ApplicantID = applicants.Last().ApplicantID;

                    // get correct application id
                    var applications = await GetApplicationsAsync();
                    application.ApplicationID = applications.Last().ApplicationID;

                    // Create Applied DAO;
                    AppliedDAO applied = new AppliedDAO();
                    applied.ApplicantID = applicant.ApplicantID;
                    applied.ApplicationID = application.ApplicationID;
                    applied.JobID = 1;
                    applied.DateApplied = DateTime.Now;

                    // post (save) applied data
                    result = httpClient.PostAsJsonAsync(uriApplied, applied).Result;
                    resultContent = result.Content.ReadAsStringAsync().Result;
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
