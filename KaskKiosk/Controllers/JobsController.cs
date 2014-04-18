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
    public class JobsController : Controller
    {
        readonly string uriJob = "http://localhost:51309/api/Job";

        private async Task<List<JobDAO>> GetJobsAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(uriJob);
                return JsonConvert.DeserializeObjectAsync<List<JobDAO>>(response).Result;
            }
        }

        //
        // GET: /Jobs/

        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public async Task<ActionResult> Index()
        {
            var jobs = await GetJobsAsync();
            ViewBag.jobs = jobs;
            return View();
        }

        //
        // GET: /Jobs/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Jobs/Create
        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public async Task<ActionResult> Create()
        {
            ViewBag.baseURL = Url.Content("~/");
            return View();
        }

        //
        // POST: /Jobs/Create

        [HttpPost]
        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.Form["title"]))
                {
                    // save application form data back to database through service
                    using (HttpClient httpClient = new HttpClient())
                    {
                        httpClient.BaseAddress = new Uri("http://localhost:51309");
                        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage result = new HttpResponseMessage();
                        string resultContent = "";

                        // gather JobOpening form data
                        JobDAO job = new JobDAO();
                        //jobOpening.OpenDate = Convert.ToDateTime(Request.Form["OpenDate"]);
                        job.Title = Request.Form["title"];

                        // post (save) JobOpening data
                        result = httpClient.PostAsJsonAsync(uriJob, job).Result;
                        resultContent = result.Content.ReadAsStringAsync().Result;
                    }

                    return RedirectToAction("Index", "Jobs");
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
        // GET: /Jobs/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Jobs/Edit/5

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
        // GET: /Jobs/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Jobs/Delete/5

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
