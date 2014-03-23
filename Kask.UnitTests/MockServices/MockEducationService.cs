using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kask.Services.Interfaces;
using Kask.Services.DAO;
using Kask.Services.Exceptions;

namespace Kask.UnitTests
{
    public class MockEducationService : IEducationService
    {
        List<EducationDAO> Educations = new List<EducationDAO>();

        internal void SetUp()
        {
            EducationDAO education1 = new EducationDAO() { ID = 1, ApplicantID = 1, EducationID = 1, SchoolID = 1, DegreeAndMajor = "Math" };
            EducationDAO education2 = new EducationDAO() { ID = 2, ApplicantID = 2, EducationID = 2, SchoolID = 1, DegreeAndMajor = "Math" };
            EducationDAO education3 = new EducationDAO() { ID = 3, ApplicantID = 3, EducationID = 3, SchoolID = 1, DegreeAndMajor = "Math" };
            EducationDAO education4 = new EducationDAO() { ID = 4, ApplicantID = 4, EducationID = 4, SchoolID = 1, DegreeAndMajor = "Math" };

            Educations.Add(education1);
            Educations.Add(education2);
            Educations.Add(education3);
            Educations.Add(education4);
        }

        public EducationDAO GetEducationByID(int id)
        {
            foreach (var e in Educations)
                if (e.EducationID == id)
                    return e;
            throw new Exception("Education not found");
        }

        public IList<EducationDAO> GetEducationsByName(string first, string last, string ssn)
        {
            throw new NotImplementedException();
        }

        public IList<EducationDAO> GetEducations()
        {
            return Educations;
        }
        
        public bool CreateEducation(EducationDAO e)
        {
            Educations.Add(e);
            return true;
        }

        public bool UpdateEducation(EducationDAO newEmp)
        {
            foreach (var e in Educations)
                if (e.EducationID == newEmp.EducationID)
                {
                    Educations.Remove(e);
                    Educations.Add(newEmp);
                    return true;
                }
            return false;
        }

        public bool DeleteEducation(int id)
        {
            foreach (var e in Educations)
                if (e.EducationID == id)
                {
                    Educations.Remove(e);
                    return true;
                }
            return false;

        }

        private void initializeEducation(EducationDAO education, int id, int educationID, int applicantID, int schoolID, string degreeAndmajor)
        {
            education.ID = id;
            education.EducationID = educationID;
            education.ApplicantID = applicantID;
            education.SchoolID = schoolID;
            education.DegreeAndMajor = degreeAndmajor;
            education.YearsAttendedFrom = null;
            education.Graduated = null;
            education.YearsAttendedTo = null;
        }        
    }
}
