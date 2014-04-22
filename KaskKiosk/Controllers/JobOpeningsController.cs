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
    public class JobOpeningsController : Controller
    {
        readonly string uriJob = "http://localhost:51309/api/Job";
        readonly string uriJobOpening = "http://localhost:51309/api/JobOpening";

        private async Task<List<JobDAO>> GetJobsAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(uriJob);
                return JsonConvert.DeserializeObjectAsync<List<JobDAO>>(response).Result;
            }
        }

        private async Task<List<JobOpeningDAO>> GetJobOpeningsAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(uriJobOpening);
                return JsonConvert.DeserializeObjectAsync<List<JobOpeningDAO>>(response).Result;
            }
        }

        private async Task<JobOpeningDAO> GetJobOpeningByIdAsync(int id = 0)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(uriJobOpening + "/" + id.ToString());
                return JsonConvert.DeserializeObjectAsync<JobOpeningDAO>(response).Result;
            }
        }

        //
        // GET: /JobOpenings/

        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public async Task<ActionResult> Index()
        {
            List<JobDAO> jobs = await GetJobsAsync();
            List<JobOpeningDAO> jobOpenings = await GetJobOpeningsAsync();
            List<string> allowableActions = new List<string>();
            
            if (this.User.IsInRole("StoreManager"))
            {
                allowableActions.Add("approve");
                allowableActions.Add("reject");
            }
   
            ViewBag.allowableActions = allowableActions;
            ViewBag.jobs = jobs;
            ViewBag.jobOpenings = jobOpenings;
            return View();
        }

        //
        // GET: /JobOpenings/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /JobOpenings/Create
        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public async Task<ActionResult> Create()
        {
            List<JobDAO> jobs = await GetJobsAsync();

            ViewBag.baseURL = Url.Content("~/");
            ViewBag.jobs = jobs;
            return View();
        }

        //
        // POST: /JobOpenings/Create

        [HttpPost]
        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.Form["JobID"]))
                {
                    // save application form data back to database through service
                    using (HttpClient httpClient = new HttpClient())
                    {
                        httpClient.BaseAddress = new Uri("http://localhost:51309");
                        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage result = new HttpResponseMessage();
                        string resultContent = "";

                        // gather JobOpening form data
                        JobOpeningDAO jobOpening = new JobOpeningDAO();
                        //jobOpening.OpenDate = Convert.ToDateTime(Request.Form["OpenDate"]);
                        jobOpening.OpenDate = DateTime.Now; // note: this could change to the above "OpenDate" line of code in the future...
                        jobOpening.JobID = Convert.ToInt32(Request.Form["JobID"]);
                        jobOpening.Approved = 0;    // "not approved" by default

                        // post (save) JobOpening data
                        result = httpClient.PostAsJsonAsync(uriJobOpening, jobOpening).Result;
                        resultContent = result.Content.ReadAsStringAsync().Result;
                    }

                    return RedirectToAction("Welcome", "Home");
                }
                else
                {
                    // TODO: validation later on...
                    return RedirectToAction("Create");
                }
            }
            catch
            {
                // TODO: validation later on...
                return RedirectToAction("Create");
            }
        }

        //
        // GET: /JobOpenings/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /JobOpenings/Edit/5

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
        // GET: /JobOpenings/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /JobOpenings/Delete/5

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

        //
        // GET: /JobOpenings/Approve

        [Authorize(Roles = "Administrator, StoreManager")]
        public async Task<ActionResult> Approve(int id)
        {
            List<JobDAO> jobs = await GetJobsAsync();
            JobOpeningDAO jobOpening = await GetJobOpeningByIdAsync(id);

            ViewBag.baseURL = Url.Content("~/");
            ViewBag.jobs = jobs;
            ViewBag.jobOpening = jobOpening;
            return View();
        }

        //
        // POST: /JobOpenings/Approve

        [HttpPost]
        [Authorize(Roles = "Administrator, StoreManager")]
        public ActionResult Approve(FormCollection collection)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.Form["JobOpeningID"]))
                {
                    // save application form data back to database through service
                    using (HttpClient httpClient = new HttpClient())
                    {
                        httpClient.BaseAddress = new Uri("http://localhost:51309");
                        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage result = new HttpResponseMessage();
                        string resultContent = "";

                        // gather JobOpening form data
                        JobOpeningDAO jobOpening = new JobOpeningDAO();
                        jobOpening.JobOpeningID = Convert.ToInt32(Request.Form["JobOpeningID"]);
                        jobOpening.Approved = 1;

                        // post (save) JobOpening data
                        result = httpClient.PostAsJsonAsync(uriJobOpening, jobOpening).Result;
                        resultContent = result.Content.ReadAsStringAsync().Result;
                    }

                    return RedirectToAction("Index", "JobOpenings");
                }
                else
                {
                    // TODO: validation later on...
                    return RedirectToAction("Index", "JobOpenings");
                }
            }
            catch
            {
                return RedirectToAction("Index", "JobOpenings");
            }
        }

        //
        // GET: /JobOpenings/Reject

        [Authorize(Roles = "Administrator, StoreManager")]
        public async Task<ActionResult> Reject(int id)
        {
            List<JobDAO> jobs = await GetJobsAsync();
            JobOpeningDAO jobOpening = await GetJobOpeningByIdAsync(id);

            ViewBag.baseURL = Url.Content("~/");
            ViewBag.jobs = jobs;
            ViewBag.jobOpening = jobOpening;
            return View();
        }

        //
        // POST: /JobOpenings/Reject

        [HttpPost]
        [Authorize(Roles = "Administrator, StoreManager")]
        public ActionResult Reject(FormCollection collection)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.Form["JobOpeningID"]))
                {
                    // save application form data back to database through service
                    using (HttpClient httpClient = new HttpClient())
                    {
                        httpClient.BaseAddress = new Uri("http://localhost:51309");
                        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage result = new HttpResponseMessage();
                        string resultContent = "";

                        // gather JobOpening form data
                        JobOpeningDAO jobOpening = new JobOpeningDAO();
                        jobOpening.JobOpeningID = Convert.ToInt32(Request.Form["JobOpeningID"]);
                        jobOpening.Approved = 0;

                        // post (save) JobOpening data
                        result = httpClient.PostAsJsonAsync(uriJobOpening, jobOpening).Result;
                        resultContent = result.Content.ReadAsStringAsync().Result;
                    }

                    return RedirectToAction("Index", "JobOpenings");
                }
                else
                {
                    // TODO: validation later on...
                    return RedirectToAction("Index", "JobOpenings");
                }
            }
            catch
            {
                return RedirectToAction("Index", "JobOpenings");
            }
        }
    }
}