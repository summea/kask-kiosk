using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            ApplicationServiceClient client = new ApplicationServiceRef.ApplicationServiceClient();
            ICollection<Application> apps = client.GetApplications();
            Console.ReadLine();
        }
    }
}
