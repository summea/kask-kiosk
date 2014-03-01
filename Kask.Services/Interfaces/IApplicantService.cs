using Kask.DAL2.Models;
using Kask.Services.Exceptions;
using System.Collections.Generic;
using System.ServiceModel;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface IApplicantService
    {
        /// <summary>
        /// Get Applicant by ID
        /// </summary>
        /// <param name="id">Applicant ID</param>
        /// <returns>Applicant</returns>
        [OperationContract]
<<<<<<< HEAD
        [FaultContract(typeof(KaskServiceExceptions))]
=======
        [FaultContract(typeof(KaskServiceException))]
>>>>>>> origin/views
        Applicant GetApplicantByID(int id);

        /// <summary>
        /// Get all Applicants
        /// </summary>
        /// <returns>List of Applicants</returns>
        [OperationContract]
<<<<<<< HEAD
        [FaultContract(typeof(KaskServiceExceptions))]
=======
        [FaultContract(typeof(KaskServiceException))]
>>>>>>> origin/views
        IList<Applicant> GetApplicants();

        /// <summary>
        /// Create New Applicant
        /// </summary>
        /// <returns>A boolean whether the new applicant is created</returns>
        [OperationContract]
<<<<<<< HEAD
        [FaultContract(typeof(KaskServiceExceptions))]
=======
        [FaultContract(typeof(KaskServiceException))]
>>>>>>> origin/views
        bool CreateApplicant(Applicant a);

        /// <summary>
        /// Update an applicant
        /// </summary>
        /// <returns>A boolean whether the applicant is updated</returns>
        [OperationContract]
<<<<<<< HEAD
        [FaultContract(typeof(KaskServiceExceptions))]
=======
        [FaultContract(typeof(KaskServiceException))]
>>>>>>> origin/views
        bool UpdateApplicant(Applicant newApp);

        /// <summary>
        /// Delete an applicant by ID
        /// </summary>
        /// <param name="ID">Applicant ID</param>
        /// <returns>A boolean whether an applicant is deleted</returns>
        [OperationContract]
<<<<<<< HEAD
        [FaultContract(typeof(KaskServiceExceptions))]
=======
        [FaultContract(typeof(KaskServiceException))]
>>>>>>> origin/views
        bool DeleteApplicant(int ID);
    }
}
