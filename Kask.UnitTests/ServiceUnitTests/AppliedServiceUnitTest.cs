using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kask.Services.DAO;
using Kask.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kask.UnitTests
{
    [TestClass]
    public class AppliedServiceUnitTest
    {
        public MockAppliedService appliedService = new MockAppliedService();
        List<AppliedDAO> Applieds = new List<AppliedDAO>();

        [TestInitialize]
        public void Initialize()
        {
            appliedService.SetUp();
        }

        [TestMethod]
        [Description("Happy GetAppliedByID")]
        public void HappyTest_GetAppliedByID()
        {
            Assert.IsNotNull(appliedService.GetAppliedByID(1));
        }

        [TestMethod]
        [Description("FailTest GetAppliedByID")]
        [ExpectedException(typeof(Exception))]
        public void FailTest_GetAppliedByID()
        {
            Assert.IsNull(appliedService.GetAppliedByID(4));
        }

        [TestMethod]
        public void Test_GetApplieds()
        {
            List<AppliedDAO> list = appliedService.GetApplieds() as List<AppliedDAO>;
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void Test_CreateApplied()
        {
            AppliedDAO applied4 = new AppliedDAO() { AppliedID = 4, ApplicantID = 4, ApplicationID = 4, JobID = 4 };
            appliedService.CreateApplied(applied4);
            Assert.AreEqual(appliedService.GetApplieds().Count, 4);
            Assert.IsNotNull(appliedService.GetAppliedByID(4));
        }

        [TestMethod]
        public void Test_UpdateApplied()
        {
            AppliedDAO applied4 = new AppliedDAO() { AppliedID = 3, ApplicantID = 3, ApplicationID = 3, JobID = 4 };
            appliedService.UpdateApplied(applied4);
            Assert.AreEqual(appliedService.GetAppliedByID(3).AppliedID, 3);
            Assert.AreEqual(appliedService.GetAppliedByID(3).JobID, 4);
        }

        [TestMethod]
        public void Test_DeleteApplied()
        {
            Assert.IsTrue(appliedService.DeleteApplied(2));
            Assert.AreEqual(appliedService.GetApplieds().Count, 2);

        }
    }
}
