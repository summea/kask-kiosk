using System;
using System.Collections.Generic;
using Kask.Services.DAO;
using Kask.Services.Interfaces;

namespace Kask.UnitTests
{
    class MockApplicationService: IApplicationService
    {
        List<ApplicationDAO> Applications = new List<ApplicationDAO>();
        List<ApplicantDAO> Applicants = new List<ApplicantDAO>();
       
        internal void SetUp()
        {
            ApplicationDAO applicationA = new ApplicationDAO();
            ApplicationDAO applicationB = new ApplicationDAO();
            ApplicationDAO applicationC = new ApplicationDAO();
            ApplicationDAO applicationD = new ApplicationDAO();
            ApplicationDAO applicationE = new ApplicationDAO();

            initializeApplication(applicationA, "submitted", "1111", 1, 1);
            initializeApplication(applicationB, "Updated", "2222", 2, 2);
            initializeApplication(applicationC, "Declined", "3333", 3, 3);
            initializeApplication(applicationD, "Submitted", "4444", 4, 4);
            initializeApplication(applicationE, "Rejected", "9999", 9, 9); 
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
            ApplicationDAO application = new ApplicationDAO() { ApplicationID = 5, ApplicationStatus = "Reviewed" };
            List<ApplicationDAO> list = new List<ApplicationDAO>();
            list.Add(application);
            return list;
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

        private void initializeApplication(ApplicationDAO application, string status, string salary, byte otherVals, int time)
        {
            //ApplicationDAO application = new ApplicationDAO();
            application.ApplicationID = otherVals;
            application.ApplicationStatus = status;
            application.SalaryExpectation = salary;
            application.FullTime = otherVals;
            application.AvailableForDays = otherVals;
            application.AvailableForEvenings = otherVals;
            application.AvailableForWeekends = otherVals;
            application.MondayFrom = System.TimeSpan.FromHours(Convert.ToDouble(time));
            application.TuesdayFrom = System.TimeSpan.FromHours(Convert.ToDouble(time + 1));
            application.WednesdayFrom = System.TimeSpan.FromHours(Convert.ToDouble(time + 3));
            application.ThursdayFrom = System.TimeSpan.FromHours(Convert.ToDouble(time + 4));
            application.FridayFrom = System.TimeSpan.FromHours(Convert.ToDouble(time + 5));
            application.SaturdayFrom = System.TimeSpan.FromHours(Convert.ToDouble(time + 6));
            application.SundayFrom = System.TimeSpan.FromHours(Convert.ToDouble(time + 7));
            application.MondayTo = System.TimeSpan.FromHours(Convert.ToDouble(time + 8));
            application.TuesdayTo = System.TimeSpan.FromHours(Convert.ToDouble(time + 9));
            application.WednesdayTo = System.TimeSpan.FromHours(Convert.ToDouble(time + 10));
            application.ThursdayTo = System.TimeSpan.FromHours(Convert.ToDouble(time + 11));
            application.FridayTo = System.TimeSpan.FromHours(Convert.ToDouble(time + 12));
            application.SaturdayTo = System.TimeSpan.FromHours(Convert.ToDouble(time + 13));
            application.SundayTo = System.TimeSpan.FromHours(Convert.ToDouble(time + 14));
        }
    }
}
