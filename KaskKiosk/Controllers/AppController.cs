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

        private async Task<List<Application>> GetApplicationsAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                Task<string> response = httpClient.GetStringAsync(uriApplication);
                return JsonConvert.DeserializeObjectAsync<List<Application>>(response.Result).Result;
            }
        }
        // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

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
                timePickerList.Add(i.ToString());
            }

            ViewBag.timePickerList = timePickerList;
            return View();
        }

        //
        // POST: /App/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
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
                    Applicant applicant = new Applicant();
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
                    Application application = new Application();
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
                    Task<string> foundApplicantsresponse = httpClient.GetStringAsync(uriApplicant);
                    List<Applicant> foundApplicants = JsonConvert.DeserializeObjectAsync<List<Applicant>>(foundApplicantsresponse.Result).Result;

                    foreach (Applicant foundApplicant in foundApplicants)
                    {
                        // look for current applicant (the one we just inserted earlier...)
                        if ((foundApplicant.FirstName == applicant.FirstName) && (foundApplicant.LastName == applicant.LastName) && (foundApplicant.SSN == applicant.SSN))
                            applicant.Applicant_ID = foundApplicant.Applicant_ID;
                    }

                    // get correct application id (have to wait until application is created in database)
                    Task<string> foundApplicationsResponse = httpClient.GetStringAsync(uriApplication);
                    List<Application> foundApplications = JsonConvert.DeserializeObjectAsync<List<Application>>(foundApplicationsResponse.Result).Result;



                    foreach (Application foundApplication in foundApplications)
                    {
                        // look for current application (the one we just inserted earlier...)

                        // TODO: this is painful
                        // this can probably be refactored/revised by comparing the foundApplication and application objects
                        // but we need to somehow take out the "Application_ID" field of the foundApplication objects
                        // so that we can find a match on the rest of the properties/fields
                        if ((foundApplication.ApplicationStatus == application.ApplicationStatus) &&
                        (foundApplication.SalaryExpectation == application.SalaryExpectation) &&
                        (foundApplication.FullTime == application.FullTime) &&
                        (foundApplication.AvailableForDays == application.AvailableForDays) &&
                        (foundApplication.AvailableForEvenings == application.AvailableForEvenings) &&
                        (foundApplication.AvailableForWeekends == application.AvailableForWeekends) &&
                        (foundApplication.MondayFrom == application.MondayFrom) &&
                        (foundApplication.TuesdayFrom == application.TuesdayFrom) &&
                        (foundApplication.WednesdayFrom == application.WednesdayFrom) &&
                        (foundApplication.ThursdayFrom == application.ThursdayFrom) &&
                        (foundApplication.FridayFrom == application.FridayFrom) &&
                        (foundApplication.SaturdayFrom == application.SaturdayFrom) &&
                        (foundApplication.SundayFrom == application.SundayFrom) &&
                        (foundApplication.MondayTo == application.MondayTo) &&
                        (foundApplication.TuesdayTo == application.TuesdayTo) &&
                        (foundApplication.WednesdayTo == application.WednesdayTo) &&
                        (foundApplication.ThursdayTo == application.ThursdayTo) &&
                        (foundApplication.FridayTo == application.FridayTo) &&
                        (foundApplication.SaturdayTo == application.SaturdayTo) &&
                        (foundApplication.SundayTo == application.SundayTo))
                        {
                            application.Application_ID = foundApplication.Application_ID;
                        }
                    }

                    // TODO: make sure to connect applicant/application/job in applied

                    // NOTE: LINQ needs a primary key in order for this to work...

                    Applied applied = new Applied();
                    applied.Applicant_ID = applicant.Applicant_ID;
                    applied.Application_ID = application.Application_ID;
                    applied.Job_ID = Convert.ToInt32(Request.Form["JobID"]);
                    applied.DateApplied = DateTime.Now;


                    /*
                    // test data
                    applied.Applicant_ID = 1;
                    applied.Application_ID = 2;
                    applied.Job_ID = 2;
                    applied.DateApplied = DateTime.Now;
                    */

                    // post (save) applied data
                    // result = httpClient.PostAsJsonAsync(uriApplied, applied).Result;
                    // resultContent = result.Content.ReadAsStringAsync().Result;
                }


                // TODO: need to send all application/applicant related data back to service...

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