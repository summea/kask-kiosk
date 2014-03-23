using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kask.Services.Interfaces;
using Kask.Services.DAO;
using KaskUnitTests;

namespace Kask.UnitTests
{
    public class MockSchoolService : ISchoolService
    {
        List<SchoolDAO> Schools = new List<SchoolDAO>();

        internal void SetUp()
        {
            SchoolDAO school1 = new SchoolDAO() { ID = 1, SchoolID = 1, SchoolName = "Name1", SchoolAddress = "Address1" };
            SchoolDAO school2 = new SchoolDAO() { ID = 2, SchoolID = 2, SchoolName = "Name2", SchoolAddress = "Address2" };
            SchoolDAO school3 = new SchoolDAO() { ID = 3, SchoolID = 3, SchoolName = "Name3", SchoolAddress = "Address3" };

            Schools.Add(school1);
            Schools.Add(school2);
            Schools.Add(school3);
        }

        public SchoolDAO GetSchoolByID(int id)
        {
            foreach (var s in Schools)
                if (s.SchoolID == id)
                    return s;
            throw new Exception("School not found");
        }

        public IList<SchoolDAO> GetSchools()
        {
            return Schools;
        }

        public bool CreateSchool(SchoolDAO e)
        {
            Schools.Add(e);
            return true;
        }

        public bool UpdateSchool(SchoolDAO newEmp)
        {
            foreach(var s in Schools)
                if(s.SchoolID == newEmp.SchoolID)
                {
                    Schools.Remove(s);
                    Schools.Add(newEmp);
                    return true;
                }
            return false;
        }

        public bool DeleteSchool(int id)
        {
            foreach(var s in Schools)
                if(s.SchoolID == id)
                {
                    Schools.Remove(s);
                    return true;
                }
            return false;
        }

        private void initializeSchool(SchoolDAO school, int id, int schoolID, string schoolName, string schoolAddress)
        {
            school.ID = id;
            school.SchoolID = schoolID;
            school.SchoolName = schoolName;
            school.SchoolAddress = schoolAddress;
        }
    }
}
