using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kask.Services.DAO;
using Kask.Services.Interfaces;

namespace KaskUnitTests.MockServices
{
    public class MockAssessmentService : IAssessmentService
    {
        List<AssessmentDAO> Assessments = new List<AssessmentDAO>();

        internal void SetUp()
        {
            AssessmentDAO assessment1 = new AssessmentDAO() { ID = 1, AssessmentID = 1, ApplicantID = 1, QuestionBankID = 1 };
            AssessmentDAO assessment2 = new AssessmentDAO() { ID = 2, AssessmentID = 2, ApplicantID = 2, QuestionBankID = 2 };
            AssessmentDAO assessment3 = new AssessmentDAO() { ID = 3, AssessmentID = 3, ApplicantID = 3, QuestionBankID = 3 };

            Assessments.Add(assessment1);
            Assessments.Add(assessment2);
            Assessments.Add(assessment3);
        }
        public AssessmentDAO GetAssessmentByID(int id)
        {
            foreach (var assess in Assessments)
                if (assess.AssessmentID == id)
                    return assess;
            throw new Exception("No Assessment found");
        }

        public IList<AssessmentDAO> GetAssessments()
        {
            return Assessments;
        }

        public bool CreateAssessment(AssessmentDAO e)
        {
            Assessments.Add(e);
            return true;
        }

        public bool UpdateAssessment(AssessmentDAO newAssessment)
        {
            foreach(var assess in Assessments)
                if(assess.AssessmentID == newAssessment.AssessmentID)
                {
                    Assessments.Remove(assess);
                    Assessments.Add(newAssessment);
                    return true;
                }
            return false;
        }

        public bool DeleteAssessment(int id)
        {
            foreach(var assess in Assessments)
                if(assess.AssessmentID == id)
                {
                    Assessments.Remove(assess);
                    return true;
                }
            return false;
        }

        private void Initialize( AssessmentDAO assessment, int id, int assessmentID, int applicantID, int questionBankID )
        {
            assessment.ID = id;
            assessment.AssessmentID = assessmentID;
            assessment.ApplicantID = applicantID;
            assessment.QuestionBankID = questionBankID;
        }
    }
}
