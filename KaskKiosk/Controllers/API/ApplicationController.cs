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
    public class ApplicationController : ApiController
    {
        // GET: /Application/
        [HttpGet]
        public IEnumerable<Application> GetApplications()
        {
            ApplicationServiceClient client = new ApplicationServiceClient();

            try
            {
                IEnumerable<Application> result = client.GetApplications();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /Application/id
        [HttpGet]
        public Application GetApplication(int id)
        {
            ApplicationServiceClient client = new ApplicationServiceClient();

            try
            {
                Application result = client.GetApplicationByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostApplication(Application app)
        {
            ApplicationServiceClient client = new ApplicationServiceClient();

            try
            {
                bool result = client.CreateApplication(app);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }
    }
}
