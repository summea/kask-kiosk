using Kask.Services.Exceptions;
using Kask.Services.DAO;

using System.Collections.Generic;
using System.ServiceModel;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface ISAQuestionService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        SAQuestionDAO GetSAQuestionByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<SAQuestionDAO> GetSAQuestions();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateSAQuestion(SAQuestionDAO e);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateSAQuestion(SAQuestionDAO newSAQuestion);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteSAQuestion(int id);
    }
}