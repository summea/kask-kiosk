using KaskKiosk.AESApplicationServiceRef;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web;
using System.Web.Http;

namespace KaskKiosk.Controllers.API
{
    public class SkillQuestionBankController : ApiController
    {
        // GET: /SkillQuestionBank/
        [HttpGet]
        public IEnumerable<SkillQuestionBankDAO> GetSkillQuestionBanks()
        {
            SkillQuestionBankServiceClient client = new SkillQuestionBankServiceClient();

            try
            {
                IEnumerable<SkillQuestionBankDAO> result = client.GetSkillQuestionBanks();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /SkillQuestionBank/id
        [HttpGet]
        public SkillQuestionBankDAO GetSkillQuestionBank(int id)
        {
            SkillQuestionBankServiceClient client = new SkillQuestionBankServiceClient();

            try
            {
                SkillQuestionBankDAO result = client.GetSkillQuestionBankByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostSkillQuestionBank(SkillQuestionBankDAO skillQuestionBank)
        {
            SkillQuestionBankServiceClient client = new SkillQuestionBankServiceClient();

            try
            {
                bool result = client.CreateSkillQuestionBank(skillQuestionBank);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }
    }
}