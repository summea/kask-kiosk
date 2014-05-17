using Kask.Services.Exceptions;
using Kask.Services.DAO;

using System.Collections.Generic;
using System.ServiceModel;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface IQuestionBankService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        QuestionBankDAO GetQuestionBankByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        QuestionBankDAO GetQuestionBankByMCQuestionIDAndMCOptionID(int MCQuestionID, int MCOptionID);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<QuestionBankDAO> GetQuestionBanks();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateQuestionBank(QuestionBankDAO e);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateQuestionBank(QuestionBankDAO newQuestionBank);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteQuestionBank(int id);
    }
}