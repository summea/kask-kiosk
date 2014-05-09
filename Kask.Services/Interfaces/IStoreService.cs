using Kask.Services.Exceptions;
using Kask.Services.DAO;

using System.Collections.Generic;
using System.ServiceModel;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface IStoreService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        StoreDAO GetStoreByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<StoreDAO> GetStores();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateStore(StoreDAO e);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateStore(StoreDAO newEmp);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteStore(int id);
    }
}