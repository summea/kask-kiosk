using KaskKiosk.AESApplicationServiceRef;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web;
using System.Web.Http;

namespace KaskKiosk.Controllers.API
{
    public class JobController : ApiController
    {
        // GET: /Job/
        [HttpGet]
        public IEnumerable<JobDAO> GetJobs()
        {
            JobServiceClient client = new JobServiceClient();

            try
            {
                IEnumerable<JobDAO> result = client.GetJobs();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /Job/id
        [HttpGet]
        public JobDAO GetJob(int id)
        {
            JobServiceClient client = new JobServiceClient();

            try
            {
                JobDAO result = client.GetJobByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostJob(JobDAO job)
        {
            JobServiceClient client = new JobServiceClient();

            try
            {
                bool result = client.CreateJob(job);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }
    }
}