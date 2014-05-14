using Kask.Services.Exceptions;
using Kask.Services.DAO;

using System.Collections.Generic;
using System.ServiceModel;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface ISAResponseService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        SAResponseDAO GetSAResponseByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<SAResponseDAO> GetSAResponses();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateSAResponse(SAResponseDAO e);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateSAResponse(SAResponseDAO newSAResponse);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteSAResponse(int id);
    }
}