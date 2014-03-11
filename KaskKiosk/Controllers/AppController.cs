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
                Task<string> response = httpClient.GetStringAsync(uriApplication);
                return JsonConvert.DeserializeObjectAsync<List<ApplicationDAO>>(response.Result).Result;
            }
        }
        
        private async Task<List<ApplicantDAO>> GetApplicantsAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                Task<string> response = httpClient.GetStringAsync(uriApplicant);
                return JsonConvert.DeserializeObjectAsync<List<ApplicantDAO>>(response.Result).Result;
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

        //
        // POST: /App/Create

        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

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


                    // get correct applicant id (have to wait until applicant is created in database)
                    var response = await httpClient.GetAsync(uriApplicant);
                    if(response.IsSuccessStatusCode)
                    {
                        IEnumerable<ApplicantDAO> foundApplicants = response.Content.ReadAsAsync<IEnumerable<ApplicantDAO>>().Result;
                        applicant.ApplicantID = foundApplicants.Last().ApplicantID;
                    }
                    else
                    {
                        throw new HttpException();
                    }

                    var resp = await httpClient.GetAsync(uriApplication);
                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<ApplicationDAO> foundApplications = resp.Content.ReadAsAsync<IEnumerable<ApplicationDAO>>().Result;
                        application.ApplicationID = foundApplications.Last().ApplicationID;
                    }
                    else
                    {
                        throw new HttpException();
                    }

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

        //
        // GET: /App/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /App/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /App/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /App/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
