using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kask.Services.DAO;
using Kask.Services.Interfaces;

namespace KaskUnitTests.MockServices
{
    public class MockQuestionBankService : IQuestionBankService
    {
        List<QuestionBankDAO> QuestionBanks = new List<QuestionBankDAO>();

        internal void SetUp()
        {
            QuestionBankDAO questionBank0 = new QuestionBankDAO() { ID = 0, QuestionBankID = 0, MCQuestionID = 0, MCOptionID = 0 };
            QuestionBankDAO questionBank1 = new QuestionBankDAO() { ID = 1, QuestionBankID = 1, MCQuestionID = 1, MCOptionID = 1 };
            QuestionBankDAO questionBank2 = new QuestionBankDAO() { ID = 2, QuestionBankID = 2, MCQuestionID = 2, MCOptionID = 2 };

            QuestionBanks.Add(questionBank0);
            QuestionBanks.Add(questionBank1);
            QuestionBanks.Add(questionBank2);
        }
        public QuestionBankDAO GetQuestionBankByID(int id)
        {
            foreach (var qb in QuestionBanks)
                if (qb.QuestionBankID == id)
                    return qb;
            throw new Exception("QuestionBank not found");
        }

        public QuestionBankDAO GetQuestionBankByMCQuestionIDAndMCOptionID(int MCQuestionID, int MCOptionID)
        {
            foreach (var qb in QuestionBanks)
                if (qb.MCQuestionID == MCQuestionID && qb.MCOptionID == MCOptionID)
                    return qb;
            throw new Exception("QuestionBank not found");
        }

        public IList<QuestionBankDAO> GetQuestionBanks()
        {
            return QuestionBanks;
        }

        public bool CreateQuestionBank(QuestionBankDAO e)
        {
            QuestionBanks.Add(e);
            return true;
        }

        public bool UpdateQuestionBank(QuestionBankDAO newQuestionBank)
        {
            foreach(var qb in QuestionBanks)
                if(qb.QuestionBankID == newQuestionBank.QuestionBankID)
                {
                    QuestionBanks.Remove(qb);
                    QuestionBanks.Add(newQuestionBank);
                    return true;
                }
            return false;
        }

        public bool DeleteQuestionBank(int id)
        {
            foreach (var qb in QuestionBanks)
                if (qb.QuestionBankID == id)
                {
                    QuestionBanks.Remove(qb);
                    return true;
                }
            return false;
        }

        private void Initialize( QuestionBankDAO QuestionBank, int id, int questionBankID, int mcQuestionID, int mcOptionID )
        {
            QuestionBank.ID = id;
            QuestionBank.QuestionBankID = questionBankID;
            QuestionBank.MCQuestionID = mcQuestionID;
            QuestionBank.MCOptionID = mcOptionID;
        }
    }
}
