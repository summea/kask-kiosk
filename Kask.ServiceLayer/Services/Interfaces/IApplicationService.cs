using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Kask.DAL.Models;

namespace Kask.ServiceLayer.Services.Interfaces
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
        Applications GetApplication(int id);

        /// <summary>
        /// Gets all Applications
        /// </summary>
        /// <returns>All applications</returns>
        [OperationContract]
        Applications GetApplications();

        /// <summary>
        /// Create new Application
        /// </summary>
        /// <returns>A boolean whether a new application is created</returns>
        [OperationContract]
        Applications CreateApplication(Applications app);

        /// <summary>
        /// Update an Application by ID
        /// </summary>
        /// <param name="ID">Application's ID</param>
        /// <returns>A boolean whether an application is updated</returns>
        [OperationContract]
        bool UpdateApplication(int ID);

        /* ================ HTTP Delete /Application/id/ ================ */
        /// <summary>
        /// Delete an application by Application's ID
        /// </summary>
        /// <param name="ID">Application's ID</param>
        /// <returns>A boolean whether an application is deleted or not</returns>
        [OperationContract]
        bool DeleteApplication(int ID);
    }
}
