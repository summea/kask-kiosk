using Kask.Services.Exceptions;
using Kask.Services.DAO;

using System.Collections.Generic;
using System.ServiceModel;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface IMCQuestionService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        MCQuestionDAO GetMCQuestionByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<MCQuestionDAO> GetMCQuestions();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateMCQuestion(MCQuestionDAO e);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateMCQuestion(MCQuestionDAO newMCQuestion);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteMCQuestion(int id);
    }
}