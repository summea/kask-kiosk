using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Kask.Services.DAO;
using Kask.Services.Interfaces;
/*
namespace Kask.UnitTests
{
    [TestClass]
    public class ApplicationServiceUnitTest
    {
        public MockApplicationService applicationService;
        public MockApplicantService applicantService;

        ApplicantDAO applicant1 = new ApplicantDAO() { ApplicantID = 1, FirstName = "Sam1", LastName = "Edy1", Phone = "000 000 0000", SSN = "1111" };
        ApplicantDAO applicant2 = new ApplicantDAO() { ApplicantID = 2, FirstName = "Sam2", LastName = "Edy2", Phone = "222 222 2222", SSN = "2222" };


        [TestInitialize]
        public void Initialize()
        {
            applicationService = new MockApplicationService();
            applicationService.SetUp();
            applicantService = new MockApplicantService();
            applicantService.SetUp();

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

            // TODO: This MockApplicationService function is wrong.
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
    }
}
*/