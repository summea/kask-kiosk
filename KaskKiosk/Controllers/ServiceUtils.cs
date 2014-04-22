using Newtonsoft.Json;
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;

namespace KaskKiosk.Controllers
{
    public class ServiceURIs
    {
        public static readonly string ServiceApplicantUri   =   "http://localhost:51309/api/Applicant";
        public static readonly string ServiceApplicationUri =   "http://localhost:51309/api/Application";
        public static readonly string ServiceAppliedUri     =   "http://localhost:51309/api/Applied";
        public static readonly string ServiceEducationUri   =   "http://localhost:51309/api/Education";
        public static readonly string ServiceEmployerUri    =   "http://localhost:51309/api/Employer";
        public static readonly string ServiceEmploymentUri  =   "http://localhost:51309/api/Employment";
        public static readonly string ServiceJobOpeningUri  =   "http://localhost:51309/api/JobOpening";
        public static readonly string ServiceJobUri         =   "http://localhost:51309/api/Job";
        public static readonly string ServiceSchoolUri      =   "http://localhost:51309/api/School";
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