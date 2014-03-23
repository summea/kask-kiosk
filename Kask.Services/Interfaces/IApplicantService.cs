using Kask.Services.DAO;

using Kask.Services.Exceptions;
using System.Collections.Generic;
using System.ServiceModel;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface IApplicantService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        ApplicantDAO GetApplicantByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<ApplicantDAO> GetApplicants();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateApplicant(ApplicantDAO a);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateApplicant(ApplicantDAO newApp);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteApplicant(int ID);
    }
}
