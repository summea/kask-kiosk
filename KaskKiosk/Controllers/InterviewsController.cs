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
    public class InterviewsController : Controller
    {
        //
        // GET: /Interviews/

        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public async Task<ActionResult> Index()
        {
            var interviews = await ServerResponse<List<InterviewDAO>>.GetResponseAsync(ServiceURIs.ServiceInterviewUri);
            ViewBag.interviews = interviews;
            return View();
        }

        //
        // GET: /Interviews/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Interviews/Create
        [AllowAnonymous]
        public async Task<ActionResult> Create(int id)
        {
            // get all sa questions
            List<SAQuestionDAO> allSaQuestions = await ServerResponse<List<SAQuestionDAO>>.GetResponseAsync(ServiceURIs.ServiceSAQuestionUri);

            // get list of sa question for this particular job opening
            List<JobOpeningInterviewQuestionDAO> allSaQuestionsForJobOpening = await ServerResponse<List<JobOpeningInterviewQuestionDAO>>.GetResponseAsync(ServiceURIs.ServiceJobOpeningInterviewQuestionUri);

            // organize related sa question ids into a list for the view
            HashSet<int> relatedSaQuestionIds = new HashSet<int>();
            foreach (JobOpeningInterviewQuestionDAO item in allSaQuestionsForJobOpening)
            {
                if (item.JobOpeningID.Equals(id))
                    relatedSaQuestionIds.Add(item.SAQuestionID);
            }

            // find applicant id from the previously submitted Application form
            var applieds = await ServerResponse<List<AppliedDAO>>.GetResponseAsync(ServiceURIs.ServiceAppliedUri);
            int applicantID = applieds.Last().ApplicantID;

            ViewBag.baseURL = Url.Content("~/");
            ViewBag.allSaQuestions = allSaQuestions;
            ViewBag.relatedSaQuestionIds = relatedSaQuestionIds;
            ViewBag.JobOpeningIDReferenceNumber = id;
            ViewBag.applicantID = applicantID;
            return View();
        }

        //
        // POST: /Interviews/Create

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                // save application form data back to database through service
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("http://localhost:51309");
                    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage result = new HttpResponseMessage();
                    string resultContent = "";

                    // find applicant id from the previously submitted Application form, if available
                    int applicantID = 0;
                    if (Convert.ToInt32(Request.Form["applicantID"]) > 0)
                    {
                        applicantID = Convert.ToInt32(Request.Form["applicantID"]);
                    }
                    // otherwise, get last applicant id from database
                    else
                    {
                        var applieds = await ServerResponse<List<AppliedDAO>>.GetResponseAsync(ServiceURIs.ServiceAppliedUri);
                        applicantID = applieds.Last().ApplicantID;
                    }

                    // and save the short answer responses (both to SAResponses and to Interview tables)

                    // TODO: change the hardcoded "50" into some sort of variable
                    // (i.e. the length of all possible SA responses from form)
                    for (int i = 0; i < 50; i++)
                    {
                        try
                        {
                            if (Request.Form["SAResponse_" + i] != null)
                            {
                                // save short answer response to SAResponses, first
                                // so gather up response info:
                                SAResponseDAO saResponse = new SAResponseDAO();
                                saResponse.SAResponseDescription = Request.Form["SAResponse_" + i];

                                // post (save) short answer response data to SAResponses
                                result = httpClient.PostAsJsonAsync(ServiceURIs.ServiceSAResponseUri, saResponse).Result;
                                resultContent = result.Content.ReadAsStringAsync().Result;

                                // then get last inserted short answer response ID for the Interview table later
                                var saResponses = await ServerResponse<List<SAResponseDAO>>.GetResponseAsync(ServiceURIs.ServiceSAResponseUri);
                                int lastInsertedSaResponseId = saResponses.Last().SAResponseID;

                                // gather short answer response data
                                InterviewDAO interview = new InterviewDAO();
                                interview.ApplicantID = applicantID;
                                interview.SAQuestionID = i;
                                interview.SAResponseID = lastInsertedSaResponseId;
                                //interview.UserID = applicantID; // this might be overlapping... we might just need applicantID for later matchups...

                                // post (save) short answer response data to Interview table
                                result = httpClient.PostAsJsonAsync(ServiceURIs.ServiceInterviewUri, interview).Result;
                                resultContent = result.Content.ReadAsStringAsync().Result;
                            }
                        }
                        catch { }
                    }
                }

                return RedirectToAction("Welcome", "Home");
            }
            catch
            {
                // TODO: validation later on...
                return RedirectToAction("Create");
            }
            
        }

        //
        // GET: /Interviews/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Interviews/Edit/5

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
        // GET: /Interviews/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Interviews/Delete/5

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