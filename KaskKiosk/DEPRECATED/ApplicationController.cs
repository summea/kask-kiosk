/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KaskKiosk.AESApplicationServiceRef;

namespace KaskKiosk.Controllers
{
    public class ApplicationController : Controller
    {
        //
        // GET: /Application/

        public ActionResult Index()
        {
            ApplicationServiceClient client = new ApplicationServiceClient();
            IList<Application> applications = client.GetApplications();
            ViewBag.applications = applications.ToList();

            return View();
        }

        //
        // GET: /Application/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Application/Create

        public ActionResult Create()
        {
            List<String> timePickerList = new List<String>();
            for (int i = 0; i < 24; i++)
            {
                timePickerList.Add(i.ToString() + ":00");
            }

            ViewBag.timePickerList = timePickerList;
            return View();
        }

        //
        // POST: /Application/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Application/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Application/Edit/5

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
        // GET: /Application/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Application/Delete/5

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
*/