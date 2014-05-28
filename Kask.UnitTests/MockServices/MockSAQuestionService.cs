using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kask.Services.DAO;
using Kask.Services.Interfaces;

namespace KaskUnitTests.MockServices
{
    public class MockSAQuestionService : ISAQuestionService
    {
        List<SAQuestionDAO> SAQuestions = new List<SAQuestionDAO>();

        internal void SetUp()
        {
            SAQuestionDAO saQuest0 = new SAQuestionDAO() { ID = 0, SAQuestionID = 0, SAQuestionDescription = "Desc0" };
            SAQuestionDAO saQuest1 = new SAQuestionDAO() { ID = 1, SAQuestionID = 1, SAQuestionDescription = "Desc1" };
            SAQuestionDAO saQuest2 = new SAQuestionDAO() { ID = 2, SAQuestionID = 2, SAQuestionDescription = "Desc2" };

            SAQuestions.Add(saQuest0);
            SAQuestions.Add(saQuest1);
            SAQuestions.Add(saQuest2);
        }
        public SAQuestionDAO GetSAQuestionByID(int id)
        {
            foreach (var value in SAQuestions)
                if (value.SAQuestionID == id)
                    return value;
            throw new Exception("SAQuestion not found");
        }

        public IList<SAQuestionDAO> GetSAQuestions()
        {
            return SAQuestions;
        }

        public bool CreateSAQuestion(SAQuestionDAO e)
        {
            SAQuestions.Add(e);
            return true;
        }

        public bool UpdateSAQuestion(SAQuestionDAO newSAQuestion)
        {
            foreach (var value in SAQuestions)
                if (value.SAQuestionID == newSAQuestion.SAQuestionID)
                {
                    SAQuestions.Remove(value);
                    SAQuestions.Add(newSAQuestion);
                    return true;
                }
            return false;
        }

        public bool DeleteSAQuestion(int id)
        {
            foreach (var value in SAQuestions)
                if (value.SAQuestionID == id)
                {
                    SAQuestions.Remove(value);
                    return true;
                }
            return false;
        }

        private void Initialize( SAQuestionDAO SAQuestion, int id, int saQuestionID, string saQuestionDesc )
        {
            SAQuestion.ID = id;
            SAQuestion.SAQuestionID = saQuestionID;
            SAQuestion.SAQuestionDescription = saQuestionDesc;
        }
    }
}
