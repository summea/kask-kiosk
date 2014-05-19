using Kask.Services.Exceptions;
using Kask.Services.DAO;

using System.Collections.Generic;
using System.ServiceModel;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface IJobOpeningInterviewQuestionService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        JobOpeningInterviewQuestionDAO GetJobOpeningInterviewQuestionByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<JobOpeningInterviewQuestionDAO> GetJobOpeningInterviewQuestions();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateJobOpeningInterviewQuestion(JobOpeningInterviewQuestionDAO e);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateJobOpeningInterviewQuestion(JobOpeningInterviewQuestionDAO newJobOpeningQuestionData);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteJobOpeningInterviewQuestion(int id);
    }
}