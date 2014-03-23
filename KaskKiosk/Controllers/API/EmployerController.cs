using KaskKiosk.AESApplicationServiceRef;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web;
using System.Web.Http;

namespace KaskKiosk.Controllers.API
{
    public class EmployerController : ApiController
    {
        // GET: /Employer/
        [HttpGet]
        public IEnumerable<EmployerDAO> GetEmployers()
        {
            EmployerServiceClient client = new EmployerServiceClient();

            try
            {
                IEnumerable<EmployerDAO> result = client.GetEmployers();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /Employer/id
        [HttpGet]
        public EmployerDAO GetEmployer(int id)
        {
            EmployerServiceClient client = new EmployerServiceClient();

            try
            {
                EmployerDAO result = client.GetEmployerByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostEmployer(EmployerDAO emp)
        {
            EmployerServiceClient client = new EmployerServiceClient();

            try
            {
                bool result = client.CreateEmployer(emp);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }
    }
}