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
    public class SkillsController : Controller
    {
        //
        // GET: /Skills/

        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public async Task<ActionResult> Index()
        {
            var skills = await ServerResponse<List<SkillDAO>>.GetResponseAsync(ServiceURIs.ServiceSkillUri);
            ViewBag.skills = skills;
            return View();
        }

        //
        // GET: /Skills/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Skills/Create
        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public async Task<ActionResult> Create()
        {
            List<MCQuestionDAO> mcQuestions = await ServerResponse<List<MCQuestionDAO>>.GetResponseAsync(ServiceURIs.ServiceMCQuestionUri);
            List<MCOptionDAO> mcOptions = await ServerResponse<List<MCOptionDAO>>.GetResponseAsync(ServiceURIs.ServiceMCOptionUri);

            ViewBag.baseURL = Url.Content("~/");
            ViewBag.mcQuestions = mcQuestions;
            ViewBag.mcOptions = mcOptions;
            return View();
        }

        //
        // POST: /Skills/Create

        [HttpPost]
        [Authorize(Roles = "Administrator, HiringManager, StoreManager")]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.Form["skillName"]))
                {
                    // save application form data back to database through service
                    using (HttpClient httpClient = new HttpClient())
                    {
                        httpClient.BaseAddress = new Uri("http://localhost:51309");
                        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage result = new HttpResponseMessage();
                        string resultContent = "";

                        // gather SkillOpening form data
                        SkillDAO skill = new SkillDAO();
                        skill.SkillName = Request.Form["skillName"];
                        
                        // post (save) SkillOpening data
                        result = httpClient.PostAsJsonAsync(ServiceURIs.ServiceSkillUri, skill).Result;
                        resultContent = result.Content.ReadAsStringAsync().Result;

                        // get skill id from last insert
                        // get correct skill id of the skill we just saved
                        var skills = await ServerResponse<List<SkillDAO>>.GetResponseAsync(ServiceURIs.ServiceSkillUri);
                        int lastSkillId = skills.Last().SkillID;

                        // gather data for QuestionBank
                        
                        // TODO: change the hardcoded "50" into the length of all available mc options
                        for (int i = 0; i < 50; i++)
                        {
                            try
                            {
                                QuestionBankDAO questionBank = new QuestionBankDAO();
                                questionBank.MCQuestionID = Convert.ToInt32(Request.Form["MCQuestion_1"]);

                                // TODO: the hardcoded "1" should match the number of mc questions we iterate over... for now, it's just 1
                                // check if this is a selected multiple choice option
                                if (Request.Form["MCOptionID_1_" + i] != null)
                                {
                                    questionBank.MCOptionID = i;
                                
                                    // TODO: the hardcoded "1" should match the number of mc questions we iterate over... for now, it's just 1
                                    // check if this is a valid option (a "correct answer")
                                    if (Request.Form["Valid_MCOptionID_1_" + i] != null)
                                    {
                                        questionBank.MCCorrectOption = 1;
                                    }
                                    else
                                    {
                                        questionBank.MCCorrectOption = 0;
                                    }

                                    // post data for QuestionBank
                                    result = httpClient.PostAsJsonAsync(ServiceURIs.ServiceQuestionBankUri, questionBank).Result;
                                    resultContent = result.Content.ReadAsStringAsync().Result;

                                    // get question bank id from last insert
                                    // get correct question bank id of the question bank we just saved
                                    var questionBanks = await ServerResponse<List<QuestionBankDAO>>.GetResponseAsync(ServiceURIs.ServiceQuestionBankUri);
                                    int lastQuestionBankId = questionBanks.Last().QuestionBankID;

                                    // then connect up QuestionBank data to SkillQuestionBank
                                    // gather skill-question bank data
                                    SkillQuestionBankDAO skillQuestionBank = new SkillQuestionBankDAO();
                                    skillQuestionBank.SkillID = lastSkillId;
                                    skillQuestionBank.QuestionBankID = lastQuestionBankId;

                                    // and then post data for skill-question bank
                                    result = httpClient.PostAsJsonAsync(ServiceURIs.ServiceSkillQuestionBankUri, skillQuestionBank).Result;
                                    resultContent = result.Content.ReadAsStringAsync().Result;
                                }
                            }
                            catch { }
                        }
                    }

                    return RedirectToAction("Index", "Skills");
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
        // GET: /Skills/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Skills/Edit/5

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
        // GET: /Skills/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Skills/Delete/5

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