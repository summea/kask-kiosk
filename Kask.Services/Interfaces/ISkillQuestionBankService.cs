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
    public interface ISkillQuestionBankService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        SkillQuestionBankDAO GetSkillQuestionBankByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<SkillQuestionBankDAO> GetSkillQuestionBanks();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateSkillQuestionBank(SkillQuestionBankDAO e);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateSkillQuestionBank(SkillQuestionBankDAO newSkillQuestionBank);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteSkillQuestionBank(int id);
    }
}