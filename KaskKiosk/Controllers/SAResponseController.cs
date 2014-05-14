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
    public class SAResponsesController : Controller
    {
        //
        // GET: /SAResponses/

        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public async Task<ActionResult> Index()
        {
            var sAResponses = await ServerResponse<List<SAResponseDAO>>.GetResponseAsync(ServiceURIs.ServiceSAResponseUri);
            ViewBag.sAResponses = sAResponses;
            return View();
        }

        //
        // GET: /SAResponses/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /SAResponses/Create
        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public ActionResult Create()
        {
            ViewBag.baseURL = Url.Content("~/");
            return View();
        }

        //
        // POST: /SAResponses/Create

        [HttpPost]
        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.Form["SAResponseDescription"]))
                {
                    // save application form data back to database through service
                    using (HttpClient httpClient = new HttpClient())
                    {
                        httpClient.BaseAddress = new Uri("http://localhost:51309");
                        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage result = new HttpResponseMessage();
                        string resultContent = "";

                        // gather SAResponseOpening form data
                        SAResponseDAO sAResponse = new SAResponseDAO();
                        sAResponse.SAResponseDescription = Request.Form["SAResponseDescription"];

                        // post (save) SAResponseOpening data
                        result = httpClient.PostAsJsonAsync(ServiceURIs.ServiceSAResponseUri, sAResponse).Result;
                        resultContent = result.Content.ReadAsStringAsync().Result;
                    }

                    return RedirectToAction("Index", "SAResponses");
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
        // GET: /SAResponses/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /SAResponses/Edit/5

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
        // GET: /SAResponses/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /SAResponses/Delete/5

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