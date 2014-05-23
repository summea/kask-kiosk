using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kask.Services.DAO;
using Kask.Services.Interfaces;

namespace KaskUnitTests.MockServices
{
    public class MockInterviewService : IInterviewService
    {
        List<InterviewDAO> Interviews = new List<InterviewDAO>();

        internal void SetUp()
        {
            InterviewDAO interview0 = new InterviewDAO() { ID = 0, InterviewID = 0, ApplicantID = 0, SAQuestionID = 0, SAResponseID = 0 };
            InterviewDAO interview1 = new InterviewDAO() { ID = 1, InterviewID = 1, ApplicantID = 1, SAQuestionID = 1, SAResponseID = 1 };
            InterviewDAO interview2 = new InterviewDAO() { ID = 2, InterviewID = 2, ApplicantID = 2, SAQuestionID = 2, SAResponseID = 2 };

            Interviews.Add(interview0);
            Interviews.Add(interview1);
            Interviews.Add(interview2);
        }
        public InterviewDAO GetInterviewByID(int id)
        {
            foreach (var interview in Interviews)
                if (interview.InterviewID == id)
                    return interview;
            throw new Exception("No Interview Found");
        }

        public IList<InterviewDAO> GetInterviews()
        {
            return Interviews;
        }

        public bool CreateInterview(InterviewDAO e)
        {
            Interviews.Add(e);
            return true;
        }

        public bool UpdateInterview(InterviewDAO newInterview)
        {
            foreach(var interview in Interviews)
                if(interview.InterviewID == newInterview.InterviewID)
                {
                    Interviews.Remove(interview);
                    Interviews.Add(newInterview);
                    return true;
                }
            return false;
        }

        public bool DeleteInterview(int id)
        {
            foreach(var interview in Interviews)
                if(interview.InterviewID == id)
                {
                    Interviews.Remove(interview);
                    return true;
                }
            return false;
        }

        private void Initialize( InterviewDAO interview, int id, int interviewID, int applicantID, int saQuestionID, int saResponseID )
        {
            interview.ID = id;
            interview.InterviewID = interviewID;
            interview.ApplicantID = applicantID;
            interview.SAQuestionID = saQuestionID;
            interview.SAResponseID = saResponseID;
        }
    }
}
