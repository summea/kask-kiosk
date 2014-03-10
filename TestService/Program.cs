﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestService.AESApplicationServiceRef;
using Kask.DAL.Models;

namespace TestService
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationServiceClient client = new ApplicationServiceClient();
            IList<Application> apps = client.GetApplications();
            Console.ReadLine();
        }
    }
}
