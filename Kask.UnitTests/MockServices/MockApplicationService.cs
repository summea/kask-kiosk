using System;
using System.Collections.Generic;
using Kask.Services.DAO;
using Kask.Services.Interfaces;

namespace Kask.UnitTests
{
    public class MockApplicationService: IApplicationService
    {
        List<ApplicationDAO> Applications = new List<ApplicationDAO>();
       
        internal void SetUp()
        {
            ApplicationDAO applicationA = new ApplicationDAO() { ID = 1, ApplicationID = 1, ApplicationStatus = "Rejected", SalaryExpectation = "20.00" };
            ApplicationDAO applicationB = new ApplicationDAO() { ID = 2, ApplicationID = 2, ApplicationStatus = "Accepted", SalaryExpectation = "21.00" };
            ApplicationDAO applicationC = new ApplicationDAO() { ID = 3, ApplicationID = 3, ApplicationStatus = "Reviewed", SalaryExpectation = "22.00" };

            Applications.Add(applicationA);
            Applications.Add(applicationB);
            Applications.Add(applicationC);
        }

        public ApplicationDAO GetApplicationByID(int id)
        {
            foreach (var application in Applications)
                if (application.ApplicationID == id)
                    return application;
            throw new Exception("Applicant not found");
        }

        public IList<ApplicationDAO> GetApplicationsByName(string first, string last, string ssn)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicationDAO> GetApplications()
        {
            return Applications;
        }

        public bool CreateApplication(ApplicationDAO app)
        {
            Applications.Add(app);
            return true;
        }

        public bool UpdateApplication(ApplicationDAO newApp)
        {
            foreach (var application in Applications)
                if (application.ApplicationID == newApp.ApplicationID)
                {
                    Applications.Remove(application);
                    Applications.Add(newApp);
                    return true;
                }
            return false;
        }

        public bool DeleteApplication(int ID)
        {
            foreach (var application in Applications)
                if (application.ApplicationID == ID)
                {
                    Applications.Remove(application);
                    return true;
                }
            return false;
        }

        private void initializeApplication(ApplicationDAO application, int id, int applicationID, string applicationStatus, string salaryExpect )
        {
            //ApplicationDAO application = new ApplicationDAO();
            application.ID = id;
            application.ApplicationID = applicationID;
            application.ApplicationStatus = applicationStatus;
            application.SalaryExpectation = salaryExpect;
            application.FullTime = null;
            application.AvailableForDays = null;
            application.AvailableForEvenings = null;
            application.AvailableForWeekends = null;
            application.MondayFrom = null;
            application.TuesdayFrom = null;
            application.WednesdayFrom = null;
            application.ThursdayFrom = null;
            application.FridayFrom = null;
            application.SaturdayFrom = null;
            application.SundayFrom = null;
            application.MondayTo = null;
            application.TuesdayTo = null;
            application.WednesdayTo = null;
            application.ThursdayTo = null;
            application.FridayTo = null;
            application.SaturdayTo = null;
            application.SundayTo = null;
        }
    }
}
