using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kask.Services.DAO;
using Kask.Services.Interfaces;

namespace KaskUnitTests.MockServices
{
    public class MockJobOpeningService : IJobOpeningService
    {
        List<JobOpeningDAO> JobOpenings = new List<JobOpeningDAO>();

        internal void SetUp()
        {
            JobOpeningDAO jobopening1 = new JobOpeningDAO() { JobOpeningID = 1, JobID = 1, Approved = 1 };
            JobOpeningDAO jobopening2 = new JobOpeningDAO() { JobOpeningID = 2, JobID = 2, Approved = 1 };
            JobOpeningDAO jobopening3 = new JobOpeningDAO() { JobOpeningID = 3, JobID = 3, Approved = 1 };

            JobOpenings.Add(jobopening1);
            JobOpenings.Add(jobopening2);
            JobOpenings.Add(jobopening3);
        }

        public JobOpeningDAO GetJobOpeningByID(int id)
        {
            foreach (var jo in JobOpenings)
                if (jo.JobOpeningID == id)
                    return jo;
            throw new Exception("JobOpening not found..");
        }

        public IList<JobOpeningDAO> GetJobOpeningsByStoreID(int storeID)
        {
            throw new NotImplementedException();
        }

        public IList<JobOpeningDAO> GetJobOpenings()
        {
            return JobOpenings;
        }

        public bool CreateJobOpening(JobOpeningDAO e)
        {
            JobOpenings.Add(e);
            return true;
        }

        public bool UpdateJobOpening(JobOpeningDAO newEmp)
        {
            foreach(var jo in JobOpenings)
                if(jo.JobOpeningID == newEmp.JobOpeningID)
                {
                    JobOpenings.Remove(jo);
                    JobOpenings.Add(newEmp);
                    return true;
                }
            return false;
        }

        public bool DeleteJobOpening(int id)
        {
            foreach(var jo in JobOpenings)
                if(jo.JobOpeningID == id)
                {
                    JobOpenings.Remove(jo);
                    return true;
                }
            return false;
        }

        private void initializeJobOpening(JobOpeningDAO jobOpening, int id, int jobOpeningID, int jobID, int approved)
        {
            jobOpening.ID = id;
            jobOpening.JobOpeningID = jobOpeningID;
            jobOpening.JobID = jobID;
            jobOpening.Approved = approved;
        }
    }
}
