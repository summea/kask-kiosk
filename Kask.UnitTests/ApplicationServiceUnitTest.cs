using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Kask.Services.DAO;
using Kask.Services.Interfaces;
using Kask.UnitTests;

namespace Kask.UnitTests
{
    [TestClass]
    public class ApplicationServiceUnitTest
    {
        public MockApplicationService applicationService = new MockApplicationService();
        List<ApplicationDAO> Applications = new List<ApplicationDAO>();
        
        [TestInitialize]
        public void Initialize()
        {
            applicationService.SetUp();

        }

        [TestMethod]
        [Description("Happy Test_GetApplicationByID")]
        public void Test_GetApplicationByID()
        {            
            Assert.IsNotNull(applicationService.GetApplicationByID(1));
        }
        
        [TestMethod]
        [Description("Fail GetApplicationByID")]
        [ExpectedException(typeof(Exception))]
        public void Test_GetEducationByIDFail()
        {
            Assert.IsNull(applicationService.GetApplicationByID(8));
        }


        [TestMethod]
        public void Test_CreateApplication()
        {
            ApplicationDAO application4 = new ApplicationDAO() { ID = 4, ApplicationID = 4, ApplicationStatus = "TestStatus", SalaryExpectation = "TestSalary" };
            applicationService.CreateApplication(application4);
            Assert.AreEqual(applicationService.GetApplicationByID(4).SalaryExpectation, "TestSalary");
            Assert.AreEqual(applicationService.GetApplications().Count, 4);
        }

        [TestMethod]
        public void Test_UpdateApplication()
        {
            ApplicationDAO application4 = new ApplicationDAO() { ID = 3, ApplicationID = 3, ApplicationStatus = "TestStatus2", SalaryExpectation = "TestSalary2" };
            applicationService.UpdateApplication(application4);
            Assert.AreEqual(applicationService.GetApplicationByID(3).SalaryExpectation, "TestSalary2");
            Assert.AreEqual(applicationService.GetApplications().Count, 3);
        }

        [TestMethod]
        public void Test_DeleteApplication()
        {
            Assert.IsTrue(applicationService.DeleteApplication(3));
            Assert.AreEqual(applicationService.GetApplications().Count, 2);
        }
    }
}
