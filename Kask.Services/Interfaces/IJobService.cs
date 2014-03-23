using Kask.Services.Exceptions;
using Kask.Services.DAO;

using System.Collections.Generic;
using System.ServiceModel;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface IJobService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        JobDAO GetJobByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<JobDAO> GetJobs();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateJob(JobDAO e);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateJob(JobDAO newEmp);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteJob(int id);
    }
}