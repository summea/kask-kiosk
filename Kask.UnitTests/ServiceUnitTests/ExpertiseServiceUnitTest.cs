using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kask.Services.DAO;
using Kask.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KaskUnitTests.MockServices;

namespace KaskUnitTests.ServiceUnitTests
{
    [TestClass]
    public class ExpertiseServiceUnitTest
    {
        public MockExpertiseService expertiseService = new MockExpertiseService();
        List<ExpertiseDAO> Expertises = new List<ExpertiseDAO>();

        [TestInitialize]
        public void Initialize()
        {
            expertiseService.SetUp();
        }

        [TestMethod]
        [Description("Happy test")]
        public void HappyTest_GetExpertiseByID()
        {
            Assert.IsNotNull(expertiseService.GetExpertiseByID(1));
            Assert.AreEqual(expertiseService.GetExpertiseByID(1).ApplicantID, 1);
        }

        [TestMethod]
        [Description("Fail GetExpertiseByID")]
        [ExpectedException(typeof(Exception))]
        public void Test_FailExpertiseByID()
        {
            Assert.IsNull(expertiseService.GetExpertiseByID(5));
        }

        [TestMethod]
        public void Test_GetExpertises()
        {
            List<ExpertiseDAO> list = expertiseService.GetExpertises() as List<ExpertiseDAO>;
            Assert.IsNotNull(list);
            Assert.AreEqual(list.Count, 3);
        }

        [TestMethod]
        public void Test_CreateExpertise()
        {
            ExpertiseDAO expertise4 = new ExpertiseDAO() { ID = 4, ExpertiseID = 4, ApplicantID = 4, SkillID = 4 };
            expertiseService.CreateExpertise(expertise4);
            Assert.IsNotNull(expertiseService.GetExpertiseByID(3));
            Assert.AreEqual(expertiseService.GetExpertises().Count, 4);
        }

        [TestMethod]
        public void Test_UpdateExpertise()
        {
            ExpertiseDAO expertise4 = new ExpertiseDAO() { ID = 4, ExpertiseID = 3, ApplicantID = 4, SkillID = 4 };
            expertiseService.UpdateExpertise(expertise4);
            Assert.AreEqual(expertiseService.GetExpertiseByID(3).ApplicantID, 4);
            Assert.AreEqual(expertiseService.GetExpertises().Count, 3);
        }

        [TestMethod]
        public void Test_DeleteExpertise()
        {
            expertiseService.DeleteExpertise(3);
            Assert.AreEqual(expertiseService.GetExpertises().Count, 2);
        }
    }
}
