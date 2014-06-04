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
    public class MCOptionUnitTest
    {
        public MockMCOptionService mcoService = new MockMCOptionService();
        List<MCOptionDAO> mcOptions = new List<MCOptionDAO>();

        [TestInitialize]
        public void Initialize()
        {
            mcoService.SetUp();
        }

        [TestMethod]
        [Description("HappyTest GetMCOptionByID")]
        public void HappyTest_GetMCOptionByID()
        {
            Assert.IsNotNull(mcoService.GetMCOptionByID(1));
            Assert.AreEqual(mcoService.GetMCOptionByID(1).MCOptionID, 1);
        }

        [TestMethod]
        [Description("Fail Test GetMCOptionByID")]
        [ExpectedException(typeof(Exception))]
        public void FailTest_GetMCOptionByID()
        {
            Assert.IsNull(mcoService.GetMCOptionByID(9));
        }

        [TestMethod]
        public void Test_GetMCOptionss()
        {
            Assert.IsNotNull(mcoService.GetMCOptions());
            Assert.AreEqual(mcoService.GetMCOptions().Count, 3);
        }

        [TestMethod]
        public void Test_CreateMCOption()
        {
            MCOptionDAO mcOpt3 = new MCOptionDAO() { ID = 3, MCOptionID = 3, MCOptionDescription = "Description 3" };

            Assert.IsTrue(mcoService.CreateMCOption(mcOpt3));
            Assert.AreEqual(mcoService.GetMCOptions().Count, 4);
        }

        [TestMethod]
        public void Test_UpdateMCOption()
        {
            MCOptionDAO mcOpt3 = new MCOptionDAO() { ID = 3, MCOptionID = 0, MCOptionDescription = "Description 3" };

            Assert.IsTrue(mcoService.UpdateMCOption(mcOpt3));
            Assert.AreEqual(mcoService.GetMCOptionByID(0).ID, 3);
        }

        [TestMethod]
        public void Test_DeleteMCOption()
        {
            Assert.IsTrue(mcoService.DeleteMCOption(2));
            Assert.AreEqual(mcoService.GetMCOptions().Count, 2);
        }
    }    
}
