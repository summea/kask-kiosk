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
    public class SAQuestionsController : Controller
    {
        //
        // GET: /SAQuestions/

        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public async Task<ActionResult> Index()
        {
            var sAQuestions = await ServerResponse<List<SAQuestionDAO>>.GetResponseAsync(ServiceURIs.ServiceSAQuestionUri);
            ViewBag.sAQuestions = sAQuestions;
            return View();
        }

        //
        // GET: /SAQuestions/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /SAQuestions/Create
        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public ActionResult Create()
        {
            ViewBag.baseURL = Url.Content("~/");
            return View();
        }

        //
        // POST: /SAQuestions/Create

        [HttpPost]
        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.Form["SAQuestionDescription"]))
                {
                    // save application form data back to database through service
                    using (HttpClient httpClient = new HttpClient())
                    {
                        httpClient.BaseAddress = new Uri("http://localhost:51309");
                        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage result = new HttpResponseMessage();
                        string resultContent = "";

                        // gather SAQuestionOpening form data
                        SAQuestionDAO sAQuestion = new SAQuestionDAO();
                        sAQuestion.SAQuestionDescription = Request.Form["SAQuestionDescription"];

                        // post (save) SAQuestionOpening data
                        result = httpClient.PostAsJsonAsync(ServiceURIs.ServiceSAQuestionUri, sAQuestion).Result;
                        resultContent = result.Content.ReadAsStringAsync().Result;
                    }

                    return RedirectToAction("Index", "SAQuestions");
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
        // GET: /SAQuestions/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /SAQuestions/Edit/5

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
        // GET: /SAQuestions/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /SAQuestions/Delete/5

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