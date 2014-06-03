using KaskKiosk.AESApplicationServiceRef;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace KaskKiosk.Controllers.API
{
    public class UserProfileController : ApiController
    {
        // GET: /UserProfile/
        [HttpGet]
        public IEnumerable<UserProfileDAO> GetUserProfiles()
        {
            UserProfileServiceClient client = new UserProfileServiceClient();

            try
            {
                IEnumerable<UserProfileDAO> result = client.GetUserProfiles();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /UserProfile/id
        [HttpGet]
        public UserProfileDAO GetUserProfile(int id)
        {
            UserProfileServiceClient client = new UserProfileServiceClient();

            try
            {
                UserProfileDAO result = client.GetUserProfileByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostUserProfile(UserProfileDAO uspro)
        {
            UserProfileServiceClient client = new UserProfileServiceClient();

            try
            {
                bool result = client.CreateUserProfile(uspro);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpDelete]
        public bool DeleteUserProfile(int id)
        {
            try
            {
                UserProfileServiceClient client = new UserProfileServiceClient();

                if (client.DeleteUserProfile(id))
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