using KaskKiosk.AESApplicationServiceRef;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web;
using System.Web.Http;

namespace KaskKiosk.Controllers.API
{
    public class JobOpeningController : ApiController
    {
        // GET: /JobOpening/
        [HttpGet]
        public IEnumerable<JobOpeningDAO> GetJobOpenings()
        {
            JobOpeningServiceClient client = new JobOpeningServiceClient();

            try
            {
                IEnumerable<JobOpeningDAO> result = client.GetJobOpenings();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /JobOpening/id
        [HttpGet]
        public JobOpeningDAO GetJobOpening(int id)
        {
            JobOpeningServiceClient client = new JobOpeningServiceClient();

            try
            {
                JobOpeningDAO result = client.GetJobOpeningByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostJobOpening(JobOpeningDAO jobOpening)
        {
            JobOpeningServiceClient client = new JobOpeningServiceClient();

            try
            {
                bool result = false;

                // are we editing or creating this item?
                if (jobOpening.JobOpeningID > 0)
                    result = client.UpdateJobOpening(jobOpening);
                else
                    result = client.CreateJobOpening(jobOpening);

                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }
    }
}