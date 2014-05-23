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
    public class JobOpenQuestionServiceUnitTest
    {
        public MockJobOpeningQuestionService jobOpenQuestionService = new MockJobOpeningQuestionService();
        List<JobOpeningInterviewQuestionDAO> JOIQs = new List<JobOpeningInterviewQuestionDAO>();

        [TestInitialize]
        public void Initialize()
        {
            jobOpenQuestionService.SetUp();
        }

        [TestMethod]
        [Description("Happy Test GetJobOpeningInterviewQuestionByID")]
        public void Test_GetJobOpeningInterviewQuestionByID()
        {
            Assert.IsNotNull(jobOpenQuestionService.GetJobOpeningInterviewQuestionByID(1));
            Assert.AreEqual(jobOpenQuestionService.GetJobOpeningInterviewQuestionByID(1).JobOpeningInterviewQuestionID, 1);
        }

        [TestMethod]
        [Description("Fail Test GetJobOpeningInterviewQuestionByID")]
        [ExpectedException(typeof(Exception))]
        public void FailTest_GetJobOpeningInterviewQuestionByID()
        {
            Assert.IsNull(jobOpenQuestionService.GetJobOpeningInterviewQuestionByID(9));
        }

        [TestMethod]
        [Description("Test GetAllJOIQ")]
        public void GetJobOpeningInterviewQuestions()
        {
            Assert.IsNotNull(jobOpenQuestionService.GetJobOpeningInterviewQuestions());
            Assert.AreEqual(jobOpenQuestionService.GetJobOpeningInterviewQuestions().Count, 3);
        }

        [TestMethod]
        public void Test_CreateJobOpeningInterviewQuestion()
        {
            JobOpeningInterviewQuestionDAO JOIQ3 = new JobOpeningInterviewQuestionDAO() { ID = 3, JobOpeningInterviewQuestionID = 3, JobOpeningID = 3, SAQuestionID = 3 };

            Assert.IsTrue(jobOpenQuestionService.CreateJobOpeningInterviewQuestion(JOIQ3));
            Assert.AreEqual(jobOpenQuestionService.GetJobOpeningInterviewQuestionByID(3).JobOpeningInterviewQuestionID, 3);
        }

        [TestMethod]
        public void Test_UpdateJobOpeningInterviewQuestion()
        {
            JobOpeningInterviewQuestionDAO JOIQ3 = new JobOpeningInterviewQuestionDAO() { ID = 3, JobOpeningInterviewQuestionID = 2, JobOpeningID = 3, SAQuestionID = 3 };

            Assert.IsTrue(jobOpenQuestionService.UpdateJobOpeningInterviewQuestion(JOIQ3));
            Assert.AreEqual(jobOpenQuestionService.GetJobOpeningInterviewQuestions().Count, 3);
            //Assert.AreEqual(jobOpenQuestionService.GetJobOpeningInterviewQuestionByID(2).ID, 3);
        }

        [TestMethod]
        public void Test_DeleteJobOpeningInterviewQuestion()
        {
            Assert.IsTrue(jobOpenQuestionService.DeleteJobOpeningInterviewQuestion(2));
            Assert.AreEqual(jobOpenQuestionService.GetJobOpeningInterviewQuestions().Count, 2);
        }
    }
}
