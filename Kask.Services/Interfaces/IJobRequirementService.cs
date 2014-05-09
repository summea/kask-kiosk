using Kask.Services.DAO;
using Kask.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface IJobRequirementService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        JobRequirementDAO GetJobRequirementByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<JobRequirementDAO> GetJobRequirements();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateJobRequirement(JobRequirementDAO e);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateJobRequirement(JobRequirementDAO newJobRequirement);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteJobRequirement(int id);
    }
}