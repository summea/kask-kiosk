using Kask.DAL2.Models;
using System.Collections.Generic;
using System.ServiceModel;

namespace Kask.Services.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IApplicationService
    {
        /// <summary>
        /// Get Application by ID
        /// </summary>
        /// <param name="id">Application ID</param>
        /// <returns>An application by ID</returns>
        [OperationContract]
        Application GetApplicationById(int id);

        /// <summary>
        /// Get all Applications
        /// </summary>
        /// <returns>List of Applications</returns>
        [OperationContract]
        IList<Application> GetApplications();

        /// <summary>
        /// Create new Application
        /// </summary>
        /// <returns>A boolean whether a new application is created</returns>
        [OperationContract]
        bool CreateApplication(Application app);

        /// <summary>
        /// Update an Application
        /// </summary>
        /// <returns>A boolean whether an application is updated</returns>
        [OperationContract]
        bool UpdateApplication(Application newApp);

        /// <summary>
        /// Delete an application by Application's ID
        /// </summary>
        /// <param name="ID">Application's ID</param>
        /// <returns>A boolean whether an application is deleted or not</returns>
        [OperationContract]
        bool DeleteApplication(int ID);
    }
}
