using KaskKiosk.AESApplicationServiceRef;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web;
using System.Web.Http;

namespace KaskKiosk.Controllers.API
{
    public class StoreController : ApiController
    {
        // GET: /Store/
        [HttpGet]
        public IEnumerable<StoreDAO> GetStores()
        {
            StoreServiceClient client = new StoreServiceClient();

            try
            {
                IEnumerable<StoreDAO> result = client.GetStores();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /Store/id
        [HttpGet]
        public StoreDAO GetStore(int id)
        {
            StoreServiceClient client = new StoreServiceClient();

            try
            {
                StoreDAO result = client.GetStoreByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostStore(StoreDAO store)
        {
            StoreServiceClient client = new StoreServiceClient();

            try
            {
                bool result = client.CreateStore(store);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }
    }
}