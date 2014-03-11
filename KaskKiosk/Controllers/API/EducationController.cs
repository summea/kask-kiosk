using KaskKiosk.AESApplicationServiceRef;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web;
using System.Web.Http;

namespace KaskKiosk.Controllers.API
{
    public class EducationController : ApiController
    {
        // GET: /Education/
        [HttpGet]
        public IEnumerable<EducationDAO> GetEducations()
        {
            EducationServiceClient client = new EducationServiceClient();

            try
            {
                IEnumerable<EducationDAO> result = client.GetEducations();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /Education/id
        [HttpGet]
        public EducationDAO GetEducation(int id)
        {
            EducationServiceClient client = new EducationServiceClient();

            try
            {
                EducationDAO result = client.GetEducationByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostEducation(EducationDAO edu)
        {
            EducationServiceClient client = new EducationServiceClient();

            try
            {
                bool result = client.CreateEducation(edu);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }
    }
}