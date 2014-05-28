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
    public class QuestionBankUnitTest
    {
        public MockQuestionBankService QBService = new MockQuestionBankService();
        List<QuestionBankDAO> QuestionBanks = new List<QuestionBankDAO>();

        [TestInitialize]
        public void Initialize()
        {
            QBService.SetUp();
        }

        [TestMethod]
        [Description("Happy Test")]
        public void HappyTest_GetQuestionBankByID()
        {
            Assert.IsNotNull(QBService.GetQuestionBankByID(0));
            Assert.AreEqual(QBService.GetQuestionBankByID(0).MCQuestionID, 0);
        }

        [TestMethod]
        [Description("Fail Test GetQuestionBankByID")]
        [ExpectedException(typeof(Exception))]
        public void FailTest_GetQuestionBankByID()
        {
            Assert.IsNull(QBService.GetQuestionBankByID(9));
        }

        [TestMethod]
        public void Test_GetQuestionBanks()
        {
            Assert.IsNotNull(QBService.GetQuestionBanks());
            Assert.AreEqual(QBService.GetQuestionBanks().Count, 3);
        }

        [TestMethod]
        public void Test_CreateQuestionBank()
        {
            QuestionBankDAO questionBank3 = new QuestionBankDAO() { ID = 3, QuestionBankID = 3, MCQuestionID = 3, MCOptionID = 3 };

            Assert.IsTrue(QBService.CreateQuestionBank(questionBank3));
            Assert.AreEqual(QBService.GetQuestionBanks().Count, 4);
        }

        [TestMethod]
        public void Test_UpdateQuestionBank()
        {
            QuestionBankDAO questionBank2 = new QuestionBankDAO() { ID = 2, QuestionBankID = 2, MCQuestionID = 2, MCOptionID = 4 };

            Assert.IsTrue(QBService.UpdateQuestionBank(questionBank2));
            Assert.AreEqual(QBService.GetQuestionBankByID(2).MCOptionID, 4);
        }

        [TestMethod]
        public void Test_DeleteQuestionBank()
        {
            Assert.IsTrue(QBService.DeleteQuestionBank(2));
            Assert.AreEqual(QBService.GetQuestionBanks().Count, 2);
        }
    }
}
