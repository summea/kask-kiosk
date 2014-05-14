using KaskKiosk.AESApplicationServiceRef;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web;
using System.Web.Http;

namespace KaskKiosk.Controllers.API
{
    public class SAQuestionController : ApiController
    {
        // GET: /SAQuestion/
        [HttpGet]
        public IEnumerable<SAQuestionDAO> GetSAQuestions()
        {
            SAQuestionServiceClient client = new SAQuestionServiceClient();

            try
            {
                IEnumerable<SAQuestionDAO> result = client.GetSAQuestions();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /SAQuestion/id
        [HttpGet]
        public SAQuestionDAO GetSAQuestion(int id)
        {
            SAQuestionServiceClient client = new SAQuestionServiceClient();

            try
            {
                SAQuestionDAO result = client.GetSAQuestionByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostSAQuestion(SAQuestionDAO sAQuestion)
        {
            SAQuestionServiceClient client = new SAQuestionServiceClient();

            try
            {
                bool result = client.CreateSAQuestion(sAQuestion);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }
    }
}