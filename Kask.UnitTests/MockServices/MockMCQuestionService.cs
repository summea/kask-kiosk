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
        public MCQuestionDAO GetMCQuestionByID(int id)
        {
            throw new NotImplementedException();
        }

        public IList<MCQuestionDAO> GetMCQuestions()
        {
            throw new NotImplementedException();
        }

        public bool CreateMCQuestion(MCQuestionDAO e)
        {
            throw new NotImplementedException();
        }

        public bool UpdateMCQuestion(MCQuestionDAO newMCQuestion)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMCQuestion(int id)
        {
            throw new NotImplementedException();
        }

        private void Initialize( MCQuestionDAO mcQuestion, int id, int mcQuestionID, string mcQuestionDescription )
        {
            mcQuestion.ID = id;
            mcQuestion.MCQuestionID = mcQuestionID;
            mcQuestion.MCQuestionDescription = mcQuestionDescription;
        }
    }
}
