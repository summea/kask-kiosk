using KaskKiosk.AESApplicationServiceRef;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web;
using System.Web.Http;

namespace KaskKiosk.Controllers.API
{
    public class AppliedController : ApiController
    {
        // GET: /Applied/
        [HttpGet]
        public IEnumerable<AppliedDAO> GetApplieds()
        {
            AppliedServiceClient client = new AppliedServiceClient();

            try
            {
                IEnumerable<AppliedDAO> result = client.GetApplieds();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /Applied/id
        [HttpGet]
        public AppliedDAO GetApplied(int id)
        {
            AppliedServiceClient client = new AppliedServiceClient();

            try
            {
                AppliedDAO result = client.GetAppliedByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostApplied(AppliedDAO app)
        {
            AppliedServiceClient client = new AppliedServiceClient();

            try
            {
                bool result = client.CreateApplied(app);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }
    }
}