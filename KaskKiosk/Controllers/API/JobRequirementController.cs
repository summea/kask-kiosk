using KaskKiosk.AESApplicationServiceRef;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web;
using System.Web.Http;

namespace KaskKiosk.Controllers.API
{
    public class JobRequirementController : ApiController
    {
        // GET: /JobRequirement/
        [HttpGet]
        public IEnumerable<JobRequirementDAO> GetJobRequirements()
        {
            JobRequirementServiceClient client = new JobRequirementServiceClient();

            try
            {
                IEnumerable<JobRequirementDAO> result = client.GetJobRequirements();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /JobRequirement/id
        [HttpGet]
        public JobRequirementDAO GetJobRequirement(int id)
        {
            JobRequirementServiceClient client = new JobRequirementServiceClient();

            try
            {
                JobRequirementDAO result = client.GetJobRequirementByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostJobRequirement(JobRequirementDAO jobRequirementRequirement)
        {
            JobRequirementServiceClient client = new JobRequirementServiceClient();

            try
            {
                bool result = client.CreateJobRequirement(jobRequirementRequirement);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }
    }
}