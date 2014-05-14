using Kask.Services.Exceptions;
using Kask.Services.DAO;

using System.Collections.Generic;
using System.ServiceModel;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface IAssessmentService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        AssessmentDAO GetAssessmentByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<AssessmentDAO> GetAssessments();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateAssessment(AssessmentDAO e);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateAssessment(AssessmentDAO newAssessment);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteAssessment(int id);
    }
}