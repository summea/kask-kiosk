using KaskKiosk.AESApplicationServiceRef;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace KaskKiosk.Controllers.API
{
    public class UserProfileToApplicantController : ApiController
    {
        // GET: /UserProfileToApplicant/
        [HttpGet]
        public IEnumerable<UserProfileToApplicantDAO> GetUserProfileToApplicants()
        {
            UserProfileToApplicantServiceClient client = new UserProfileToApplicantServiceClient();

            try
            {
                IEnumerable<UserProfileToApplicantDAO> result = client.GetUserProfileToApplicants();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /UserProfileToApplicant/id
        [HttpGet]
        public UserProfileToApplicantDAO GetUserProfileToApplicant(int id)
        {
            UserProfileToApplicantServiceClient client = new UserProfileToApplicantServiceClient();

            try
            {
                UserProfileToApplicantDAO result = client.GetUserProfileToApplicantByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostUserProfileToApplicant(UserProfileToApplicantDAO usproToApplicant)
        {
            UserProfileToApplicantServiceClient client = new UserProfileToApplicantServiceClient();

            try
            {
                bool result = client.CreateUserProfileToApplicant(usproToApplicant);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpDelete]
        public bool DeleteUserProfileToApplicant(int id)
        {
            try
            {
                UserProfileToApplicantServiceClient client = new UserProfileToApplicantServiceClient();

                if (client.DeleteUserProfileToApplicant(id))
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