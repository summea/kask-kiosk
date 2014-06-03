using Newtonsoft.Json;
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;

namespace KaskKiosk.Controllers
{
    public class ServiceURIs
    {
        public static readonly string ServiceApplicantUri                   =   "http://localhost:51309/api/Applicant";
        public static readonly string ServiceApplicationUri                 =   "http://localhost:51309/api/Application";
        public static readonly string ServiceAppliedUri                     =   "http://localhost:51309/api/Applied";
        public static readonly string ServiceAssessmentUri                  =   "http://localhost:51309/api/Assessment";
        public static readonly string ServiceEducationUri                   =   "http://localhost:51309/api/Education";
        public static readonly string ServiceEmployerUri                    =   "http://localhost:51309/api/Employer";
        public static readonly string ServiceEmploymentUri                  =   "http://localhost:51309/api/Employment";
        public static readonly string ServiceInterviewUri                   =   "http://localhost:51309/api/Interview";
        public static readonly string ServiceJobOpeningUri                  =   "http://localhost:51309/api/JobOpening";
        public static readonly string ServiceJobOpeningInterviewQuestionUri =   "http://localhost:51309/api/JobOpeningInterviewQuestion";
        public static readonly string ServiceJobRequirementUri              =   "http://localhost:51309/api/JobRequirement";
        public static readonly string ServiceJobUri                         =   "http://localhost:51309/api/Job";
        public static readonly string ServiceMCQuestionUri                  =   "http://localhost:51309/api/MCQuestion";
        public static readonly string ServiceMCOptionUri                    =   "http://localhost:51309/api/MCOption";
        public static readonly string ServiceQuestionBankUri                =   "http://localhost:51309/api/QuestionBank";
        public static readonly string ServiceSAQuestionUri                  =   "http://localhost:51309/api/SAQuestion";
        public static readonly string ServiceSAResponseUri                  =   "http://localhost:51309/api/SAResponse";
        public static readonly string ServiceSchoolUri                      =   "http://localhost:51309/api/School";
        public static readonly string ServiceSkillUri                       =   "http://localhost:51309/api/Skill";
        public static readonly string ServiceSkillQuestionBankUri           =   "http://localhost:51309/api/SkillQuestionBank";
        public static readonly string ServiceStoreUri                       =   "http://localhost:51309/api/Store";
        public static readonly string ServiceUserProfileUri                 =   "http://localhost:51309/api/UserProfile";
        public static readonly string ServiceUserProfileToApplicantUri      =   "http://localhost:51309/api/UserProfileToApplicant";
    }

    public class ServerResponse<T> where T : class
    {
        public async static Task<T> GetResponseAsync(string uri)
        {
            using (HttpClient cli = new HttpClient())
            {
                var response = await cli.GetStringAsync(uri);
                return JsonConvert.DeserializeObjectAsync<T>(response).Result;
            }
        }

        public async static Task<T> GetResponseByParamsAsync(string uri, int id = 0, params string[] list)
        {
            string request = uri;
            if (list.Length != 0)
            {
                for (int i = 0; i < list.Length; i++)
                    request += '/' + list[i].ToString();
            }
            else
            {
                request += '/' + id.ToString();
            }

            using (HttpClient cli = new HttpClient())
            {
                var response = await cli.GetStringAsync(request);
                return JsonConvert.DeserializeObjectAsync<T>(response).Result;
            }
        }
    }
}