using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestService.ApplicationServiceRef;

namespace TestService
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationServiceRef.ApplicationServiceClient client = new ApplicationServiceRef.ApplicationServiceClient();
            Application app = client.GetApplicationById(1);
            Console.WriteLine("{0}: {1}", app.Application_ID, app.ApplicationStatus);
        }
    }
}
