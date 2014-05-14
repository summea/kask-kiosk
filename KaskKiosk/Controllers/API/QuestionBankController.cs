using KaskKiosk.AESApplicationServiceRef;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web;
using System.Web.Http;

namespace KaskKiosk.Controllers.API
{
    public class QuestionBankController : ApiController
    {
        // GET: /QuestionBank/
        [HttpGet]
        public IEnumerable<QuestionBankDAO> GetQuestionBanks()
        {
            QuestionBankServiceClient client = new QuestionBankServiceClient();

            try
            {
                IEnumerable<QuestionBankDAO> result = client.GetQuestionBanks();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /QuestionBank/id
        [HttpGet]
        public QuestionBankDAO GetQuestionBank(int id)
        {
            QuestionBankServiceClient client = new QuestionBankServiceClient();

            try
            {
                QuestionBankDAO result = client.GetQuestionBankByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostQuestionBank(QuestionBankDAO questionBank)
        {
            QuestionBankServiceClient client = new QuestionBankServiceClient();

            try
            {
                bool result = client.CreateQuestionBank(questionBank);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }
    }
}