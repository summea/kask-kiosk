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
<<<<<<< HEAD
        [FaultContract(typeof(KaskServiceExceptions))]
=======
        [FaultContract(typeof(KaskServiceException))]
>>>>>>> origin/views
=======
        [FaultContract(typeof(KaskServiceException))]
>>>>>>> 8570f2ae69cfe040570c17ccf514b7259181a9f1
        Applicant GetApplicantByID(int id);

        /// <summary>
        /// Get all Applicants
        /// </summary>
        /// <returns>List of Applicants</returns>
        [OperationContract]
<<<<<<< HEAD
<<<<<<< HEAD
        [FaultContract(typeof(KaskServiceExceptions))]
=======
        [FaultContract(typeof(KaskServiceException))]
>>>>>>> origin/views
=======
        [FaultContract(typeof(KaskServiceException))]
>>>>>>> 8570f2ae69cfe040570c17ccf514b7259181a9f1
        IList<Applicant> GetApplicants();

        /// <summary>
        /// Create New Applicant
        /// </summary>
        /// <returns>A boolean whether the new applicant is created</returns>
        [OperationContract]
<<<<<<< HEAD
<<<<<<< HEAD
        [FaultContract(typeof(KaskServiceExceptions))]
=======
        [FaultContract(typeof(KaskServiceException))]
>>>>>>> origin/views
=======
        [FaultContract(typeof(KaskServiceException))]
>>>>>>> 8570f2ae69cfe040570c17ccf514b7259181a9f1
        bool CreateApplicant(Applicant a);

        /// <summary>
        /// Update an applicant
        /// </summary>
        /// <returns>A boolean whether the applicant is updated</returns>
        [OperationContract]
<<<<<<< HEAD
<<<<<<< HEAD
        [FaultContract(typeof(KaskServiceExceptions))]
=======
        [FaultContract(typeof(KaskServiceException))]
>>>>>>> origin/views
=======
        [FaultContract(typeof(KaskServiceException))]
>>>>>>> 8570f2ae69cfe040570c17ccf514b7259181a9f1
        bool UpdateApplicant(Applicant newApp);

        /// <summary>
        /// Delete an applicant by ID
        /// </summary>
        /// <param name="ID">Applicant ID</param>
        /// <returns>A boolean whether an applicant is deleted</returns>
        [OperationContract]
<<<<<<< HEAD
<<<<<<< HEAD
        [FaultContract(typeof(KaskServiceExceptions))]
=======
        [FaultContract(typeof(KaskServiceException))]
>>>>>>> origin/views
=======
        [FaultContract(typeof(KaskServiceException))]
>>>>>>> 8570f2ae69cfe040570c17ccf514b7259181a9f1
        bool DeleteApplicant(int ID);
    }
}
