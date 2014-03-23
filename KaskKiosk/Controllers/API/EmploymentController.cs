using KaskKiosk.AESApplicationServiceRef;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web;
using System.Web.Http;

namespace KaskKiosk.Controllers.API
{
    public class EmploymentController : ApiController
    {
        // GET: /Employment/
        [HttpGet]
        public IEnumerable<EmploymentDAO> GetEmployments()
        {
            EmploymentServiceClient client = new EmploymentServiceClient();

            try
            {
                IEnumerable<EmploymentDAO> result = client.GetEmployments();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /Employment/id
        [HttpGet]
        public EmploymentDAO GetEmployment(int id)
        {
            EmploymentServiceClient client = new EmploymentServiceClient();

            try
            {
                EmploymentDAO result = client.GetEmploymentByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /Employment/first/last/ssn
        [HttpGet]
        public IEnumerable<EmploymentDAO> GetItemsForApplicant(string first, string last, string ssn)
        {
            EmploymentServiceClient client = new EmploymentServiceClient();

            try
            {
                IEnumerable<EmploymentDAO> result = client.GetEmploymentsByName(first, last, ssn);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostEmployment(EmploymentDAO emp)
        {
            EmploymentServiceClient client = new EmploymentServiceClient();

            try
            {
                bool result = client.CreateEmployment(emp);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }
    }
}