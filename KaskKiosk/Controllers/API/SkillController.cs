using KaskKiosk.AESApplicationServiceRef;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web;
using System.Web.Http;

namespace KaskKiosk.Controllers.API
{
    public class SkillController : ApiController
    {
        // GET: /Skill/
        [HttpGet]
        public IEnumerable<SkillDAO> GetSkills()
        {
            SkillServiceClient client = new SkillServiceClient();

            try
            {
                IEnumerable<SkillDAO> result = client.GetSkills();
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        // GET: /Skill/id
        [HttpGet]
        public SkillDAO GetSkill(int id)
        {
            SkillServiceClient client = new SkillServiceClient();

            try
            {
                SkillDAO result = client.GetSkillByID(id);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }

        [HttpPost]
        public bool PostSkill(SkillDAO skill)
        {
            SkillServiceClient client = new SkillServiceClient();

            try
            {
                bool result = client.CreateSkill(skill);
                return result;
            }
            catch (FaultException<KaskServiceException> e)
            {
                throw new HttpException(e.Message);
            }
        }
    }
}