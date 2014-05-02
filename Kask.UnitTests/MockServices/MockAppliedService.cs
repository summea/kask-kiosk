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
    public class MockAppliedService : IAppliedService
    {
        List<AppliedDAO> Applieds = new List<AppliedDAO>();

        internal void SetUp()
        {
            AppliedDAO applied1 = new AppliedDAO() { AppliedID = 1, ApplicantID = 1, ApplicationID = 1, JobID = 1 };
            AppliedDAO applied2 = new AppliedDAO() { AppliedID = 2, ApplicantID = 2, ApplicationID = 2, JobID = 2 };
            AppliedDAO applied3 = new AppliedDAO() { AppliedID = 3, ApplicantID = 3, ApplicationID = 3, JobID = 3 };

            Applieds.Add(applied1);
            Applieds.Add(applied2);
            Applieds.Add(applied3);
        }

        public AppliedDAO GetAppliedByID(int id)
        {
            foreach (var a in Applieds)
                if (a.AppliedID == id)
                    return a;
            throw new Exception("Applied not found");
        }

        public IList<AppliedDAO> GetApplieds()
        {
            return Applieds;
        }

        public bool CreateApplied(AppliedDAO a)
        {
            Applieds.Add(a);
            return true;
        }

        public bool UpdateApplied(AppliedDAO newApp)
        {
            foreach (var a in Applieds)
                if (a.AppliedID == newApp.AppliedID)
                {
                    Applieds.Remove(a);
                    Applieds.Add(newApp);
                    return true;
                }
            return false;
        }

        public bool DeleteApplied(int id)
        {
            foreach (var a in Applieds)
                if (a.AppliedID == id)
                {
                    Applieds.Remove(a);
                    return true;
                }
            return false;                    
        }

        private void initializeApplied(AppliedDAO applied, int id, int appliedID, int applicantID, int applicantionID, int applicationID, int jobID, DateTime dateApplied)
        {
            applied.ID = id;
            applied.AppliedID = appliedID;
            applied.ApplicantID = applicantID;
            applied.ApplicationID = applicationID;
            applied.JobID = jobID;
            applied.DateApplied = dateApplied;
        }

        // TODO: Implement this mock test
        public IList<AppliedDAO> GetAppliedsByName(string first, string last, string ssn)
        {
            throw new NotImplementedException();
        }
    }
}
