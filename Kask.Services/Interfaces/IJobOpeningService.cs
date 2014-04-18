using Kask.Services.Exceptions;
using Kask.Services.DAO;

using System.Collections.Generic;
using System.ServiceModel;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface IJobOpeningService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        JobOpeningDAO GetJobOpeningByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<JobOpeningDAO> GetJobOpeningsByStoreID(int storeID);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<JobOpeningDAO> GetJobOpenings();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateJobOpening(JobOpeningDAO e);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateJobOpening(JobOpeningDAO newEmp);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteJobOpening(int id);
    }
}