using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kask.Services.DAO;
using Kask.Services.Interfaces;

namespace KaskUnitTests.MockServices
{
    public class MockJobOpeningQuestionService : IJobOpeningInterviewQuestionService
    {
        List<JobOpeningInterviewQuestionDAO> JOIQs = new List<JobOpeningInterviewQuestionDAO>();

        internal void SetUp()
        {
            JobOpeningInterviewQuestionDAO JOIQ0 = new JobOpeningInterviewQuestionDAO() { ID = 0, JobOpeningInterviewQuestionID = 0, JobOpeningID = 0, SAQuestionID = 0 };
            JobOpeningInterviewQuestionDAO JOIQ1 = new JobOpeningInterviewQuestionDAO() { ID = 1, JobOpeningInterviewQuestionID = 1, JobOpeningID = 1, SAQuestionID = 1 };
            JobOpeningInterviewQuestionDAO JOIQ2 = new JobOpeningInterviewQuestionDAO() { ID = 2, JobOpeningInterviewQuestionID = 2, JobOpeningID = 2, SAQuestionID = 2 };

            JOIQs.Add(JOIQ0);
            JOIQs.Add(JOIQ1);
            JOIQs.Add(JOIQ2);
        }
        public JobOpeningInterviewQuestionDAO GetJobOpeningInterviewQuestionByID(int id)
        {
            foreach (var joiq in JOIQs)
                if (joiq.ID == id)
                    return joiq;
            throw new Exception("No JOIQ Found");
        }

        public IList<JobOpeningInterviewQuestionDAO> GetJobOpeningInterviewQuestions()
        {
            return JOIQs;
        }

        public bool CreateJobOpeningInterviewQuestion(JobOpeningInterviewQuestionDAO e)
        {
            JOIQs.Add(e);
            return true;
        }

        public bool UpdateJobOpeningInterviewQuestion(JobOpeningInterviewQuestionDAO newJobOpeningQuestionData)
        {
            foreach(var joiq in JOIQs)
                if(joiq.JobOpeningInterviewQuestionID == newJobOpeningQuestionData.JobOpeningInterviewQuestionID)
                {
                    JOIQs.Remove(joiq);
                    JOIQs.Add(newJobOpeningQuestionData);
                    return true;
                }
            return false;
        }

        public bool DeleteJobOpeningInterviewQuestion(int id)
        {
            foreach(var joiq in JOIQs)
                if(joiq.JobOpeningInterviewQuestionID == id)
                {
                    JOIQs.Remove(joiq);
                    return true;
                }
            return false;
        }

        private void Initialize(JobOpeningInterviewQuestionDAO JOIQ, int id, int joiqID, int jobOpeningID, int saQuestionID )
        {
            JOIQ.ID = id;
            JOIQ.JobOpeningInterviewQuestionID = joiqID;
            JOIQ.JobOpeningID = jobOpeningID;
            JOIQ.SAQuestionID = saQuestionID;
        }
    }
}
