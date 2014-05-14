using KaskKiosk.AESApplicationServiceRef;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web;
using System.Web.Http;

namespace KaskKiosk.Controllers.API
{
    public class MCQuestionController : ApiController
    {
        // GET: /MCQuestion/
        [HttpGet]
        public IEnumerable<MCQuestionDAO> GetMCQuestions()
        {
            MCQuestionServiceClient client = new MCQuestionServiceClient();

            try
            {
                IEnumerable<MCQuestionDAO> result = client.GetMCQuestions();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /MCQuestion/id
        [HttpGet]
        public MCQuestionDAO GetMCQuestion(int id)
        {
            MCQuestionServiceClient client = new MCQuestionServiceClient();

            try
            {
                MCQuestionDAO result = client.GetMCQuestionByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostMCQuestion(MCQuestionDAO mCQuestion)
        {
            MCQuestionServiceClient client = new MCQuestionServiceClient();

            try
            {
                bool result = client.CreateMCQuestion(mCQuestion);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }
    }
}