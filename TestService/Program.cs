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
            ApplicationServiceClient client = new ApplicationServiceRef.ApplicationServiceClient();
            object app = client.GetApplicationById(1);
        }
    }
}
