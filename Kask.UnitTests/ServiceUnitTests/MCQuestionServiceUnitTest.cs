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
    public class MCQuestionServiceUnitTest
    {
        public MockMCQuestionService MCQService = new MockMCQuestionService();
        List<MCQuestionDAO> mcQuestions = new List<MCQuestionDAO>();

        [TestInitialize]
        public void Initialize()
        {
            MCQService.SetUp();
        }

        [TestMethod]
        [Description("Happy Test")]
        public void HappyTest_GetMCQuestionByID()
        {
            Assert.IsNotNull(MCQService.GetMCQuestionByID(0));
            Assert.AreEqual(MCQService.GetMCQuestionByID(0).MCQuestionDescription, "mcQ0");
        }

        [TestMethod]
        [Description("Fail Test GetMCQuestionByID")]
        [ExpectedException(typeof(Exception))]
        public void FailTest_GetMCQuestionByID()
        {
            Assert.IsNull(MCQService.GetMCQuestionByID(9));
        }

        [TestMethod]
        public void Test_GetMCQuestions()
        {
            Assert.IsNotNull(MCQService.GetMCQuestions());
            Assert.AreEqual(MCQService.GetMCQuestions().Count, 3);
        }

        [TestMethod]
        public void Test_CreateMCQuestion()
        {
            MCQuestionDAO mcQuestion3 = new MCQuestionDAO() { ID = 3, MCQuestionID = 3, MCQuestionDescription = "mcQ3" };

            Assert.IsTrue(MCQService.CreateMCQuestion(mcQuestion3));
            Assert.AreEqual(MCQService.GetMCQuestions().Count, 4);
        }

        [TestMethod]
        public void Test_UpdateMCQuestion()
        {
            MCQuestionDAO mcQuestion2 = new MCQuestionDAO() { ID = 2, MCQuestionID = 2, MCQuestionDescription = "NEWmcQ2" };

            Assert.IsTrue(MCQService.UpdateMCQuestion(mcQuestion2));
            Assert.AreEqual(MCQService.GetMCQuestionByID(2).MCQuestionDescription, "NEWmcQ2");
        }

        [TestMethod]
        public void Test_DeleteMCQuestion()
        {
            Assert.IsTrue(MCQService.DeleteMCQuestion(2));
            Assert.AreEqual(MCQService.GetMCQuestions().Count, 2);
        }
    }
}
