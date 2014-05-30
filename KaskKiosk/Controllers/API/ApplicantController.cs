using KaskKiosk.AESApplicationServiceRef;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace KaskKiosk.Controllers.API
{
    public class ApplicantController : ApiController
    {
        // GET: /Applicant/
        [HttpGet]
        public IEnumerable<ApplicantDAO> GetApplicants()
        {
            ApplicantServiceClient client = new ApplicantServiceClient();

            try
            {
                IEnumerable<ApplicantDAO> result = client.GetApplicants();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /Applicant/id
        [HttpGet]
        public ApplicantDAO GetApplicant(int id)
        {
            ApplicantServiceClient client = new ApplicantServiceClient();

            try
            {
                ApplicantDAO result = client.GetApplicantByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostApplicant(ApplicantDAO app)
        {
            ApplicantServiceClient client = new ApplicantServiceClient();

            try
            {
                bool result = client.CreateApplicant(app);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpDelete]
        public bool DeleteApplicant(int id)
        {
            try
            {
                ApplicantServiceClient client = new ApplicantServiceClient();

                if (client.DeleteApplicant(id))
                    return true;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
            return false;
        }
    }
}