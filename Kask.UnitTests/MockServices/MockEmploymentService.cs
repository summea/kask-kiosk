using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kask.Services.DAO;
using Kask.Services.Interfaces;
using Kask.UnitTests;

namespace Kask.UnitTests
{
    public class MockEmploymentService : IEmploymentService
    {
        List<EmploymentDAO> Employments = new List<EmploymentDAO>();

        internal void SetUp()
        {
            EmploymentDAO employment1 = new EmploymentDAO() { ID = 1, EmploymentID = 1, ApplicantID = 1, EmployerID = 1, Supervisor = "John Smith", Position = "Office", StartingSalary = "13.00", EndingSalary = "19.00", Responsibilities = "Making Coffee" };
            EmploymentDAO employment2 = new EmploymentDAO() { ID = 2, EmploymentID = 2, ApplicantID = 2, EmployerID = 2, Supervisor = "Amanda Raw", Position = "Saleman", StartingSalary = "15.00", EndingSalary = "25.00", Responsibilities = "Sell Car" };
            EmploymentDAO employment3 = new EmploymentDAO() { ID = 3, EmploymentID = 3, ApplicantID = 3, EmployerID = 3, Supervisor = "Anna Smith", Position = "Manager", StartingSalary = "25.00", EndingSalary = "30.00", Responsibilities = "General" };

            Employments.Add(employment1);
            Employments.Add(employment2);
            Employments.Add(employment3);
        }
        public EmploymentDAO GetEmploymentByID(int id)
        {
            foreach (var e in Employments)
                if (e.EmploymentID == id)
                    return e;
            throw new Exception("Employment not found");
        }

        public IList<EmploymentDAO> GetEmployments()
        {
            return Employments;
        }
        
        public bool CreateEmployment(EmploymentDAO e)
        {
            Employments.Add(e);
            return true;
        }

        public bool UpdateEmployment(EmploymentDAO newEmp)
        {
            foreach(var e in Employments)
                if(e.EmploymentID == newEmp.EmploymentID)
                {
                    Employments.Remove(e);
                    Employments.Add(newEmp);
                    return true;
                }
            return false;
        }

        public bool DeleteEmployment(int id)
        {
            foreach (var e in Employments)
                if (e.EmploymentID == id)
                {
                    Employments.Remove(e);
                    return true;
                }
            return false;
        }

        private void initializeEmployment(EmploymentDAO employment, int id, int employmentID, int applicantID, int employerID, string superviser, string position, string startingSalary, string endingSalary, string leavingReason, string responsibility )
        {
            employment.ID = id;
            employment.EmploymentID = employmentID;
            employment.ApplicantID = applicantID;
            employment.EmployerID = employerID;
            employment.Supervisor = superviser;
            employment.Position = position;
            employment.StartingSalary = startingSalary;
            employment.EndingSalary = endingSalary;
            employment.ReasonForLeaving = leavingReason;
            employment.Responsibilities = responsibility;
        }


        public IList<EmploymentDAO> GetEmploymentsByName(string first, string last, string ssn)
        {
            throw new NotImplementedException();
        }
    }
}
