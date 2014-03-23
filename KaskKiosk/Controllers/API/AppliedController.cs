using KaskKiosk.AESApplicationServiceRef;
<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
=======
using System.Collections.Generic;
>>>>>>> origin/views
using System.ServiceModel;
using System.Web;
using System.Web.Http;

namespace KaskKiosk.Controllers.API
{
    public class AppliedController : ApiController
    {
        // GET: /Applied/
        [HttpGet]
<<<<<<< HEAD
        public IEnumerable<Applied> GetApplieds()
=======
        public IEnumerable<AppliedDAO> GetApplieds()
>>>>>>> origin/views
        {
            AppliedServiceClient client = new AppliedServiceClient();

            try
            {
<<<<<<< HEAD
                IEnumerable<Applied> result = client.GetApplieds();
=======
                IEnumerable<AppliedDAO> result = client.GetApplieds();
>>>>>>> origin/views
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /Applied/id
        [HttpGet]
<<<<<<< HEAD
        public Applied GetApplied(int id)
=======
        public AppliedDAO GetApplied(int id)
>>>>>>> origin/views
        {
            AppliedServiceClient client = new AppliedServiceClient();

            try
            {
<<<<<<< HEAD
                Applied result = client.GetAppliedByID(id);
=======
                AppliedDAO result = client.GetAppliedByID(id);
>>>>>>> origin/views
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
<<<<<<< HEAD
        public bool PostApplied(Applied app)
=======
        public bool PostApplied(AppliedDAO app)
>>>>>>> origin/views
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