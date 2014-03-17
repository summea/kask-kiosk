using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kask.UnitTests;
using Kask.Services.DAO;
using Kask.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kask.UnitTests
{
    [TestClass]
    public class EmploymentServiceUnitTest
    {
        public MockEmploymentService employmentService = new MockEmploymentService();
        List<EmploymentDAO> Employments = new List<EmploymentDAO>();

        [TestInitialize]
        public void Initialize()
        {
            employmentService.SetUp();
        }

        [TestMethod]
        [Description("Happy GetEmploymentByID")]
        public void Test_GetEmploymentByID()
        {
            Assert.IsNotNull(employmentService.GetEmploymentByID(1));
        }

        [TestMethod]
        [Description("Fail GetEmploymentByID")]
        [ExpectedException(typeof(Exception))]
        public void Test_GetEmploymentByIDFail()
        {
            Assert.IsNull(employmentService.GetEmploymentByID(9));
        }

        [TestMethod]
        public void Test_GetEmployments()
        {
            List<EmploymentDAO> list = employmentService.GetEmployments() as List<EmploymentDAO>;
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void Test_CreateEmployment()
        {
            EmploymentDAO employment4 = new EmploymentDAO() { ID = 4, EmploymentID = 4, ApplicantID = 4, EmployerID = 3, Supervisor = "Anna Smith", Position = "Manager", StartingSalary = "25.00", EndingSalary = "35.00", Responsibilities = "General" };
            employmentService.CreateEmployment(employment4);
            Assert.AreEqual(employmentService.GetEmployments().Count, 4);
        }

        [TestMethod]
        public void Test_UpdateEmployment()
        {
            EmploymentDAO employment3 = new EmploymentDAO() { ID = 3, EmploymentID = 3, ApplicantID = 4, EmployerID = 3, Supervisor = "Anna Smith", Position = "TestPosition", StartingSalary = "25.00", EndingSalary = "35.00", Responsibilities = "General" };
            employmentService.UpdateEmployment(employment3);
            Assert.AreEqual(employmentService.GetEmploymentByID(3).Position, "TestPosition");
        }

        [TestMethod]
        public void Test_DeleteEmployment()
        {
            Assert.IsTrue(employmentService.DeleteEmployment(3));
            Assert.AreEqual(employmentService.GetEmployments().Count, 2);
        }
    }
}
