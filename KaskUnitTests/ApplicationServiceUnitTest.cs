using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Kask.Services.DAO;
using Kask.Services.Interfaces;

namespace Kask.UnitTests
{
    [TestClass]
    public class ApplicationServiceUnitTest
    {
        public IApplicationService applicationService;
        public IApplicantService applicantService;
        public ApplicationDAO applicationA;
        public ApplicationDAO applicationB;
        public ApplicationDAO applicationC;
        public ApplicationDAO applicationD;
        public ApplicationDAO applicationE;
        ApplicantDAO applicant1 = new ApplicantDAO() { ApplicantID = 1, FirstName = "Sam1", LastName = "Edy1", Phone = "000 000 0000", SSN = "1111" };
        ApplicantDAO applicant2 = new ApplicantDAO() { ApplicantID = 2, FirstName = "Sam2", LastName = "Edy2", Phone = "222 222 2222", SSN = "2222" };


        [TestInitialize]
        public void Initialize()
        {
            applicationService = new MockApplicationService();
            applicantService = new MockApplicantService();

            applicationA = new ApplicationDAO();
            applicationB = new ApplicationDAO();
            applicationC = new ApplicationDAO();
            applicationD = new ApplicationDAO();
            applicationE = new ApplicationDAO();

            initializeApplication(applicationA, "submitted", "1111", 1, 1);
            initializeApplication(applicationB, "Updated", "2222", 2, 2);
            initializeApplication(applicationC, "Declined", "3333", 3, 3);
            initializeApplication(applicationD, "Submitted", "4444", 4, 4);
            initializeApplication(applicationE, "Rejected", "9999", 9, 9); 
        }

        [TestMethod]
        public void Test_GetApplicationByID()
        {
            applicationService.CreateApplication(applicationA);
            applicationService.CreateApplication(applicationB);

            List<ApplicationDAO> list = new List<ApplicationDAO>();
            list.Add(applicationA);
            list.Add(applicationB);

            Assert.AreEqual(list.Count, 2);
            Assert.IsNotNull(applicationService.GetApplicationByID(1));
            Assert.IsNotNull(applicationService.GetApplicationByID(2));
            Assert.AreSame(applicationService.GetApplicationByID(1), applicationA);
            Assert.AreEqual(applicationService.GetApplicationByID(2), applicationB);
        }

        [TestMethod]
        public void Test_GetApplicationsByName()
        {
            applicationService.CreateApplication(applicationA);

            applicantService.CreateApplicant(applicant1);
           // applicantService.CreateApplicant(applicant2);
            
            IList<ApplicationDAO> list = new List<ApplicationDAO>();
            IList<ApplicationDAO> list2 = new List<ApplicationDAO>();

            list.Add(applicationService.GetApplicationByID(applicant1.ApplicantID));

            list2 = applicationService.GetApplicationsByName(applicant1.FirstName, applicant1.LastName, applicant1.SSN) ;

            Assert.AreEqual(list, list2);
        }

        [TestMethod]
        public void Test_CreateApplication()
        {
            applicationService.CreateApplication(applicationA);
            applicationService.CreateApplication(applicationB);
            applicationService.CreateApplication(applicationC);
            applicationService.CreateApplication(applicationD);

            Assert.AreEqual(applicationService.GetApplicationByID(1), applicationA);
            Assert.AreEqual(applicationService.GetApplicationByID(2), applicationB);
            Assert.AreEqual(applicationService.GetApplicationByID(3), applicationC);
            Assert.AreEqual(applicationService.GetApplicationByID(4), applicationD);
        }

        [TestMethod]
        public void Test_UpdateApplication()
        {
            applicationService.UpdateApplication(applicationE);

            Assert.AreEqual(applicationE.ApplicationStatus, "Rejected");
        }

        [TestMethod]
        public void Test_DeleteApplication()
        {
            applicationService.CreateApplication(applicationC);
            applicationService.CreateApplication(applicationD);
            applicationService.CreateApplication(applicationE);

            
            IList<ApplicationDAO> list = new List<ApplicationDAO>();
            list = applicationService.GetApplications();

            Assert.AreEqual(list.Count, 3);

            applicationService.DeleteApplication(4);

            Assert.AreEqual(list.Count, 2);
            Assert.IsNotNull(applicationService.GetApplicationByID(3), "ApplicationC exist");
            Assert.IsNotNull(applicationService.GetApplicationByID(9), "ApplicationE exist");
        }

        void initializeApplication( ApplicationDAO application, string status, string salary, byte otherVals, int time)
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
            application.TuesdayFrom = System.TimeSpan.FromHours(Convert.ToDouble(time+1));
            application.WednesdayFrom = System.TimeSpan.FromHours(Convert.ToDouble(time+3));
            application.ThursdayFrom = System.TimeSpan.FromHours(Convert.ToDouble(time+4));
            application.FridayFrom = System.TimeSpan.FromHours(Convert.ToDouble(time+5));
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
