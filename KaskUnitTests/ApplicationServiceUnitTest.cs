using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kask.Services.DAO;
using Kask.Services.Interfaces;

namespace Kask.UnitTests
{
    [TestClass]
    public class ApplicationServiceUnitTest
    {
        public IApplicationService applicationService;
        public ApplicationDAO applicationA;
        public ApplicationDAO applicationB;
        public ApplicationDAO applicationC;
        public ApplicationDAO applicationD;

        [TestInitialize]
        public void Initialize()
        {
            applicationService = new MockApplicationService();
            applicationA = new ApplicationDAO();
            applicationB = new ApplicationDAO();
            applicationC = new ApplicationDAO();
            applicationD = new ApplicationDAO();
        }

        [TestMethod]
        public void Test_GetApplicationByID()
        {
            applicationService.CreateApplication(applicationA);
            applicationService.CreateApplication(applicationB);
           // Assert.IsNull(applicationService.GetApplicationByID(1));
        }
    }
}
