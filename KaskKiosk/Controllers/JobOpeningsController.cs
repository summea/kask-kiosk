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
        //
        // GET: /JobOpenings/

        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public async Task<ActionResult> Index()
        {
            List<JobDAO> jobs = await ServerResponse<List<JobDAO>>.GetResponseAsync(ServiceURIs.ServiceJobUri);
            List<JobOpeningDAO> jobOpenings = await ServerResponse<List<JobOpeningDAO>>.GetResponseAsync(ServiceURIs.ServiceJobOpeningUri);
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

        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public async Task<ActionResult> Details(int id)
        {
            List<JobDAO> jobs = await ServerResponse<List<JobDAO>>.GetResponseAsync(ServiceURIs.ServiceJobUri);
            JobOpeningDAO jobOpening = await ServerResponse<JobOpeningDAO>.GetResponseAsync(ServiceURIs.ServiceJobOpeningUri + "/" + id);
            List<JobRequirementDAO> jobRequirements = await ServerResponse<List<JobRequirementDAO>>.GetResponseAsync(ServiceURIs.ServiceJobRequirementUri);
            List<SkillDAO> skills = await ServerResponse<List<SkillDAO>>.GetResponseAsync(ServiceURIs.ServiceSkillUri);
            List<StoreDAO> stores = await ServerResponse<List<StoreDAO>>.GetResponseAsync(ServiceURIs.ServiceStoreUri);

            // TODO: refactor this section so that we only find relevant
            // job requirements for this job opening...
            // at the moment, we loop through all job requirements and pick out what we want

            ViewBag.baseURL = Url.Content("~/");
            ViewBag.jobs = jobs;
            ViewBag.jobOpening = jobOpening;
            ViewBag.jobRequirements = jobRequirements;
            ViewBag.skills = skills;
            ViewBag.stores = stores;
            return View();
        }

        //
        // GET: /JobOpenings/Create
        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public async Task<ActionResult> Create()
        {
            List<JobDAO> jobs = await ServerResponse<List<JobDAO>>.GetResponseAsync(ServiceURIs.ServiceJobUri);
            List<SkillDAO> skills = await ServerResponse<List<SkillDAO>>.GetResponseAsync(ServiceURIs.ServiceSkillUri);
            List<StoreDAO> stores = await ServerResponse<List<StoreDAO>>.GetResponseAsync(ServiceURIs.ServiceStoreUri);

            ViewBag.baseURL = Url.Content("~/");
            ViewBag.stores = stores;
            ViewBag.jobs = jobs;
            ViewBag.skills = skills;
            return View();
        }

        //
        // POST: /JobOpenings/Create

        [HttpPost]
        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public async Task<ActionResult> Create(FormCollection collection)
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
                        jobOpening.OpenDate = DateTime.Now; // note: this could change to the above "OpenDate" line of code in the future...
                        jobOpening.JobID = Convert.ToInt32(Request.Form["JobID"]);
                        jobOpening.Approved = 0;    // "not approved" by default
                        jobOpening.Description = Request.Form["Description"];
                        jobOpening.StoreID = Convert.ToInt32(Request.Form["StoreID"]);

                        // post (save) JobOpening data
                        result = httpClient.PostAsJsonAsync(ServiceURIs.ServiceJobOpeningUri, jobOpening).Result;
                        resultContent = result.Content.ReadAsStringAsync().Result;

                        // save job requirement data
                        // 1. see which checkboxes are checked
                        // 2. create new JobRequirement entries and tie those back to this JobOpening

                        // get correct job opening id of the job opening we just saved
                        var jobOpenings = await ServerResponse<List<JobOpeningDAO>>.GetResponseAsync(ServiceURIs.ServiceJobOpeningUri);
                        int jobOpeningId = jobOpenings.Last().JobOpeningID;

                        // TODO: change the hardcoded "50" into the length of all available skills
                        for (int i = 0; i < 50; i++)
                        {
                            try
                            {
                                if (Request.Form["Skill_" + i] != null)
                                {
                                    // NOTE: renamed Job_ID to JobOpening_ID in JobRequirement table?
                                    // because we aren't really linking these requirements to a "job type" in the Job table
                                    // we are actually linking these to JobOpenings...
                                    JobRequirementDAO jobRequirement = new JobRequirementDAO();
                                    jobRequirement.JobOpeningID = jobOpeningId;
                                    jobRequirement.SkillID = i;

                                    // post (save) JobRequirement data
                                    result = httpClient.PostAsJsonAsync(ServiceURIs.ServiceJobRequirementUri, jobRequirement).Result;
                                    resultContent = result.Content.ReadAsStringAsync().Result;
                                }
                            }
                            catch { }
                        }
                    }

                    return RedirectToAction("Backend", "Home");
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
            List<JobDAO> jobs = await ServerResponse<List<JobDAO>>.GetResponseAsync(ServiceURIs.ServiceJobUri);
            JobOpeningDAO jobOpening = await ServerResponse<JobOpeningDAO>.GetResponseByParamsAsync(ServiceURIs.ServiceJobOpeningUri, id);

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
                        result = httpClient.PostAsJsonAsync(ServiceURIs.ServiceJobOpeningUri, jobOpening).Result;
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
            List<JobDAO> jobs = await ServerResponse<List<JobDAO>>.GetResponseAsync(ServiceURIs.ServiceJobUri);
            JobOpeningDAO jobOpening = await ServerResponse<JobOpeningDAO>.GetResponseByParamsAsync(ServiceURIs.ServiceJobOpeningUri, id);

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
                        result = httpClient.PostAsJsonAsync(ServiceURIs.ServiceJobOpeningUri, jobOpening).Result;
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

        [AllowAnonymous]
        public async Task<ActionResult> ViewCurrentOpenings()
        {
            List<JobDAO> jobs = await ServerResponse<List<JobDAO>>.GetResponseAsync(ServiceURIs.ServiceJobUri);
            List<JobOpeningDAO> jobOpenings = await ServerResponse<List<JobOpeningDAO>>.GetResponseAsync(ServiceURIs.ServiceJobOpeningUri);
            List<StoreDAO> stores = await ServerResponse<List<StoreDAO>>.GetResponseAsync(ServiceURIs.ServiceStoreUri);

            var jobsSortedByDate = jobOpenings.OrderByDescending(o => o.OpenDate);

            ViewBag.jobs = jobs;
            ViewBag.jobOpenings = jobsSortedByDate;
            ViewBag.stores = stores;
            return View();
        }
    }
}