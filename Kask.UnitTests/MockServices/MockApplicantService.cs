using Kask.Services.DAO;
using Kask.Services.Interfaces;
using System;
using System.Collections.Generic;


namespace Kask.UnitTests
{
    public class MockApplicantService : IApplicantService
    {
        List<ApplicantDAO> Applicants = new List<ApplicantDAO>();

        internal void SetUp()
        {
            ApplicantDAO applicant1 = new ApplicantDAO() { ApplicantID = 1, FirstName = "First1", LastName = "Last1", Phone = "1111", SSN = "0001" };
            ApplicantDAO applicant2 = new ApplicantDAO() { ApplicantID = 2, FirstName = "First2", LastName = "Last2", Phone = "2222", SSN = "0002" };
            ApplicantDAO applicant3 = new ApplicantDAO() { ApplicantID = 3, FirstName = "First3", LastName = "Last3", Phone = "3333", SSN = "0003" };
            ApplicantDAO applicant4 = new ApplicantDAO() { ApplicantID = 4, FirstName = "First4", LastName = "Last4", Phone = "4444", SSN = "0004" };

            Applicants.Add(applicant1);
            Applicants.Add(applicant2);
            Applicants.Add(applicant3);
            Applicants.Add(applicant4);
        }

        public ApplicantDAO GetApplicantByID(int id)
        {
            foreach (var applicant in Applicants)
                if (applicant.ApplicantID == id)
                    return applicant ;
            throw new Exception("Applicant not found");
        }

        public IList<ApplicantDAO> GetApplicants()
        {
            return Applicants;
        }

        public bool CreateApplicant(ApplicantDAO a)
        {
            Applicants.Add(a);
            return true;
        }

        public bool UpdateApplicant(ApplicantDAO newApp)
        {
            foreach(var applicant in Applicants)
                if (applicant.ApplicantID == newApp.ApplicantID)
                {
                    Applicants.Remove(applicant);
                    Applicants.Add(newApp);
                    return true;
                }
            return false;
        }

        public bool DeleteApplicant(int ID)
        {
            foreach(var applicant in Applicants)
                if (applicant.ApplicantID == ID)
                {
                    Applicants.Remove(applicant);
                    return true;
                }
            return false;
        }

        private void initializeApplicant( ApplicantDAO applicant, int id, int applicantID, string firstname, string middlename, string lastname, string ssn, string gender, string applicantAddress, string phone, string nameAlias )
        {
            applicant.ID = id;
            applicant.ApplicantID = applicantID;
            applicant.FirstName = firstname;
            applicant.LastName = lastname;
            applicant.MiddleName = middlename;
            applicant.SSN = ssn;
            applicant.Gender = gender;
            applicant.ApplicantAddress = applicantAddress;
            applicant.Phone = phone;
            applicant.NameAlias = nameAlias;
        }
    }
}
