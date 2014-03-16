using Kask.Services.DAO;
using Kask.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Kask.UnitTests
{
    [TestClass]
    public class ApplicantServiceUnitTest
    {
        public MockApplicantService applicantService = new MockApplicantService();
        List<ApplicantDAO> Applicants = new List<ApplicantDAO>();        
        
        [TestInitialize]
        public void Initialize()
        {
            applicantService.SetUp();
        }

        [TestMethod]
        [Description("Happy GetApplicantID")]
        public void Test_GetApplicantByID()
        {            
            Assert.IsNotNull(applicantService.GetApplicantByID(1));            
        }

        [TestMethod]
        [Description("Fail GetApplicantByID")]
        [ExpectedException(typeof(Exception))]
        public void Test_GetApplicantByIDFail()
        {
            Assert.IsNull(applicantService.GetApplicantByID(6));
        }

        [TestMethod]
        public void Test_GetApplicants()
        {
            List<ApplicantDAO> list = applicantService.GetApplicants() as List<ApplicantDAO>;
            Assert.IsNotNull(list); 
        }

        [TestMethod]
        public void Test_CreateApplicant()
        {
            ApplicantDAO applicant6 = new ApplicantDAO() { ApplicantID = 6, FirstName = "TestFirst", LastName = "TestLast", Phone = "TestNum", SSN = "TestSSN" };
            Applicants.Add(applicant6);
            Assert.AreEqual(applicant6.ApplicantID, 6);
        }

        [TestMethod]
        public void Test_UpdateApplicant()
        {
            ApplicantDAO applicant4 = new ApplicantDAO() { ApplicantID = 4, FirstName = "NewFirst4", LastName = "NewLast4", Phone = "4444", SSN = "0005" };

            applicantService.UpdateApplicant(applicant4);
            Assert.AreEqual(applicant4.ApplicantID, 4);
            Assert.AreEqual(applicant4.FirstName, "NewFirst4");
            Assert.AreEqual(applicant4.LastName, "NewLast4");
            Assert.AreEqual(applicant4.SSN, "0005");
            Assert.AreEqual(applicant4.Phone, "4444");
            
        }

        [TestMethod]
        public void Test_DeleteApplicant()
        {            
            Assert.IsTrue(applicantService.DeleteApplicant(4));
        }
    }
}
