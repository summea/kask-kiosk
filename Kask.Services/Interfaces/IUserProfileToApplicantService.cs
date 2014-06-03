using Kask.Services.Exceptions;
using Kask.Services.DAO;

using System.Collections.Generic;
using System.ServiceModel;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface IUserProfileToApplicantService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        UserProfileToApplicantDAO GetUserProfileToApplicantByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<UserProfileToApplicantDAO> GetUserProfileToApplicants();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateUserProfileToApplicant(UserProfileToApplicantDAO e);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateUserProfileToApplicant(UserProfileToApplicantDAO newUserProfileToApplicantData);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteUserProfileToApplicant(int id);
    }
}