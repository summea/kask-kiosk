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
    public class MockEmployerService : IEmployerService
    {
        List<EmployerDAO> Employers = new List<EmployerDAO>();

        internal void SetUp()
        {
            EmployerDAO employer1 = new EmployerDAO() { ID = 1, EmployerID = 1, Name = "Intel", EmployerAddress = "Address1", PhoneNumber = "503" };
            EmployerDAO employer2 = new EmployerDAO() { ID = 2, EmployerID = 2, Name = "Google", EmployerAddress = "Address2", PhoneNumber = "504" };
            EmployerDAO employer3 = new EmployerDAO() { ID = 3, EmployerID = 3, Name = "Microsoft", EmployerAddress = "Address3", PhoneNumber = "505" };


            Employers.Add(employer1);
            Employers.Add(employer2);
            Employers.Add(employer3);
        }

        public EmployerDAO GetEmployerByID(int id)
        {
            foreach (var e in Employers)
                if (e.EmployerID == id)
                    return e;
            throw new Exception("Employer not found");
        }

        public IList<EmployerDAO> GetEmployers()
        {
            return Employers;
        }

        public bool CreateEmployer(EmployerDAO e)
        {
            Employers.Add(e);
            return true;
        }

        public bool UpdateEmployer(EmployerDAO newEmp)
        {
            foreach (var e in Employers)
                if (e.EmployerID == newEmp.EmployerID)
                {
                    Employers.Remove(e);
                    Employers.Add(newEmp);
                    return true;
                }
            return false;
        }

        public bool DeleteEmployer(int id)
        {
            foreach (var e in Employers)
                if (e.EmployerID == id)
                {
                    Employers.Remove(e);
                    return true;
                }
            return false;
        }

        private void initializeEmployer(EmployerDAO employer, int id, int employerID, string name, string employerAddress, string phoneNumber)
        {
            employer.ID = id;
            employer.EmployerID = employerID;
            employer.Name = name;
            employer.EmployerAddress = employerAddress;
            employer.PhoneNumber = phoneNumber;           
        }  
    }
}
