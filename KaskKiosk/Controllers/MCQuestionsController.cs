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
    public class MCQuestionsController : Controller
    {
        //
        // GET: /MCQuestions/

        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public async Task<ActionResult> Index()
        {
            var mCQuestions = await ServerResponse<List<MCQuestionDAO>>.GetResponseAsync(ServiceURIs.ServiceMCQuestionUri);
            ViewBag.mCQuestions = mCQuestions;
            return View();
        }

        //
        // GET: /MCQuestions/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MCQuestions/Create
        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public ActionResult Create()
        {
            ViewBag.baseURL = Url.Content("~/");
            return View();
        }

        //
        // POST: /MCQuestions/Create

        [HttpPost]
        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.Form["MCQuestionDescription"]))
                {
                    // save application form data back to database through service
                    using (HttpClient httpClient = new HttpClient())
                    {
                        httpClient.BaseAddress = new Uri("http://localhost:51309");
                        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage result = new HttpResponseMessage();
                        string resultContent = "";

                        // gather MCQuestionOpening form data
                        MCQuestionDAO mCQuestion = new MCQuestionDAO();
                        mCQuestion.MCQuestionDescription = Request.Form["MCQuestionDescription"];

                        // post (save) MCQuestionOpening data
                        result = httpClient.PostAsJsonAsync(ServiceURIs.ServiceMCQuestionUri, mCQuestion).Result;
                        resultContent = result.Content.ReadAsStringAsync().Result;
                    }

                    return RedirectToAction("Index", "MCQuestions");
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
        // GET: /MCQuestions/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MCQuestions/Edit/5

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
        // GET: /MCQuestions/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /MCQuestions/Delete/5

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