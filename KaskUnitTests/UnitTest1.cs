using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Kask.DAL.Models;
using Kask.Services.Interfaces;
using KaskUnitTests;


namespace KaskUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private IApplicantService applicantService;

        [TestInitialize]
        public void Initialize()
        {
            applicantService = new MockApplicantService();
        }


        [TestMethod]
        public void Test_ApplicantService()
        {
            applicantService.CreateApplicant(new Applicant(){Applicant_ID = 1, FirstName = "Sam", LastName = "Edy", SSN = "1234"});
            Assert.AreEqual(applicantService.GetApplicantByID(1).Applicant_ID, 1);
        }
    }
}
