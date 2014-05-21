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
    public class AssessmentsController : Controller
    {
        //
        // GET: /Assessments/

        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public async Task<ActionResult> Index()
        {
            var assessments = await ServerResponse<List<AssessmentDAO>>.GetResponseAsync(ServiceURIs.ServiceAssessmentUri);
            ViewBag.assessments = assessments;
            return View();
        }

        //
        // GET: /Assessments/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Assessments/Create
        [AllowAnonymous]
        public async Task<ActionResult> Create(int id)
        {
            // TODO: in the future, use more specific API calls for these queries... or LINQ or something for this section

            // get skill information for questions
            // via job opening id in JobRequirement table
            List<JobRequirementDAO> jobRequirements = await ServerResponse<List<JobRequirementDAO>>.GetResponseAsync(ServiceURIs.ServiceJobRequirementUri);

            // find related skill ids
            List<int> relatedSkillIds = new List<int>();
            foreach (JobRequirementDAO requirementItem in jobRequirements)
            {
                if (requirementItem.JobOpeningID.Equals(id))
                    relatedSkillIds.Add(requirementItem.SkillID);
            }

            // then find related skill-questionbank items
            List<SkillQuestionBankDAO> skillQuestionBanks = await ServerResponse<List<SkillQuestionBankDAO>>.GetResponseAsync(ServiceURIs.ServiceSkillQuestionBankUri);

            List<int> relatedQuestionBankIds = new List<int>();
            foreach (SkillQuestionBankDAO skillQuestionBankItem in skillQuestionBanks)
            {
                if (relatedSkillIds.Contains(skillQuestionBankItem.SkillID))
                    relatedQuestionBankIds.Add(skillQuestionBankItem.QuestionBankID);
            }

            // then find related mc question banks
            List<QuestionBankDAO> questionBanks = await ServerResponse<List<QuestionBankDAO>>.GetResponseAsync(ServiceURIs.ServiceQuestionBankUri);
            
            HashSet<int> relatedMcQuestionIds = new HashSet<int>();

            // idea for this:
            // QuestionBankID -> QuestionID -> OptionID
            Dictionary<int, Dictionary<int, HashSet<int>>> relatedMcOptionsForMcQuestions = new Dictionary<int, Dictionary<int, HashSet<int>>>();

            foreach (QuestionBankDAO questionBankItem in questionBanks)
            {
                if (relatedQuestionBankIds.Contains(questionBankItem.QuestionBankID))
                {
                    // get related question ids
                    relatedMcQuestionIds.Add(questionBankItem.MCQuestionID);
                    
                    // get related question options for each question id
                    if (relatedMcOptionsForMcQuestions.ContainsKey(questionBankItem.MCQuestionID))
                    {
                        // key already exists, so just append
                        relatedMcOptionsForMcQuestions[questionBankItem.QuestionBankID][questionBankItem.MCQuestionID].Add(questionBankItem.MCOptionID);
                    }
                    else
                    {
                        // create new key, then append
                        relatedMcOptionsForMcQuestions.Add(questionBankItem.QuestionBankID, new Dictionary<int, HashSet<int>>());
                        relatedMcOptionsForMcQuestions[questionBankItem.QuestionBankID].Add(questionBankItem.MCQuestionID, new HashSet<int> { questionBankItem.MCOptionID });
                    }
                }
            }

            // for now, get a list of all mc questions (to use in the view)
            List<MCQuestionDAO> allMcQuestions = await ServerResponse<List<MCQuestionDAO>>.GetResponseAsync(ServiceURIs.ServiceMCQuestionUri);

            // for now, get a list of all mc options (to use in the view)
            List<MCOptionDAO> allMcOptions = await ServerResponse<List<MCOptionDAO>>.GetResponseAsync(ServiceURIs.ServiceMCOptionUri);

            // find applicant id from the previously submitted Application form
            var applieds = await ServerResponse<List<AppliedDAO>>.GetResponseAsync(ServiceURIs.ServiceAppliedUri);
            int applicantID = applieds.Last().ApplicantID;

            ViewBag.baseURL = Url.Content("~/");
            ViewBag.allMcQuestions = allMcQuestions;
            ViewBag.allMcOptions = allMcOptions;
            ViewBag.relatedMcQuestionIds = relatedMcQuestionIds;
            ViewBag.relatedMcOptionsForMcQuestions = relatedMcOptionsForMcQuestions;
            ViewBag.jobOpeningIDReferenceNumber = id;
            ViewBag.applicantID = applicantID;
            return View();
        }

        //
        // POST: /Assessments/Create

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

                    // gather AssessmentOpening form data
                    AssessmentDAO assessment = new AssessmentDAO();
                    assessment.ApplicantID = applicantID;

                    // and save the mc answer chosen by applicant

                    // first, parse the questionBankId and optionId of checkbox chosen by user

                    int parsedQuestionBankId = 0;
                    int parsedOptionId = 0;

                    var questionBankAnswers = await ServerResponse<List<QuestionBankDAO>>.GetResponseAsync(ServiceURIs.ServiceQuestionBankUri);

                    // TODO: change the hardcoded "50" into some sort of variable
                    // (i.e. the length of all possible questionBank ids, questions, and options)
                    for (int i = 0; i < 50; i++)
                    {
                        for (int j = 0; j < 50; j++)
                        {
                            for (int k = 0; k < 50; k++)
                            {
                                try
                                {
                                    if (Request.Form["MCOption_" + i + "_" + j + "_" + k] != null)
                                    {
                                        parsedQuestionBankId = i;
                                        parsedOptionId = j;

                                        // check to see if answer is valid or "correct"
                                        // if not valid, redirect to "sorry" screen
                                        foreach (QuestionBankDAO qbItem in questionBankAnswers)
                                        {
                                            if (qbItem.QuestionBankID.Equals(parsedQuestionBankId))
                                            {
                                                if (!parsedOptionId.Equals(Convert.ToInt32(qbItem.MCCorrectOption)))
                                                {
                                                    return RedirectToAction("Sorry");
                                                }
                                            }
                                        }
                                    }
                                }
                                catch { }
                            }
                        }
                    }

                    // this is the answer that the user chose for this particular QuestionBank question
                    assessment.QuestionBankID = parsedQuestionBankId;

                    // post (save) AssessmentOpening data
                    result = httpClient.PostAsJsonAsync(ServiceURIs.ServiceAssessmentUri, assessment).Result;
                    resultContent = result.Content.ReadAsStringAsync().Result;
                }

                // redirect to phone interview questions, if possible
                return RedirectToAction("Create", "Interviews", new { ID = Convert.ToInt32(Request.Form["JobOpeningIDReferenceNumberOnAssessment"]) });
            }
            catch
            {
                // TODO: validation later on...
                return RedirectToAction("Create");
            }
        }

        //
        // GET: /Assessments/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Assessments/Edit/5

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
        // GET: /Assessments/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Assessments/Delete/5

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
        // GET: /Assessments/Sorry

        [AllowAnonymous]
        public ActionResult Sorry()
        {
            return View();
        }
    }
}