using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kask.Services.DAO;
using Kask.Services.Interfaces;

namespace KaskUnitTests.MockServices
{
    public class MockMCQuestionService : IMCQuestionService
    {
        List<MCQuestionDAO> MCQuestions = new List<MCQuestionDAO>();

        internal void SetUp()
        {
            MCQuestionDAO mcQuestion0 = new MCQuestionDAO() { ID = 0, MCQuestionID = 0, MCQuestionDescription = "mcQ0" };
            MCQuestionDAO mcQuestion1 = new MCQuestionDAO() { ID = 1, MCQuestionID = 1, MCQuestionDescription = "mcQ1" };
            MCQuestionDAO mcQuestion2 = new MCQuestionDAO() { ID = 2, MCQuestionID = 2, MCQuestionDescription = "mcQ2" };

            MCQuestions.Add(mcQuestion0);
            MCQuestions.Add(mcQuestion1);
            MCQuestions.Add(mcQuestion2);
        }
        public MCQuestionDAO GetMCQuestionByID(int id)
        {
            foreach (var mcQ in MCQuestions)
                if (mcQ.MCQuestionID == id)
                    return mcQ;
            throw new Exception("MCQuestion not found");
        }

        public IList<MCQuestionDAO> GetMCQuestions()
        {
            return MCQuestions;
        }

        public bool CreateMCQuestion(MCQuestionDAO e)
        {
            MCQuestions.Add(e);
            return true;
        }

        public bool UpdateMCQuestion(MCQuestionDAO newMCQuestion)
        {
            foreach(var mcQ in MCQuestions)
                if(mcQ.MCQuestionID == newMCQuestion.MCQuestionID)
                {
                    MCQuestions.Remove(mcQ);
                    MCQuestions.Add(newMCQuestion);
                    return true;
                }
            return false;
        }

        public bool DeleteMCQuestion(int id)
        {
            foreach(var mcQ in MCQuestions)
                if(mcQ.MCQuestionID == id)
                {
                    MCQuestions.Remove(mcQ);
                    return true;
                }
            return false;
        }

        private void Initialize( MCQuestionDAO mcQuestion, int id, int mcQuestionID, string mcQuestionDescription )
        {
            mcQuestion.ID = id;
            mcQuestion.MCQuestionID = mcQuestionID;
            mcQuestion.MCQuestionDescription = mcQuestionDescription;
        }
    }
}
