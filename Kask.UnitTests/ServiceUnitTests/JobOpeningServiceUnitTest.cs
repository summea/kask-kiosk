using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kask.Services.DAO;
using Kask.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KaskUnitTests.MockServices;

namespace Kask.UnitTests
{
    [TestClass]
    public class JobOpeningServiceUnitTest
    {
        public MockJobOpeningService jobOpeningService = new MockJobOpeningService();
        List<JobOpeningDAO> JobOpenings = new List<JobOpeningDAO>();

        [TestInitialize]
        public void Initialize()
        {
            jobOpeningService.SetUp();
        }

        [TestMethod]
        [Description("Happy test")]
        public void HappyTest_GetJobOpeningByID()
        {
            Assert.IsNotNull(jobOpeningService.GetJobOpeningByID(1));
        }

        [TestMethod]
        [Description("Fail GetJobOpeningByID")]
        [ExpectedException(typeof(Exception))]
        public void Test_FailGetJobOpeningByID()
        {
            Assert.IsNull(jobOpeningService.GetJobOpeningByID(5));
        }

        [TestMethod]
        public void Test_GetJobOpenings()
        {
            List<JobOpeningDAO> list = jobOpeningService.GetJobOpenings() as List<JobOpeningDAO>;
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void Test_CreateJobOpening()
        {
            JobOpeningDAO jobopening4 = new JobOpeningDAO() { JobOpeningID = 4, JobID = 4, Approved = 1 };
            jobOpeningService.CreateJobOpening(jobopening4);
            Assert.IsNotNull(jobOpeningService.GetJobOpeningByID(4));
            Assert.AreEqual(jobOpeningService.GetJobOpenings().Count, 4);
        }

        [TestMethod]
        public void Test_UpdateJobOpening()
        {
            JobOpeningDAO jobopening4 = new JobOpeningDAO() { JobOpeningID = 3, JobID = 4, Approved = 0 };
            jobOpeningService.UpdateJobOpening(jobopening4);
            Assert.AreEqual(jobOpeningService.GetJobOpeningByID(3).Approved, 0);
            Assert.AreEqual(jobOpeningService.GetJobOpenings().Count, 3);
        }

        [TestMethod]
        public void Test_DeleteJobOpening()
        {
            jobOpeningService.DeleteJobOpening(3);
            Assert.AreEqual(jobOpeningService.GetJobOpenings().Count, 2);
        }
    }
}
