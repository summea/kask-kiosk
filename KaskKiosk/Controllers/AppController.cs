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
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        /// TODO: MAKE THIS INTO A WRAPPER
        /// THIS WILL ALLOW MULTI THREADING AS WELL
        readonly string uriApplication = "http://localhost:51309/api/Application";
        readonly string uriApplicant = "http://localhost:51309/api/Applicant";
        readonly string uriApplied = "http://localhost:51309/api/Applied";
        readonly string uriEducation = "http://localhost:51309/api/Education";
        readonly string uriEmployer = "http://localhost:51309/api/Employer";
        readonly string uriEmployment = "http://localhost:51309/api/Employment";
        readonly string uriSchool = "http://localhost:51309/api/School";

        private async Task<List<ApplicationDAO>> GetApplicationsAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(uriApplication);
                return JsonConvert.DeserializeObjectAsync<List<ApplicationDAO>>(response).Result;
            }
        }
        
        private async Task<List<ApplicantDAO>> GetApplicantsAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(uriApplicant);
                return JsonConvert.DeserializeObjectAsync<List<ApplicantDAO>>(response).Result;
            }
        }

        private async Task<List<EmployerDAO>> GetEmployersAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(uriEmployer);
                return JsonConvert.DeserializeObjectAsync<List<EmployerDAO>>(response).Result;
            }
        }

        private async Task<List<SchoolDAO>> GetSchoolsAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(uriEmployer);
                return JsonConvert.DeserializeObjectAsync<List<SchoolDAO>>(response).Result;
            }
        }

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
                timePickerList.Add(i.ToString());
            }

            ViewBag.timePickerList = timePickerList;
            return View();
        }

        [HttpPost]
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
                    result = httpClient.PostAsJsonAsync(uriApplicant, applicant).Result;
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
                    result = httpClient.PostAsJsonAsync(uriApplication, application).Result;
                    resultContent = result.Content.ReadAsStringAsync().Result;

                    // get correct applicant id
                    // TODO: there is still something we might need to change about this... :)
                    var applicants = await GetApplicantsAsync();
                    applicant.ApplicantID = applicants.Last().ApplicantID;

                    // get correct application id
                    // TODO: there is still something we might need to change about this... :)
                    var applications = await GetApplicationsAsync();
                    application.ApplicationID = applications.Last().ApplicationID;

                    // Create Applied DAO;
                    AppliedDAO applied = new AppliedDAO();
                    applied.ApplicantID = applicant.ApplicantID;
                    applied.ApplicationID = application.ApplicationID;
                    applied.JobID = 1;
                    applied.DateApplied = DateTime.Now;

                    // post (save) applied data
                    result = httpClient.PostAsJsonAsync(uriApplied, applied).Result;
                    resultContent = result.Content.ReadAsStringAsync().Result;


                    // TODO: this can still be cleaned up (loop through 1..3)

                    // gather Employer data
                    // employer and employment 1
                    EmployerDAO employer = new EmployerDAO();
                    employer.Name = Request.Form["EmployerName_1"];
                    employer.EmployerAddress = Request.Form["EmployerAddress_1"];
                    employer.PhoneNumber = Request.Form["EmployerPhone_1"];

                    if (!string.IsNullOrWhiteSpace(employer.Name))
                    {
                        // TODO: check if employer already exists in database
                        // TODO: if employer exists in database: don't insert data
                        // if employer !exists in database: insert data

                        // post (save) employer data
                        result = httpClient.PostAsJsonAsync(uriEmployer, employer).Result;
                        resultContent = result.Content.ReadAsStringAsync().Result;
                    }
                    
                    // get correct employer id
                    // TODO: there is still something we might need to change about this... :)
                    var employers = await GetEmployersAsync();
                    employer.EmployerID = employers.Last().EmployerID;


                    // gather Employment data
                    EmploymentDAO employment = new EmploymentDAO();
                    employment.ApplicantID = applicant.ApplicantID;
                    employment.EmployerID = employer.EmployerID;
                    employment.MayWeContactCurrentEmployer = Convert.ToByte(Request.Form["MayWeContactCurrentEmployer_1"]);
                    employment.EmployedFrom = Convert.ToDateTime(Request.Form["EmployedFrom_1"]);
                    employment.EmployedTo = Convert.ToDateTime(Request.Form["EmployedTo_1"]);
                    employment.Supervisor = Request.Form["EmployerSupervisor_1"];
                    employment.Position = Request.Form["EmployedPosition_1"];
                    employment.StartingSalary = Request.Form["EmployedStartingSalary_1"];
                    employment.EndingSalary = Request.Form["EmployedEndingSalary_1"];
                    employment.ReasonForLeaving = Request.Form["EmployedReasonForLeaving_1"];
                    employment.Responsibilities = Request.Form["EmployedResponsibilities_1"];

                    // post (save) employment data
                    result = httpClient.PostAsJsonAsync(uriEmployment, employment).Result;
                    resultContent = result.Content.ReadAsStringAsync().Result;


                    // employer and employment 2
                    employer.Name = Request.Form["EmployerName_2"];
                    employer.EmployerAddress = Request.Form["EmployerAddress_2"];
                    employer.PhoneNumber = Request.Form["EmployerPhone_2"];

                    if (!string.IsNullOrWhiteSpace(employer.Name))
                    {
                        // TODO: check if employer already exists in database
                        // TODO: if employer exists in database: don't insert data
                        // if employer !exists in database: insert data

                        // post (save) employer data
                        result = httpClient.PostAsJsonAsync(uriEmployer, employer).Result;
                        resultContent = result.Content.ReadAsStringAsync().Result;
                    }

                    // get correct employer id
                    // TODO: there is still something we might need to change about this... :)
                    employers = await GetEmployersAsync();
                    employer.EmployerID = employers.Last().EmployerID;


                    // gather Employment data
                    employment = new EmploymentDAO();
                    employment.ApplicantID = applicant.ApplicantID;
                    employment.EmployerID = employer.EmployerID;
                    employment.MayWeContactCurrentEmployer = Convert.ToByte(Request.Form["MayWeContactCurrentEmployer_1"]);
                    employment.EmployedFrom = Convert.ToDateTime(Request.Form["EmployedFrom_1"]);
                    employment.EmployedTo = Convert.ToDateTime(Request.Form["EmployedTo_1"]);
                    employment.Supervisor = Request.Form["EmployerSupervisor_1"];
                    employment.Position = Request.Form["EmployedPosition_1"];
                    employment.StartingSalary = Request.Form["EmployedStartingSalary_1"];
                    employment.EndingSalary = Request.Form["EmployedEndingSalary_1"];
                    employment.ReasonForLeaving = Request.Form["EmployedReasonForLeaving_1"];
                    employment.Responsibilities = Request.Form["EmployedResponsibilities_1"];

                    // post (save) employment data
                    result = httpClient.PostAsJsonAsync(uriEmployment, employment).Result;
                    resultContent = result.Content.ReadAsStringAsync().Result;


                    // employer and employment 3
                    employer.Name = Request.Form["EmployerName_3"];
                    employer.EmployerAddress = Request.Form["EmployerAddress_3"];
                    employer.PhoneNumber = Request.Form["EmployerPhone_3"];

                    if (!string.IsNullOrWhiteSpace(employer.Name))
                    {
                        // TODO: check if employer already exists in database
                        // TODO: if employer exists in database: don't insert data
                        // if employer !exists in database: insert data

                        // post (save) employer data
                        result = httpClient.PostAsJsonAsync(uriEmployer, employer).Result;
                        resultContent = result.Content.ReadAsStringAsync().Result;
                    }

                    // get correct employer id
                    // TODO: there is still something we might need to change about this... :)
                    employers = await GetEmployersAsync();
                    employer.EmployerID = employers.Last().EmployerID;


                    // gather Employment data
                    employment = new EmploymentDAO();
                    employment.ApplicantID = applicant.ApplicantID;
                    employment.EmployerID = employer.EmployerID;
                    employment.MayWeContactCurrentEmployer = Convert.ToByte(Request.Form["MayWeContactCurrentEmployer_1"]);
                    employment.EmployedFrom = Convert.ToDateTime(Request.Form["EmployedFrom_1"]);
                    employment.EmployedTo = Convert.ToDateTime(Request.Form["EmployedTo_1"]);
                    employment.Supervisor = Request.Form["EmployerSupervisor_1"];
                    employment.Position = Request.Form["EmployedPosition_1"];
                    employment.StartingSalary = Request.Form["EmployedStartingSalary_1"];
                    employment.EndingSalary = Request.Form["EmployedEndingSalary_1"];
                    employment.ReasonForLeaving = Request.Form["EmployedReasonForLeaving_1"];
                    employment.Responsibilities = Request.Form["EmployedResponsibilities_1"];

                    // post (save) employment data
                    result = httpClient.PostAsJsonAsync(uriEmployment, employment).Result;
                    resultContent = result.Content.ReadAsStringAsync().Result;


                    // gather School data


                    // gather Education data


                    // TODO: this can still be cleaned up (loop through 1..3)

                    // gather School data
                    // school and education 1
                    SchoolDAO school = new SchoolDAO();
                    school.SchoolName = Request.Form["SchoolName_1"];
                    school.SchoolAddress = Request.Form["SchoolAddress_1"];

                    if (!string.IsNullOrWhiteSpace(school.SchoolName))
                    {
                        // TODO: check if school already exists in database
                        // TODO: if school exists in database: don't insert data
                        // if school !exists in database: insert data

                        // post (save) school data
                        result = httpClient.PostAsJsonAsync(uriSchool, school).Result;
                        resultContent = result.Content.ReadAsStringAsync().Result;
                    }

                    // get correct school id
                    // TODO: there is still something we might need to change about this... :)
                    var schools = await GetSchoolsAsync();
                    school.SchoolID = schools.Last().SchoolID;


                    // gather Education data
                    EducationDAO education = new EducationDAO();
                    education.ApplicantID = applicant.ApplicantID;
                    education.SchoolID = school.SchoolID;
                    education.YearsAttendedFrom = Convert.ToDateTime(Request.Form["YearsAttendedFrom_1"]);
                    education.YearsAttendedTo = Convert.ToDateTime(Request.Form["YearsAttendedTo_1"]);
                    education.Graduated = Convert.ToByte(Request.Form["Graduated_1"]);
                    education.DegreeAndMajor = Request.Form["DegreeAndMajor_1"];

                    // post (save) education data
                    result = httpClient.PostAsJsonAsync(uriEducation, education).Result;
                    resultContent = result.Content.ReadAsStringAsync().Result;


                    // school and education 2
                    school = new SchoolDAO();
                    school.SchoolName = Request.Form["SchoolName_2"];
                    school.SchoolAddress = Request.Form["SchoolAddress_2"];

                    if (!string.IsNullOrWhiteSpace(school.SchoolName))
                    {
                        // TODO: check if school already exists in database
                        // TODO: if school exists in database: don't insert data
                        // if school !exists in database: insert data

                        // post (save) school data
                        result = httpClient.PostAsJsonAsync(uriSchool, school).Result;
                        resultContent = result.Content.ReadAsStringAsync().Result;
                    }

                    // get correct school id
                    // TODO: there is still something we might need to change about this... :)
                    schools = await GetSchoolsAsync();
                    school.SchoolID = schools.Last().SchoolID;


                    // gather Education data
                    education = new EducationDAO();
                    education.ApplicantID = applicant.ApplicantID;
                    education.SchoolID = school.SchoolID;
                    education.YearsAttendedFrom = Convert.ToDateTime(Request.Form["YearsAttendedFrom_2"]);
                    education.YearsAttendedTo = Convert.ToDateTime(Request.Form["YearsAttendedTo_2"]);
                    education.Graduated = Convert.ToByte(Request.Form["Graduated_2"]);
                    education.DegreeAndMajor = Request.Form["DegreeAndMajor_2"];

                    // post (save) education data
                    result = httpClient.PostAsJsonAsync(uriEducation, education).Result;
                    resultContent = result.Content.ReadAsStringAsync().Result;


                    // school and education 3
                    school = new SchoolDAO();
                    school.SchoolName = Request.Form["SchoolName_3"];
                    school.SchoolAddress = Request.Form["SchoolAddress_3"];

                    if (!string.IsNullOrWhiteSpace(school.SchoolName))
                    {
                        // TODO: check if school already exists in database
                        // TODO: if school exists in database: don't insert data
                        // if school !exists in database: insert data

                        // post (save) school data
                        result = httpClient.PostAsJsonAsync(uriSchool, school).Result;
                        resultContent = result.Content.ReadAsStringAsync().Result;
                    }

                    // get correct school id
                    // TODO: there is still something we might need to change about this... :)
                    schools = await GetSchoolsAsync();
                    school.SchoolID = schools.Last().SchoolID;


                    // gather Education data
                    education = new EducationDAO();
                    education.ApplicantID = applicant.ApplicantID;
                    education.SchoolID = school.SchoolID;
                    education.YearsAttendedFrom = Convert.ToDateTime(Request.Form["YearsAttendedFrom_3"]);
                    education.YearsAttendedTo = Convert.ToDateTime(Request.Form["YearsAttendedTo_3"]);
                    education.Graduated = Convert.ToByte(Request.Form["Graduated_3"]);
                    education.DegreeAndMajor = Request.Form["DegreeAndMajor_3"];

                    // post (save) education data
                    result = httpClient.PostAsJsonAsync(uriEducation, education).Result;
                    resultContent = result.Content.ReadAsStringAsync().Result;
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
    }
}
