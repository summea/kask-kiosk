using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kask.Services.DAO;
using Kask.Services.Interfaces;

namespace Kask.UnitTests
{
    public class MockJobService : IJobService
    {
        List<JobDAO> Jobs = new List<JobDAO>();

        internal void SetUp()
        {
            JobDAO job1 = new JobDAO() { ID = 1, JobID = 1, Title = "Title1" };
            JobDAO job2 = new JobDAO() { ID = 2, JobID = 2, Title = "Title2" };
            JobDAO job3 = new JobDAO() { ID = 3, JobID = 3, Title = "Title3" };

            Jobs.Add(job1);
            Jobs.Add(job2);
            Jobs.Add(job3);
        }

        public JobDAO GetJobByID(int id)
        {
            foreach (var j in Jobs)
                if (j.JobID == id)
                    return j;
            throw new Exception("Job not found");
        }

        public IList<JobDAO> GetJobs()
        {
            return Jobs;
        }

        public bool CreateJob(JobDAO e)
        {
            Jobs.Add(e);
            return true;
        }

        public bool UpdateJob(JobDAO newEmp)
        {
            foreach(var j in Jobs)
                if(j.JobID == newEmp.JobID)
                {
                    Jobs.Remove(j);
                    Jobs.Add(newEmp);
                    return true;
                }
            return false;
        }

        public bool DeleteJob(int id)
        {
            foreach(var j in Jobs)
                if(j.JobID == id)
                {
                    Jobs.Remove(j);
                    return true;
                }
            return false;
        }

        private void initializeJob(JobDAO job, int id, int jobID, string title)
        {
            job.ID = id;
            job.JobID = jobID;
            job.Title = title;
        }
    }
}
