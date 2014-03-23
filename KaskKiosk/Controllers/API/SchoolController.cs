using KaskKiosk.AESApplicationServiceRef;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web;
using System.Web.Http;

namespace KaskKiosk.Controllers.API
{
    public class SchoolController : ApiController
    {
        // GET: /School/
        [HttpGet]
        public IEnumerable<SchoolDAO> GetSchools()
        {
            SchoolServiceClient client = new SchoolServiceClient();

            try
            {
                IEnumerable<SchoolDAO> result = client.GetSchools();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /School/id
        [HttpGet]
        public SchoolDAO GetSchool(int id)
        {
            SchoolServiceClient client = new SchoolServiceClient();

            try
            {
                SchoolDAO result = client.GetSchoolByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostSchool(SchoolDAO school)
        {
            SchoolServiceClient client = new SchoolServiceClient();

            try
            {
                bool result = client.CreateSchool(school);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }
    }
}