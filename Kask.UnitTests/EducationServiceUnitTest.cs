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
    public class EducationServiceUnitTest
    {
        public MockEducationService educationService = new MockEducationService();
        List<EducationDAO> Educations = new List<EducationDAO>();

        [TestInitialize]
        public void Initialize()
        {
            educationService.SetUp();
        }

        [TestMethod]
        [Description("Happy GetEducationByID")]
        public void Test_GetEducationByID()
        {
            Assert.IsNotNull(educationService.GetEducationByID(1));
        }

        [TestMethod]
        [Description("Fail GetApplicationByID")]
        [ExpectedException(typeof(Exception))]
        public void Test_GetEducationByIDFail()
        {
            Assert.IsNull(educationService.GetEducationByID(8));
        }

        [TestMethod]
        public void Test_GetEducations()
        {
            List<EducationDAO> list = educationService.GetEducations() as List<EducationDAO>;
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void Test_CreateEducation()
        {
            EducationDAO education5 = new EducationDAO() { ID = 5, ApplicantID = 5, EducationID = 1, SchoolID = 1, DegreeAndMajor = "BIO" };
            Educations.Add(education5);
            Assert.AreEqual(education5.EducationID, 1);
            Assert.AreEqual(education5.ID, 5);
            Assert.AreEqual(education5.DegreeAndMajor, "BIO");
        }

        [TestMethod]
        public void Test_UpdateEducation()
        {
            EducationDAO education4 = new EducationDAO() { ID = 7, ApplicantID = 7, EducationID = 4, SchoolID = 7, DegreeAndMajor = "TEST" };
            educationService.UpdateEducation(education4);
            Assert.AreEqual(education4.ID, 7);
            Assert.AreEqual(education4.DegreeAndMajor, "TEST");
        }

        [TestMethod]
        public void Test_DeleteEducation()
        {
            Assert.IsTrue(educationService.DeleteEducation(3));
        }
    }
}
