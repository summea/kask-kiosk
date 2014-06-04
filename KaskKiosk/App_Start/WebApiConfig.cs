using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace KaskKiosk
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "GetItemsForApplicant",
                routeTemplate: "api/{controller}/{first}/{last}/{ssn}",
                defaults: new { action = "GetItemsForApplicant" }
            );
            config.Routes.MapHttpRoute(
                name: "GetApplicationsByApplicant",
                routeTemplate: "api/{controller}/{by_applicant}/{id}",
                defaults: new { action = "GetApplicationsByApplicant" }
            );
            config.Routes.MapHttpRoute(
                name: "GetQuestionBankFromQuestionAndOptionId",
                routeTemplate: "api/{controller}/{questionId}/{optionId}",
                defaults: new { action = "GetQuestionBankFromQuestionAndOptionId" }
            );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
