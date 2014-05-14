using KaskKiosk.AESApplicationServiceRef;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web;
using System.Web.Http;

namespace KaskKiosk.Controllers.API
{
    public class InterviewController : ApiController
    {
        // GET: /Interview/
        [HttpGet]
        public IEnumerable<InterviewDAO> GetInterviews()
        {
            InterviewServiceClient client = new InterviewServiceClient();

            try
            {
                IEnumerable<InterviewDAO> result = client.GetInterviews();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /Interview/id
        [HttpGet]
        public InterviewDAO GetInterview(int id)
        {
            InterviewServiceClient client = new InterviewServiceClient();

            try
            {
                InterviewDAO result = client.GetInterviewByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostInterview(InterviewDAO interview)
        {
            InterviewServiceClient client = new InterviewServiceClient();

            try
            {
                bool result = client.CreateInterview(interview);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }
    }
}