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
    public class JobRequirementServiceUnitTest
    {
        public MockJobRequirementService JQService = new MockJobRequirementService();
        List<JobRequirementDAO> JobRequirements = new List<JobRequirementDAO>();

        [TestInitialize]
        public void Initialize()
        {
            JQService.SetUp();
        }

        [TestMethod]
        [Description("Happy Test")]
        public void HappyTest_GetJobRequirementByID()
        {
            Assert.IsNotNull(JQService.GetJobRequirementByID(0));
            Assert.AreEqual(JQService.GetJobRequirementByID(0).Notes, "Note 0");
        }

        [TestMethod]
        [Description("Fail Test GetJobRequirementByID")]
        [ExpectedException(typeof(Exception))]
        public void FailTest_GetJobRequirementByID()
        {
            Assert.IsNull(JQService.GetJobRequirementByID(9));
        }

        [TestMethod]
        public void Test_GetJobRequirements()
        {
            Assert.IsNotNull(JQService.GetJobRequirements());
            Assert.AreEqual(JQService.GetJobRequirements().Count, 3);
        }

        [TestMethod]
        public void Test_CreateJobRequirement()
        {
            JobRequirementDAO JobReq3 = new JobRequirementDAO() { ID = 3, JobRequirementID = 3, JobOpeningID = 3, SkillID = 3, Notes = "Note 3" };

            Assert.IsTrue(JQService.CreateJobRequirement(JobReq3));
            Assert.AreEqual(JQService.GetJobRequirements().Count, 4);
        }

        [TestMethod]
        public void Test_UpdateJobRequirement()
        {
            JobRequirementDAO JobReq3 = new JobRequirementDAO() { ID = 3, JobRequirementID = 2, JobOpeningID = 3, SkillID = 3, Notes = "Note 3" };

            Assert.IsTrue(JQService.UpdateJobRequirement(JobReq3));
            Assert.AreEqual(JQService.GetJobRequirementByID(2).SkillID, 3);
        }

        [TestMethod]
        public void Test_DeleteJobRequirement()
        {
            Assert.IsTrue(JQService.DeleteJobRequirement(2));
            Assert.AreEqual(JQService.GetJobRequirements().Count, 2);
        }
    }
}
