using KaskKiosk.AESApplicationServiceRef;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web;
using System.Web.Http;

namespace KaskKiosk.Controllers.API
{
    public class MCOptionController : ApiController
    {
        // GET: /MCOption/
        [HttpGet]
        public IEnumerable<MCOptionDAO> GetMCOptions()
        {
            MCOptionServiceClient client = new MCOptionServiceClient();

            try
            {
                IEnumerable<MCOptionDAO> result = client.GetMCOptions();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /MCOption/id
        [HttpGet]
        public MCOptionDAO GetMCOption(int id)
        {
            MCOptionServiceClient client = new MCOptionServiceClient();

            try
            {
                MCOptionDAO result = client.GetMCOptionByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostMCOption(MCOptionDAO mCOption)
        {
            MCOptionServiceClient client = new MCOptionServiceClient();

            try
            {
                bool result = client.CreateMCOption(mCOption);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }
    }
}