using KaskKiosk.AESApplicationServiceRef;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web;
using System.Web.Http;

namespace KaskKiosk.Controllers.API
{
    public class SAResponseController : ApiController
    {
        // GET: /SAResponse/
        [HttpGet]
        public IEnumerable<SAResponseDAO> GetSAResponses()
        {
            SAResponseServiceClient client = new SAResponseServiceClient();

            try
            {
                IEnumerable<SAResponseDAO> result = client.GetSAResponses();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /SAResponse/id
        [HttpGet]
        public SAResponseDAO GetSAResponse(int id)
        {
            SAResponseServiceClient client = new SAResponseServiceClient();

            try
            {
                SAResponseDAO result = client.GetSAResponseByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostSAResponse(SAResponseDAO sAResponse)
        {
            SAResponseServiceClient client = new SAResponseServiceClient();

            try
            {
                bool result = client.CreateSAResponse(sAResponse);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }
    }
}