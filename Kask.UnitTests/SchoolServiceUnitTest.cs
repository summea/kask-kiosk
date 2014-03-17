using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kask.Services.DAO;
using Kask.Services.Interfaces;
using Kask.UnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kask.UnitTests
{
    [TestClass]
    public class SchoolServiceUnitTest
    {
        public MockSchoolService schoolService = new MockSchoolService();
        List<SchoolDAO> Schools = new List<SchoolDAO>();

        [TestInitialize]
        public void Initialize()
        {
            schoolService.SetUp();
        }

        [TestMethod]
        [Description("Happy GetSchoolByID")]
        public void Test_GetSchoolByID()
        {
            Assert.IsNotNull(schoolService.GetSchoolByID(1));
        }

        [TestMethod]
        [Description("Fail GetSChoolByID")]
        [ExpectedException(typeof(Exception))]
        public void Test_GetSchoolByIDFail()
        {
            Assert.IsNull(schoolService.GetSchoolByID(8));
        }

        [TestMethod]
        public void Test_GetSchools()
        {
            List<SchoolDAO> list = schoolService.GetSchools() as List<SchoolDAO>;
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void Test_CreateSchool()
        {
            SchoolDAO school4 = new SchoolDAO() { ID = 4, SchoolID = 4, SchoolName = "Name4", SchoolAddress = "Address4" };
            schoolService.CreateSchool(school4);
            Assert.AreEqual(schoolService.GetSchools().Count, 4);
            Assert.AreEqual(schoolService.GetSchoolByID(4).SchoolAddress, "Address4");
        }

        [TestMethod]
        public void Test_UpdateSchool()
        {
            SchoolDAO school3 = new SchoolDAO() { ID = 3, SchoolID = 3, SchoolName = "TestName", SchoolAddress = "TestAddress" };
            schoolService.UpdateSchool(school3);
            Assert.AreEqual(schoolService.GetSchoolByID(3).SchoolName, "TestName");
            Assert.AreEqual(schoolService.GetSchoolByID(3).SchoolAddress, "TestAddress");
        }

        [TestMethod]
        public void Test_DeleteSchool()
        {
            Assert.IsTrue(schoolService.DeleteSchool(3));
            Assert.AreEqual(schoolService.GetSchools().Count, 2);
        }
    }
}
