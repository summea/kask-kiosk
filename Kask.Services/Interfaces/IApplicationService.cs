using Kask.DAL2.Models;
using Kask.Services.Exceptions;
using System.Collections.Generic;
using System.ServiceModel;

namespace Kask.Services.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IApplicationService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        Application GetApplicationById(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        Application GetApplicationByName(string first, string last, string ssn);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<Application> GetApplications();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateApplication(Application app);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateApplication(Application newApp);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteApplication(int ID);
    }
}
