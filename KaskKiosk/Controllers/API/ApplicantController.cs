using KaskKiosk.AESApplicationServiceRef;
<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
=======
using System.Collections.Generic;
>>>>>>> origin/views
using System.ServiceModel;
using System.Web;
using System.Web.Http;

namespace KaskKiosk.Controllers.API
{
    public class ApplicantController : ApiController
    {
        // GET: /Applicant/
        [HttpGet]
<<<<<<< HEAD
        public IEnumerable<Applicant> GetApplicants()
=======
        public IEnumerable<ApplicantDAO> GetApplicants()
>>>>>>> origin/views
        {
            ApplicantServiceClient client = new ApplicantServiceClient();

            try
            {
<<<<<<< HEAD
                IEnumerable<Applicant> result = client.GetApplicants();
=======
                IEnumerable<ApplicantDAO> result = client.GetApplicants();
>>>>>>> origin/views
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /Applicant/id
        [HttpGet]
<<<<<<< HEAD
        public Applicant GetApplicant(int id)
=======
        public ApplicantDAO GetApplicant(int id)
>>>>>>> origin/views
        {
            ApplicantServiceClient client = new ApplicantServiceClient();

            try
            {
<<<<<<< HEAD
                Applicant result = client.GetApplicantByID(id);
=======
                ApplicantDAO result = client.GetApplicantByID(id);
>>>>>>> origin/views
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
<<<<<<< HEAD
        public bool PostApplicant(Applicant app)
=======
        public bool PostApplicant(ApplicantDAO app)
>>>>>>> origin/views
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