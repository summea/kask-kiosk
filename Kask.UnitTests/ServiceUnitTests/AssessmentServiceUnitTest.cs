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
    public class AssessmentServiceUnitTest
    {
        public MockAssessmentService assessmentService = new MockAssessmentService();
        List<AssessmentDAO> Assessments = new List<AssessmentDAO>();

        [TestInitialize]
        public void Initialize()
        {
            assessmentService.SetUp();
        }

        [TestMethod]
        [Description("Happy GetAssessmentByID")]
        public void Test_GetAssessmentByID()
        {
            Assert.IsNotNull(assessmentService.GetAssessmentByID(1));
        }

        [TestMethod]
        [Description("Fail GetAssessmentByID")]
        [ExpectedException(typeof(Exception))]
        public void Test_GetAssessmentByIDFail()
        {
            Assert.IsNull(assessmentService.GetAssessmentByID(6));
        }

        [TestMethod]
        public void Test_GetAssessments()
        {
            List<AssessmentDAO> list = assessmentService.GetAssessments() as List<AssessmentDAO>;
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void Test_CreateAssessment()
        {
            AssessmentDAO assessment4 = new AssessmentDAO() { ID = 4, AssessmentID = 4, ApplicantID = 4, QuestionBankID = 4 };
            
            Assert.AreEqual(assessmentService.CreateAssessment(assessment4), true);
            Assert.AreEqual(assessment4.AssessmentID, 4);
            Assert.IsNotNull(assessmentService.GetAssessmentByID(4));
        }

        [TestMethod]
        public void Test_UpdateAssessment()
        {
            AssessmentDAO assessment4 = new AssessmentDAO() { ID = 5, AssessmentID = 3, ApplicantID = 5, QuestionBankID = 5 };

            assessmentService.UpdateAssessment(assessment4);
            Assert.AreEqual(assessmentService.GetAssessmentByID(3).ApplicantID, 5);
        }

        [TestMethod]
        public void Test_DeleteAssessment()
        {
            Assert.IsTrue(assessmentService.DeleteAssessment(3));
            Assert.AreEqual(assessmentService.GetAssessments().Count, 2);
        }
    }
}
