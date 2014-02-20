using Kask.DAL2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

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
        Applicant GetApplicantByID(int id);

        /// <summary>
        /// Get all Applicants
        /// </summary>
        /// <returns>List of Applicants</returns>
        [OperationContract]
        IList<Applicant> GetApplicants();

        /// <summary>
        /// Create New Applicant
        /// </summary>
        /// <returns>A boolean whether the new applicant is created</returns>
        [OperationContract]
        bool CreateApplicant(Applicant a);

        /// <summary>
        /// Update an applicant
        /// </summary>
        /// <returns>A boolean whether the applicant is updated</returns>
        [OperationContract]
        bool UpdateApplicant(Applicant newApp);

        /// <summary>
        /// Delete an applicant by ID
        /// </summary>
        /// <param name="ID">Applicant ID</param>
        /// <returns>A boolean whether an applicant is deleted</returns>
        [OperationContract]
        bool DeleteApplicant(int ID);
    }
}
