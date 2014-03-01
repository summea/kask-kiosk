using Kask.DAL2.Models;
using Kask.Services.Exceptions;
using System.Collections.Generic;
using System.ServiceModel;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface IAppliedService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        Applied GetAppliedByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<Applied> GetApplieds();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateApplied(Applied a);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteApplied(int id);
    }
}
