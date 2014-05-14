using Kask.Services.Exceptions;
using Kask.Services.DAO;

using System.Collections.Generic;
using System.ServiceModel;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface IInterviewService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        InterviewDAO GetInterviewByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<InterviewDAO> GetInterviews();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateInterview(InterviewDAO e);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateInterview(InterviewDAO newInterview);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteInterview(int id);
    }
}