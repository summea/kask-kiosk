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
    public class SAQuestionServiceUnitTest
    {
        public MockSAQuestionService SAQService = new MockSAQuestionService();
        List<SAQuestionDAO> saQuestions = new List<SAQuestionDAO>();

        [TestInitialize]
        public void Initialize()
        {
            SAQService.SetUp();
        }

        [TestMethod]
        [Description("Happy Test")]
        public void HappyTest_GetSAQuestionByID()
        {
            Assert.IsNotNull(SAQService.GetSAQuestionByID(0));
            Assert.AreEqual(SAQService.GetSAQuestionByID(0).SAQuestionDescription, "Desc0");
        }

        [TestMethod]
        [Description("Fail Test GetSAQuestionByID")]
        [ExpectedException(typeof(Exception))]
        public void FailTest_GetSAQuestionByID()
        {
            Assert.IsNull(SAQService.GetSAQuestionByID(9));
        }

        [TestMethod]
        public void Test_GetSAQuestions()
        {
            Assert.IsNotNull(SAQService.GetSAQuestions());
            Assert.AreEqual(SAQService.GetSAQuestions().Count, 3);
        }

        [TestMethod]
        public void Test_CreateSAQuestion()
        {
            SAQuestionDAO saQuest3 = new SAQuestionDAO() { ID = 3, SAQuestionID = 3, SAQuestionDescription = "Desc3" };

            Assert.IsTrue(SAQService.CreateSAQuestion(saQuest3));
            Assert.AreEqual(SAQService.GetSAQuestions().Count, 4);
        }

        [TestMethod]
        public void Test_UpdateSAQuestion()
        {
            SAQuestionDAO saQuest2 = new SAQuestionDAO() { ID = 2, SAQuestionID = 2, SAQuestionDescription = "TestDesc2" };

            Assert.IsTrue(SAQService.UpdateSAQuestion(saQuest2));
            Assert.AreEqual(SAQService.GetSAQuestionByID(2).SAQuestionDescription, "TestDesc2");
        }

        [TestMethod]
        public void Test_DeleteSAQuestion()
        {
            Assert.IsTrue(SAQService.DeleteSAQuestion(2));
            Assert.AreEqual(SAQService.GetSAQuestions().Count, 2);
        }
    }
}
