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
    public class AppController : Controller
    {
        readonly string generalDateFormat = "M/yyyy";
        readonly string generalTimeFormat = "{0:hh\\:mm}";

        [Authorize(Roles = "Administrator, HiringManager")]
        public async Task<ActionResult> Index()
        {
            var apps = await ServerResponse<List<ApplicationDAO>>.GetResponseAsync(ServiceURIs.ServiceApplicationUri);
            List<string> allowableActions = new List<string>();

            if (this.User.IsInRole("HiringManager"))
            {
                allowableActions.Add("approve");
                allowableActions.Add("reject");
            }

            ViewBag.allowableActions = allowableActions;
            ViewBag.applications = apps;
            return View();
        }

        [AllowAnonymous]
        public async Task<ActionResult> Create()
        {
            var jobs = await ServerResponse<List<JobDAO>>.GetResponseAsync(ServiceURIs.ServiceJobUri);
            List<String> timePickerList = new List<String>();

            // create time picker range (in hours)
            for (int i = 0; i < 24; i++)
            {
                timePickerList.Add(i.ToString());
            }

            ViewBag.baseURL = Url.Content("~/");
            ViewBag.jobs = jobs;
            ViewBag.timePickerList = timePickerList;
            return View();
        }

        /// ********************************************************************** ///
        ///     In fact, we can make a viewmodel and populate our data with it.    ///
        /// ********************************************************************** ///

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.Form["FirstName"]) && !string.IsNullOrEmpty(Request.Form["LastName"]) && !string.IsNullOrEmpty(Request.Form["SSN"]))
                {
                    // save application form data back to database through service
                    using (HttpClient httpClient = new HttpClient())
                    {
                        httpClient.BaseAddress = new Uri("http://localhost:51309");
                        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage result = new HttpResponseMessage();
                        string resultContent = "";

                        // gather Applicant form data
                        ApplicantDAO applicant = new ApplicantDAO();
                        applicant.FirstName = Request.Form["FirstName"];
                        applicant.MiddleName = Request.Form["MiddleName"];
                        applicant.LastName = Request.Form["LastName"];
                        applicant.SSN = Request.Form["SSN"];
                        applicant.ApplicantAddress = Request.Form["ApplicantAddress"];
                        applicant.Phone = Request.Form["ApplicantPhone"];
                        applicant.NameAlias = Request.Form["NameAlias"];

                        // post (save) applicant data
                        result = httpClient.PostAsJsonAsync(ServiceURIs.ServiceApplicantUri, applicant).Result;
                        resultContent = result.Content.ReadAsStringAsync().Result;

                        // gather Application form data
                        ApplicationDAO application = new ApplicationDAO();
                        application.ApplicationStatus = "Submitted";
                        application.SalaryExpectation = Request.Form["SalaryExpectation"];
                        application.FullTime = Convert.ToByte(Request.Form["FullTime"]);
                        application.AvailableForDays = Convert.ToByte(Request.Form["AvailableForDays"]);
                        application.AvailableForEvenings = Convert.ToByte(Request.Form["AvailableForEvenings"]);
                        application.AvailableForWeekends = Convert.ToByte(Request.Form["AvailableForWeekends"]);
                        application.MondayFrom = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["MondayFrom"]));
                        application.TuesdayFrom = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["TuesdayFrom"]));
                        application.WednesdayFrom = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["WednesdayFrom"]));
                        application.ThursdayFrom = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["ThursdayFrom"]));
                        application.FridayFrom = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["FridayFrom"]));
                        application.SaturdayFrom = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["SaturdayFrom"]));
                        application.SundayFrom = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["SundayFrom"]));
                        application.MondayTo = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["MondayTo"]));
                        application.TuesdayTo = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["TuesdayTo"]));
                        application.WednesdayTo = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["WednesdayTo"]));
                        application.ThursdayTo = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["ThursdayTo"]));
                        application.FridayTo = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["FridayTo"]));
                        application.SaturdayTo = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["SaturdayTo"]));
                        application.SundayTo = System.TimeSpan.FromHours(Convert.ToDouble(Request.Form["SundayTo"]));

                        // post (save) application data
                        result = httpClient.PostAsJsonAsync(ServiceURIs.ServiceApplicationUri, application).Result;
                        resultContent = result.Content.ReadAsStringAsync().Result;

                        // get correct applicant id
                        // TODO: there is still something we might need to change about this...
                        // ***** We don't, it's safe to assume that id would be the last item on the list
                        // since we're using auto incremented id. *****
                        var applicants = await ServerResponse<List<ApplicantDAO>>.GetResponseAsync(ServiceURIs.ServiceApplicantUri); ;
                        applicant.ApplicantID = applicants.Last().ApplicantID;

                        // get correct application id
                        // TODO: there is still something we might need to change about this...
                        var applications = await ServerResponse<List<ApplicationDAO>>.GetResponseAsync(ServiceURIs.ServiceApplicationUri);
                        application.ApplicationID = applications.Last().ApplicationID;

                        // Create Applied DAO;
                        AppliedDAO applied = new AppliedDAO();
                        applied.ApplicantID = applicant.ApplicantID;
                        applied.ApplicationID = application.ApplicationID;
                        applied.JobOpeningID = Convert.ToInt32(Request.Form["JobOpeningIDReferenceNumber"]);
                        applied.DateApplied = DateTime.Now;

                        // post (save) applied data
                        result = httpClient.PostAsJsonAsync(ServiceURIs.ServiceAppliedUri, applied).Result;
                        resultContent = result.Content.ReadAsStringAsync().Result;

                        // gather Employer data
                        EmployerDAO employer = new EmployerDAO();
                        var employers = await ServerResponse<List<EmployerDAO>>.GetResponseAsync(ServiceURIs.ServiceEmployerUri);
                        EmploymentDAO employment = new EmploymentDAO();

                        for (int i = 1; i < 4; i++)
                        {
                            // make sure this form item is filled in...
                            if ((!string.IsNullOrWhiteSpace(Request.Form["EmployerName_" + i]) &&
                                  !string.IsNullOrWhiteSpace(Request.Form["EmployerAddress_" + i])))
                            {
                                employer.Name = Request.Form["EmployerName_" + i];
                                employer.EmployerAddress = Request.Form["EmployerAddress_" + i];
                                if (!string.IsNullOrWhiteSpace(Request.Form["EmployerPhone_" + i]))
                                    employer.PhoneNumber = Request.Form["EmployerPhone_" + i];

                                //if (!string.IsNullOrWhiteSpace(employer.Name))
                                //{
                                // TODO: check if employer already exists in database
                                // TODO: if employer exists in database: don't insert data
                                // if employer !exists in database: insert data

                                // post (save) employer data
                                result = httpClient.PostAsJsonAsync(ServiceURIs.ServiceEmployerUri, employer).Result;
                                resultContent = result.Content.ReadAsStringAsync().Result;
                                //}

                                // get correct employer id
                                // TODO: there is still something we might need to change about this...
                                employers = await ServerResponse<List<EmployerDAO>>.GetResponseAsync(ServiceURIs.ServiceEmployerUri); ;
                                employer.EmployerID = employers.Last().EmployerID;


                                // gather Employment data
                                employment = new EmploymentDAO();
                                employment.ApplicantID = applicant.ApplicantID;
                                employment.EmployerID = employer.EmployerID;
                                if (!string.IsNullOrWhiteSpace(Request.Form["MayWeContactCurrentEmployer_" + i]))
                                    employment.MayWeContactCurrentEmployer = Convert.ToByte(Request.Form["MayWeContactCurrentEmployer_" + i]);
                                if (!string.IsNullOrWhiteSpace(Request.Form["EmployedFrom_" + i]))
                                    employment.EmployedFrom = Convert.ToDateTime(Request.Form["EmployedFrom_" + i]);
                                if (!string.IsNullOrWhiteSpace(Request.Form["EmployedTo_" + i]))
                                    employment.EmployedTo = Convert.ToDateTime(Request.Form["EmployedTo_" + i]);
                                if (!string.IsNullOrWhiteSpace(Request.Form["EmployerSupervisor_" + i]))
                                    employment.Supervisor = Request.Form["EmployerSupervisor_" + i];
                                if (!string.IsNullOrWhiteSpace(Request.Form["EmployedPosition_" + i]))
                                    employment.Position = Request.Form["EmployedPosition_" + i];
                                if (!string.IsNullOrWhiteSpace(Request.Form["EmployedStartingSalary_" + i]))
                                    employment.StartingSalary = Request.Form["EmployedStartingSalary_" + i];
                                if (!string.IsNullOrWhiteSpace(Request.Form["EmployedEndingSalary_" + i]))
                                    employment.EndingSalary = Request.Form["EmployedEndingSalary_" + i];
                                if (!string.IsNullOrWhiteSpace(Request.Form["EmployedReasonForLeaving_" + i]))
                                    employment.ReasonForLeaving = Request.Form["EmployedReasonForLeaving_" + i];
                                if (!string.IsNullOrWhiteSpace(Request.Form["EmployedResponsibilities_" + i]))
                                    employment.Responsibilities = Request.Form["EmployedResponsibilities_" + i];

                                // post (save) employment data
                                result = httpClient.PostAsJsonAsync(ServiceURIs.ServiceEmploymentUri, employment).Result;
                                resultContent = result.Content.ReadAsStringAsync().Result;
                            }
                        }

                        // gather School and Education data
                        SchoolDAO school = new SchoolDAO();
                        var schools = await ServerResponse<List<SchoolDAO>>.GetResponseAsync(ServiceURIs.ServiceSchoolUri);
                        EducationDAO education = new EducationDAO();

                        for (int i = 1; i < 4; i++)
                        {
                            // make sure this form item is filled in...
                            if ((!string.IsNullOrWhiteSpace(Request.Form["SchoolName_" + i]) &&
                                  !string.IsNullOrWhiteSpace(Request.Form["SchoolAddress_" + i])))
                            {
                                school = new SchoolDAO();
                                school.SchoolName = Request.Form["SchoolName_" + i];
                                school.SchoolAddress = Request.Form["SchoolAddress_" + i];

                                //if (!string.IsNullOrWhiteSpace(school.SchoolName))
                                //{
                                // TODO: check if school already exists in database
                                // TODO: if school exists in database: don't insert data
                                // if school !exists in database: insert data

                                // post (save) school data
                                result = httpClient.PostAsJsonAsync(ServiceURIs.ServiceSchoolUri, school).Result;
                                resultContent = result.Content.ReadAsStringAsync().Result;
                                //}

                                // get correct school id
                                // TODO: there is still something we might need to change about this...
                                schools = await ServerResponse<List<SchoolDAO>>.GetResponseAsync(ServiceURIs.ServiceSchoolUri);
                                school.SchoolID = schools.Last().SchoolID;

                                // gather Education data
                                education = new EducationDAO();
                                education.ApplicantID = applicant.ApplicantID;
                                education.SchoolID = school.SchoolID;
                                if (!string.IsNullOrWhiteSpace(Request.Form["YearsAttendedFrom_" + i]))
                                    education.YearsAttendedFrom = Convert.ToDateTime(Request.Form["YearsAttendedFrom_" + i]);
                                if (!string.IsNullOrWhiteSpace(Request.Form["YearsAttendedTo_" + i]))
                                    education.YearsAttendedTo = Convert.ToDateTime(Request.Form["YearsAttendedTo_" + i]);
                                if (!string.IsNullOrWhiteSpace(Request.Form["Graduated_" + i]))
                                    education.Graduated = Convert.ToByte(Request.Form["Graduated_" + i]);
                                if (!string.IsNullOrWhiteSpace(Request.Form["DegreeAndMajor_" + i]))
                                    education.DegreeAndMajor = Request.Form["DegreeAndMajor_" + i];

                                // post (save) education data
                                result = httpClient.PostAsJsonAsync(ServiceURIs.ServiceEducationUri, education).Result;
                                resultContent = result.Content.ReadAsStringAsync().Result;
                            }
                        }
                    }

                    try
                    {
                        // redirect to assessment questions, if possible
                        return RedirectToAction("Create", "Assessments", new { ID = Convert.ToInt32(Request.Form["JobOpeningIDReferenceNumber"]) });
                    }
                    catch
                    {
                        return RedirectToAction("Welcome", "Home");
                    }
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

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Administrator, HiringManager")]
        public async Task<ActionResult> Details(int id = 0)
        {
            AppliedDAO applied = await ServerResponse<AppliedDAO>.GetResponseByParamsAsync(ServiceURIs.ServiceAppliedUri, id);
            ApplicantDAO applicant = await ServerResponse<ApplicantDAO>.GetResponseByParamsAsync(ServiceURIs.ServiceApplicantUri, applied.ApplicantID);
            ApplicationDAO application = await ServerResponse<ApplicationDAO>.GetResponseByParamsAsync(ServiceURIs.ServiceApplicationUri, applied.ApplicationID);
            List<EmploymentDAO> applicantEmployments = await ServerResponse<List<EmploymentDAO>>.GetResponseByParamsAsync(ServiceURIs.ServiceEmploymentUri, 0, applicant.FirstName, applicant.LastName, applicant.SSN);
            List<EducationDAO> applicantEducations = await ServerResponse<List<EducationDAO>>.GetResponseByParamsAsync(ServiceURIs.ServiceEducationUri, 0, applicant.FirstName, applicant.LastName, applicant.SSN);
            List<EmployerDAO> employers = await ServerResponse<List<EmployerDAO>>.GetResponseAsync(ServiceURIs.ServiceEmployerUri);
            List<JobDAO> jobs = await ServerResponse<List<JobDAO>>.GetResponseAsync(ServiceURIs.ServiceJobUri);
            List<SchoolDAO> schools = await ServerResponse<List<SchoolDAO>>.GetResponseAsync(ServiceURIs.ServiceSchoolUri);
            List<String> timePickerList = new List<String>();

            Dictionary<int, EmployerDAO> employersByEmployerId = new Dictionary<int, EmployerDAO>();
            foreach (var employer in employers)
            {
                employersByEmployerId[employer.EmployerID] = employer;
            }

            Dictionary<int, JobDAO> jobsByJobId = new Dictionary<int, JobDAO>();
            foreach (var job in jobs)
            {
                jobsByJobId[job.JobID] = job;
            }

            Dictionary<int, SchoolDAO> schoolsBySchoolId = new Dictionary<int, SchoolDAO>();
            foreach (var school in schools)
            {
                schoolsBySchoolId[school.SchoolID] = school;
            }

            if (applied == null)
            {
                return HttpNotFound();
            }

            // create time picker range (in hours)
            for (int i = 0; i < 24; i++)
            {
                timePickerList.Add(i.ToString());
            }

            // send all of these to the view because we are displaying entire "application"
            ViewBag.timePickerList = timePickerList;
            ViewBag.applied = applied;
            ViewBag.applicant = applicant;
            ViewBag.application = application;
            ViewBag.educations = applicantEducations;
            ViewBag.employments = applicantEmployments;
            ViewBag.employers = employers;
            ViewBag.jobs = jobs;
            ViewBag.schools = schools;
            ViewBag.employersByEmployerId = employersByEmployerId;
            ViewBag.jobsByJobId = jobsByJobId;
            ViewBag.schoolsBySchoolId = schoolsBySchoolId;
            ViewBag.generalDateFormat = generalDateFormat;
            ViewBag.generalTimeFormat = generalTimeFormat;

            return View(applied);
        }

        //
        // GET: /App/Approve

        [Authorize(Roles = "Administrator, HiringManager")]
        public ActionResult Approve(int id)
        {
            ViewBag.baseURL = Url.Content("~/");
            ViewBag.applicationID = id;
            return View();
        }

        //
        // POST: /App/Approve

        [HttpPost]
        [Authorize(Roles = "Administrator, HiringManager")]
        public ActionResult Approve(FormCollection collection)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.Form["ApplicationID"]))
                {
                    // save application form data back to database through service
                    using (HttpClient httpClient = new HttpClient())
                    {
                        httpClient.BaseAddress = new Uri("http://localhost:51309");
                        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage result = new HttpResponseMessage();
                        string resultContent = "";

                        // gather application form data
                        ApplicationDAO updatedApp = new ApplicationDAO();
                        updatedApp.ApplicationID = Convert.ToInt32(Request.Form["ApplicationID"]);
                        updatedApp.ApplicationStatus = "Approved";

                        // post (save) application data
                        result = httpClient.PostAsJsonAsync(ServiceURIs.ServiceApplicationUri, updatedApp).Result;
                        resultContent = result.Content.ReadAsStringAsync().Result;
                    }

                    return RedirectToAction("Index", "App");
                }
                else
                {
                    // TODO: validation later on...
                    return RedirectToAction("Index", "App");
                }
            }
            catch
            {
                // TODO: validation later on...
                return RedirectToAction("Index", "App");
            }
        }

        //
        // GET: /App/Reject

        [Authorize(Roles = "Administrator, HiringManager")]
        public ActionResult Reject(int id)
        {
            ViewBag.baseURL = Url.Content("~/");
            ViewBag.applicationID = id;
            return View();
        }

        //
        // POST: /App/Reject

        [HttpPost]
        [Authorize(Roles = "Administrator, HiringManager")]
        public ActionResult Reject(FormCollection collection)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.Form["ApplicationID"]))
                {
                    // save application form data back to database through service
                    using (HttpClient httpClient = new HttpClient())
                    {
                        httpClient.BaseAddress = new Uri("http://localhost:51309");
                        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage result = new HttpResponseMessage();
                        string resultContent = "";

                        // gather application form data
                        ApplicationDAO updatedApp = new ApplicationDAO();
                        updatedApp.ApplicationID = Convert.ToInt32(Request.Form["ApplicationID"]);
                        updatedApp.ApplicationStatus = "Rejected";

                        // post (save) application data
                        result = httpClient.PostAsJsonAsync(ServiceURIs.ServiceApplicationUri, updatedApp).Result;
                        resultContent = result.Content.ReadAsStringAsync().Result;
                    }

                    return RedirectToAction("Index", "App");
                }
                else
                {
                    // TODO: validation later on...
                    return RedirectToAction("Index", "App");
                }
            }
            catch
            {
                // TODO: validation later on...
                return RedirectToAction("Index", "App");
            }
        }
    }
}