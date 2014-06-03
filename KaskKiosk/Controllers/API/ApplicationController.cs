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
        public IEnumerable<ApplicationDAO> GetApplications()
        {
            ApplicationServiceClient client = new ApplicationServiceClient();

            try
            {
                IEnumerable<ApplicationDAO> result = client.GetApplications();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /Application/by_applicant/id
        [HttpGet]
        public IEnumerable<ApplicationDAO> GetApplicationsByApplicant(int id)
        {
            ApplicationServiceClient client = new ApplicationServiceClient();

            try
            {
                IEnumerable<ApplicationDAO> result = client.GetApplicationsByApplicant(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /Application/id
        [HttpGet]
        public ApplicationDAO GetApplication(int id)
        {
            ApplicationServiceClient client = new ApplicationServiceClient();

            try
            {
                ApplicationDAO result = client.GetApplicationByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostApplication(ApplicationDAO app)
        {
            ApplicationServiceClient client = new ApplicationServiceClient();

            try
            {
                bool result = false;

                // are we editing or creating this item?
                if (app.ApplicationID > 0)
                    result = client.UpdateApplication(app);
                else
                    result = client.CreateApplication(app);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpDelete]
        public bool DeleteApplication(int id)
        {
            try
            {
                bool result = false;
                ApplicationServiceClient client = new ApplicationServiceClient();

                if (client.DeleteApplication(id))
                    result = true;

                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }
    }
}
