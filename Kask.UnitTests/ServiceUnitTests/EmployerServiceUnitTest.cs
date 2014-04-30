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
    public class EmployerServiceUnitTest
    {
        public MockEmployerService employerService = new MockEmployerService();
        List<EmployerDAO> Employers = new List<EmployerDAO>();

        [TestInitialize]
        public void Initialize()
        {
            employerService.SetUp();
        }

        [TestMethod]
        [Description("Happy GetEmployerByID")]
        public void Test_GetEmployerByID()
        {
            Assert.IsNotNull(employerService.GetEmployerByID(1));
        }

        [TestMethod]
        [Description("Fail GetEmployerByID")]
        [ExpectedException(typeof(Exception))]
        public void Test_GetEmployerByIDFail()
        {
            Assert.IsNull(employerService.GetEmployerByID(8));
        }

        [TestMethod]
        public void Test_GetEducations()
        {
            List<EmployerDAO> list = employerService.GetEmployers() as List<EmployerDAO>;
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void Test_CreateEmployer()
        {
            EmployerDAO employer4 = new EmployerDAO() { ID = 4, EmployerID = 4, Name = "TestEmployer", EmployerAddress = "TestAddress", PhoneNumber = "TestPhone" };
            employerService.CreateEmployer(employer4);
            Assert.AreEqual(employerService.GetEmployerByID(4).EmployerID, 4);
            Assert.AreEqual(employerService.GetEmployerByID(4).ID, 4);
            Assert.AreEqual(employerService.GetEmployerByID(4).Name, "TestEmployer");
            Assert.AreEqual(employerService.GetEmployers().Count, 4);
        }

        [TestMethod]
        public void Test_UpdateEmployer()
        {
            EmployerDAO employer4 = new EmployerDAO() { ID = 3, EmployerID = 3, Name = "UpdatedEmployer", EmployerAddress = "UpdatedAddress", PhoneNumber = "UpdatedPhone" };
            employerService.UpdateEmployer(employer4);
            Assert.AreEqual(employerService.GetEmployerByID(3).Name, "UpdatedEmployer");
            Assert.AreEqual(employerService.GetEmployerByID(3).PhoneNumber, "UpdatedPhone");
            Assert.AreEqual(employerService.GetEmployerByID(3).EmployerAddress, "UpdatedAddress");
        }

        [TestMethod]
        public void Test_DeleteEmployer()
        {
            Assert.IsTrue(employerService.DeleteEmployer(3));
            Assert.AreEqual(employerService.GetEmployers().Count, 2);
        }
    }    
}
