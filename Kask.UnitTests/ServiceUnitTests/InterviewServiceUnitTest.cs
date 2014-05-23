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
    public class InterviewServiceUnitTest
    {        
        public MockInterviewService interviewService = new MockInterviewService();
        List<InterviewDAO> Interviews = new List<InterviewDAO>();

        [TestInitialize]
        public void Initialize()
        {
            interviewService.SetUp();
        }

        [TestMethod]
        [Description("Happy Test")]
        public void HappyTest_GetInterviewByID()
        {
            Assert.IsNotNull(interviewService.GetInterviewByID(1));
            Assert.AreEqual(interviewService.GetInterviewByID(1).SAQuestionID, 1);
        }

        [TestMethod]
        [Description("Fail GetInterviewByID")]
        [ExpectedException(typeof(Exception))]
        public void Fail_GetInterviewByID()
        {
            Assert.IsNull(interviewService.GetInterviewByID(6));
        }

        [TestMethod]
        public void Test_GetInterviews()
        {
            Assert.IsNotNull(interviewService.GetInterviews());
            Assert.AreEqual(interviewService.GetInterviews().Count, 3);
        }

        [TestMethod]
        public void Test_CreateInterview()
        {
            InterviewDAO interview3 = new InterviewDAO() { ID = 3, InterviewID = 3, ApplicantID = 3, SAQuestionID = 3, SAResponseID = 3 };

            Assert.IsTrue(interviewService.CreateInterview(interview3));
            Assert.AreEqual(interviewService.GetInterviews().Count, 4);
        }

        [TestMethod]
        public void Test_UpdateInterview()
        {
            InterviewDAO interview3 = new InterviewDAO() { ID = 3, InterviewID = 0, ApplicantID = 3, SAQuestionID = 3, SAResponseID = 3 };

            Assert.IsTrue(interviewService.UpdateInterview(interview3));
            Assert.AreEqual(interviewService.GetInterviewByID(0).ApplicantID, 3);
            Assert.AreEqual(interviewService.GetInterviewByID(0).InterviewID, 0);
            Assert.AreEqual(interviewService.GetInterviews().Count, 3);
        }

        [TestMethod]
        public void Test_DeleteInterview()
        {
            Assert.IsTrue(interviewService.DeleteInterview(2));
            Assert.AreEqual(interviewService.GetInterviews().Count, 2);
        }
    }
}
