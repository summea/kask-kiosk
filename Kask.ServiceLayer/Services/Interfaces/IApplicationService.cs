using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kask.DAL.Models;
using System.ServiceModel;

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
        Application GetApplicationById(int id);

        /// <summary>
        /// Get Application by Applicant's ID
        /// </summary>
        /// <param name="applicantID">Applicant's ID</param>
        /// <returns>Returns an Application</returns>
        [OperationContract]
        Application GetApplicationByApplicantId(int applicantID);

        /// <summary>
        /// Gets all Applications
        /// </summary>
        /// <returns>All applications</returns>
        [OperationContract]
        Application GetApplications();

        /// <summary>
        /// Create new Application
        /// </summary>
        /// <returns>A boolean whether a new application is created</returns>
        [OperationContract]
        Application CreateApplication(Application app);

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
