using Kask.Services.Exceptions;
using Kask.Services.DAO;

using System.Collections.Generic;
using System.ServiceModel;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface IMCOptionService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        MCOptionDAO GetMCOptionByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<MCOptionDAO> GetMCOptions();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateMCOption(MCOptionDAO e);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateMCOption(MCOptionDAO newMCOption);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteMCOption(int id);
    }
}