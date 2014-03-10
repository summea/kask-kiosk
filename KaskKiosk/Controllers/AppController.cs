﻿using KaskKiosk.AESApplicationServiceRef;

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
        readonly string uri = "http://localhost:51309/api/Application";
        readonly string uriApplicant = "http://localhost:51309/api/Applicant";

        private async Task<List<ApplicationDAO>> GetApplicationsAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                Task<string> response = httpClient.GetStringAsync(uri);
                return JsonConvert.DeserializeObjectAsync<List<ApplicationDAO>>(response.Result).Result;
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
                timePickerList.Add(i.ToString() + ":00");
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
                // UNCOMMENT ME
                /* =================================================
                ApplicantDAO applicant = new ApplicantDAO();
                applicant.FirstName = Request.Form["FirstName"];
                applicant.LastName = Request.Form["LastName"];
                applicant.SSN = Request.Form["SSN"];*/
                // ==================================================

                ApplicationDAO application = new ApplicationDAO();
                application.ApplicationStatus = "Submitted";

                // save data back to service
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("http://localhost:51309");
                    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage result = new HttpResponseMessage();
                    string resultContent = "";

                    /* UNCOMMENT ME
                     * >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                    // post (save) applicant data
                    result = httpClient.PostAsJsonAsync(uriApplicant, applicant).Result;
                    resultContent = result.Content.ReadAsStringAsync().Result;
                    <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< */

                    // post (save) application data
                    result = httpClient.PostAsJsonAsync(uri, application).Result;
                    resultContent = result.Content.ReadAsStringAsync().Result;
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
