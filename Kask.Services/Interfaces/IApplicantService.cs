using Kask.DAL2.Models;
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
        Applicant GetApplicantByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<Applicant> GetApplicants();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateApplicant(Applicant a);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateApplicant(Applicant newApp);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteApplicant(int ID);
    }
}
