using Kask.DAL2.Models;
using System.Collections.Generic;
using System.ServiceModel;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface IAppliedService
    {
        /// <summary>
        /// Get Applied by ID
        /// </summary>
        /// <param name="id">Applied ID</param>
        /// <returns>An Applied by ID</returns>
        [OperationContract]
        Applied GetAppliedByID(int id);

        /// <summary>
        /// Get all applieds
        /// </summary>
        /// <returns>A list of all applieds</returns>
        [OperationContract]
        IList<Applied> GetApplieds();

        /// <summary>
        /// Create new applied
        /// </summary>
        /// <returns>A boolean whether a new applied is created</returns>
        [OperationContract]
        bool CreateApplied(Applied a);

        /// <summary>
        /// Update an applied
        /// </summary>
        /// <returns>A boolean whether an applied is updated</returns>
        [OperationContract]
        bool UpdateApplied(Applied a);

        /// <summary>
        /// Delete an applied
        /// </summary>
        /// <returns>A boolean whether an applied is deleted</returns>
        [OperationContract]
        bool DeleteApplied(int id);
    }
}
