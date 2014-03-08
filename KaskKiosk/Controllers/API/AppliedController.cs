using KaskKiosk.AESApplicationServiceRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web;
using System.Web.Http;

namespace KaskKiosk.Controllers.API
{
    public class AppliedController : ApiController
    {
        // GET: /Applied/
        [HttpGet]
        public IEnumerable<Applied> GetApplieds()
        {
            AppliedServiceClient client = new AppliedServiceClient();

            try
            {
                IEnumerable<Applied> result = client.GetApplieds();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /Applied/id
        [HttpGet]
        public Applied GetApplied(int id)
        {
            AppliedServiceClient client = new AppliedServiceClient();

            try
            {
                Applied result = client.GetAppliedByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostApplied(Applied app)
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