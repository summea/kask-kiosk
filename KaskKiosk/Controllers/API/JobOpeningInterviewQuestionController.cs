using KaskKiosk.AESApplicationServiceRef;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web;
using System.Web.Http;

namespace KaskKiosk.Controllers.API
{
    public class JobOpeningInterviewQuestionController : ApiController
    {
        // GET: /JobOpeningInterviewQuestion/
        [HttpGet]
        public IEnumerable<JobOpeningInterviewQuestionDAO> GetJobOpeningInterviewQuestions()
        {
            JobOpeningInterviewQuestionServiceClient client = new JobOpeningInterviewQuestionServiceClient();

            try
            {
                IEnumerable<JobOpeningInterviewQuestionDAO> result = client.GetJobOpeningInterviewQuestions();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /JobOpeningInterviewQuestion/id
        [HttpGet]
        public JobOpeningInterviewQuestionDAO GetJobOpeningInterviewQuestion(int id)
        {
            JobOpeningInterviewQuestionServiceClient client = new JobOpeningInterviewQuestionServiceClient();

            try
            {
                JobOpeningInterviewQuestionDAO result = client.GetJobOpeningInterviewQuestionByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostJobOpeningInterviewQuestion(JobOpeningInterviewQuestionDAO jobOpeningInterviewQuestion)
        {
            JobOpeningInterviewQuestionServiceClient client = new JobOpeningInterviewQuestionServiceClient();

            try
            {
                bool result = client.CreateJobOpeningInterviewQuestion(jobOpeningInterviewQuestion);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }
    }
}