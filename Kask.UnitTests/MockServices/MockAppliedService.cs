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
    }
}
