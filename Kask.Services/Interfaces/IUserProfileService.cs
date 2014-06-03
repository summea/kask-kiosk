using Kask.Services.Exceptions;
using Kask.Services.DAO;

using System.Collections.Generic;
using System.ServiceModel;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface IUserProfileService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        UserProfileDAO GetUserProfileByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<UserProfileDAO> GetUserProfiles();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateUserProfile(UserProfileDAO e);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateUserProfile(UserProfileDAO newUserProfileData);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteUserProfile(int id);
    }
}