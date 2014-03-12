using Kask.Services.DAO;
using Kask.Services.Interfaces;
using System;
using System.Collections.Generic;


namespace Kask.UnitTests
{
    public class MockApplicantService : IApplicantService
    {
        List<ApplicantDAO> Applicants = new List<ApplicantDAO>();

        public Kask.Services.DAO.ApplicantDAO GetApplicantByID(int id)
        {
            foreach (var applicant in Applicants)
                if (applicant.ApplicantID == id)
                    return applicant ;
            throw new Exception("Applicant not found");
        }

        public IList<Kask.Services.DAO.ApplicantDAO> GetApplicants()
        {
            return Applicants;
        }

        public bool CreateApplicant(Kask.Services.DAO.ApplicantDAO a)
        {
            Applicants.Add(a);
            return true;
        }

        public bool UpdateApplicant(Kask.Services.DAO.ApplicantDAO newApp)
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
    }
}
