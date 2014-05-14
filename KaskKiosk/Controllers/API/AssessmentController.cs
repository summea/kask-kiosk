using KaskKiosk.AESApplicationServiceRef;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web;
using System.Web.Http;

namespace KaskKiosk.Controllers.API
{
    public class AssessmentController : ApiController
    {
        // GET: /Assessment/
        [HttpGet]
        public IEnumerable<AssessmentDAO> GetAssessments()
        {
            AssessmentServiceClient client = new AssessmentServiceClient();

            try
            {
                IEnumerable<AssessmentDAO> result = client.GetAssessments();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /Assessment/id
        [HttpGet]
        public AssessmentDAO GetAssessment(int id)
        {
            AssessmentServiceClient client = new AssessmentServiceClient();

            try
            {
                AssessmentDAO result = client.GetAssessmentByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostAssessment(AssessmentDAO assessment)
        {
            AssessmentServiceClient client = new AssessmentServiceClient();

            try
            {
                bool result = client.CreateAssessment(assessment);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }
    }
}