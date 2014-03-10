using Kask.Services.DAO;

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
        ApplicationDAO GetApplicationByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<ApplicationDAO> GetApplicationsByName(string first, string last, string ssn);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<ApplicationDAO> GetApplications();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateApplication(ApplicationDAO app);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateApplication(ApplicationDAO newApp);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteApplication(int ID);
    }
}
