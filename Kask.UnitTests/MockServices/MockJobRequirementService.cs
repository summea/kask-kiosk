using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kask.Services.DAO;
using Kask.Services.Interfaces;

namespace KaskUnitTests.MockServices
{
    public class MockJobRequirementService : IJobRequirementService
    {
        List<JobRequirementDAO> JobRequirements = new List<JobRequirementDAO>();

        internal void SetUp()
        {
            JobRequirementDAO JobReq0 = new JobRequirementDAO() { ID = 0, JobRequirementID = 0, JobOpeningID = 0, SkillID = 0, Notes = "Note 0" };
            JobRequirementDAO JobReq1 = new JobRequirementDAO() { ID = 1, JobRequirementID = 1, JobOpeningID = 1, SkillID = 1, Notes = "Note 1" };
            JobRequirementDAO JobReq2 = new JobRequirementDAO() { ID = 2, JobRequirementID = 2, JobOpeningID = 2, SkillID = 2, Notes = "Note 2" };

            JobRequirements.Add(JobReq0);
            JobRequirements.Add(JobReq1);
            JobRequirements.Add(JobReq2);
        }
        public JobRequirementDAO GetJobRequirementByID(int id)
        {
            foreach (var jq in JobRequirements)
                if (jq.JobRequirementID == id)
                    return jq;
            throw new Exception("JobRequirement not found");
        }

        public IList<JobRequirementDAO> GetJobRequirements()
        {
            return JobRequirements;
        }

        public bool CreateJobRequirement(JobRequirementDAO e)
        {
            JobRequirements.Add(e);
            return true;
        }

        public bool UpdateJobRequirement(JobRequirementDAO newJobRequirement)
        {
            foreach(var jq in JobRequirements)
                if(jq.JobRequirementID == newJobRequirement.JobRequirementID)
                {
                    JobRequirements.Remove(jq);
                    JobRequirements.Add(newJobRequirement);
                    return true;
                }
            return false;
        }

        public bool DeleteJobRequirement(int id)
        {
            foreach(var jq in JobRequirements)
                if(jq.JobRequirementID == id)
                {
                    JobRequirements.Remove(jq);
                    return true;
                }
            return false;
        }

        private void Initialize( JobRequirementDAO jobRequirement, int id, int jobRequirementID, int jobOpeningID, int skillID, string note )
        {
            jobRequirement.ID = id;
            jobRequirement.JobRequirementID = jobRequirementID;
            jobRequirement.JobOpeningID = jobOpeningID;
            jobRequirement.SkillID = skillID;
            jobRequirement.Notes = note;
        }
    }
}
