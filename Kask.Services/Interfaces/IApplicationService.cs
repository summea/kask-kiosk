using Kask.DAL.Models;
using Kask.Services.Exceptions;
using System.Collections.Generic;
using System.ServiceModel;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface IApplicationService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        Application GetApplicationByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<Application> GetApplicationsByName(string first, string last, string ssn);

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
