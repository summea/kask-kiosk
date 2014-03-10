using KaskKiosk.AESApplicationServiceRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web;
using System.Web.Http;
/*
namespace KaskKiosk.Controllers.API
{
    public class ApplicantController : ApiController
    {
        // GET: /Applicant/
        [HttpGet]
        public IEnumerable<Applicant> GetApplicants()
        {
            ApplicantServiceClient client = new ApplicantServiceClient();

            try
            {
                IEnumerable<Applicant> result = client.GetApplicants();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /Applicant/id
        [HttpGet]
        public Applicant GetApplicant(int id)
        {
            ApplicantServiceClient client = new ApplicantServiceClient();

            try
            {
                Applicant result = client.GetApplicantByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostApplicant(Applicant app)
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
    }
}
*/