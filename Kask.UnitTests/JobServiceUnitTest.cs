using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kask.Services.DAO;
using Kask.Services.Interfaces;
using Kask.UnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KaskUnitTests
{
    [TestClass]
    public class JobServiceUnitTest
    {
        public MockJobService jobService = new MockJobService();
        List<JobDAO> Jobs = new List<JobDAO>();

        [TestInitialize]
        public void Initialize()
        {
            jobService.SetUp();
        }

        [TestMethod]
        [Description("Happy GetJobByID")]
        public void Test_GetJobByID()
        {
            Assert.IsNotNull(jobService.GetJobByID(1));
        }

        [TestMethod]
        [Description("Fail GetJobByID")]
        [ExpectedException(typeof(Exception))]
        public void Test_GetJobByIDFail()
        {
            Assert.IsNull(jobService.GetJobByID(8));
        }

        [TestMethod]
        public void Test_GetJobs()
        {
            List<JobDAO> list = jobService.GetJobs() as List<JobDAO>;
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void Test_CreateJob()
        {
            JobDAO job4 = new JobDAO() { ID = 4, JobID = 4, Title = "TestJob" };
            jobService.CreateJob(job4);
            Assert.AreEqual(jobService.GetJobByID(4).ID, 4);
            Assert.AreEqual(jobService.GetJobByID(4).JobID, 4);
            Assert.AreEqual(jobService.GetJobByID(4).Title, "TestJob");
            Assert.AreEqual(jobService.GetJobs().Count, 4);
        }

        [TestMethod]
        public void Test_UpdateJob()
        {
            JobDAO job5 = new JobDAO() { ID = 3, JobID = 3, Title = "TestTitle" };
            jobService.UpdateJob(job5);
            Assert.AreEqual(jobService.GetJobByID(3).Title, "TestTitle");
            Assert.AreEqual(jobService.GetJobByID(3).ID, 3);
            Assert.AreEqual(jobService.GetJobs().Count, 3);
        }

        [TestMethod]
        public void Test_DeleteJob()
        {
            Assert.IsTrue(jobService.DeleteJob(3));
            Assert.AreEqual(jobService.GetJobs().Count, 2);
        }
    }
}
