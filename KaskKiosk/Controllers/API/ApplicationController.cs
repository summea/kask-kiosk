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
<<<<<<< HEAD
        public IEnumerable<Application> GetApplications()
=======
        public IEnumerable<ApplicationDAO> GetApplications()
>>>>>>> origin/views
        {
            ApplicationServiceClient client = new ApplicationServiceClient();

            try
            {
<<<<<<< HEAD
                IEnumerable<Application> result = client.GetApplications();
=======
                IEnumerable<ApplicationDAO> result = client.GetApplications();
>>>>>>> origin/views
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /Application/id
        [HttpGet]
<<<<<<< HEAD
        public Application GetApplication(int id)
=======
        public ApplicationDAO GetApplication(int id)
>>>>>>> origin/views
        {
            ApplicationServiceClient client = new ApplicationServiceClient();

            try
            {
<<<<<<< HEAD
                Application result = client.GetApplicationById(id);
=======
                ApplicationDAO result = client.GetApplicationByID(id);
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
        public bool PostApplication(Application app)
=======
        public bool PostApplication(ApplicationDAO app)
>>>>>>> origin/views
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
<<<<<<< HEAD
}
=======
}
>>>>>>> origin/views
