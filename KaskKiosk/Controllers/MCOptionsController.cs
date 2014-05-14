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
    public class MCOptionsController : Controller
    {
        //
        // GET: /MCOptions/

        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public async Task<ActionResult> Index()
        {
            var mCOptions = await ServerResponse<List<MCOptionDAO>>.GetResponseAsync(ServiceURIs.ServiceMCOptionUri);
            ViewBag.mCOptions = mCOptions;
            return View();
        }

        //
        // GET: /MCOptions/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MCOptions/Create
        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public ActionResult Create()
        {
            ViewBag.baseURL = Url.Content("~/");
            return View();
        }

        //
        // POST: /MCOptions/Create

        [HttpPost]
        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.Form["MCOptionDescription"]))
                {
                    // save application form data back to database through service
                    using (HttpClient httpClient = new HttpClient())
                    {
                        httpClient.BaseAddress = new Uri("http://localhost:51309");
                        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage result = new HttpResponseMessage();
                        string resultContent = "";

                        // gather MCOptionOpening form data
                        MCOptionDAO mCOption = new MCOptionDAO();
                        mCOption.MCOptionDescription = Request.Form["MCOptionDescription"];

                        // post (save) MCOptionOpening data
                        result = httpClient.PostAsJsonAsync(ServiceURIs.ServiceMCOptionUri, mCOption).Result;
                        resultContent = result.Content.ReadAsStringAsync().Result;
                    }

                    return RedirectToAction("Index", "MCOptions");
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
        // GET: /MCOptions/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MCOptions/Edit/5

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
        // GET: /MCOptions/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /MCOptions/Delete/5

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