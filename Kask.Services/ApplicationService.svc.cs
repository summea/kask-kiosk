using Kask.DAL2.Models;
using Kask.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kask.Services
{
    public class ApplicationService : IApplicationService
    {
        /* ================ HTTP GET /Application/id ================ */
        public Application GetApplicationById(int id)
        {
            AESDatabasev2DataContext db = new AESDatabasev2DataContext();
            Application application = (from a in db.Applications where a.Application_ID == id select a).First();

            if (application != null)
            {
                return application;
            }
            else throw new Exception("Invalid Application ID");
        }

        /* ================ HTTP GET /Application/Applicant/id ================ */
        public IList<Application> GetApplicationsByApplicantId(int applicantID)
        {
            AESDatabasev2DataContext db = new AESDatabasev2DataContext();
            var query = (from a in db.Applieds where a.Applicant_ID == applicantID select a).ToList();


            throw new Exception("Invalid Applicant ID");
        }

        /* ================ HTTP Get /Application/ ================ */
        public IList<Application> GetApplications()
        {
            AESDatabasev2DataContext db = new AESDatabasev2DataContext();
            var apps = db.Applications.ToList();

            if (apps != null)
            {
                return apps;
            }

            throw new Exception("No application found");
        }

        /* ================ HTTP Post /Application/ ================ */
        public bool CreateApplication(Application app)
        {
            return true;
        }

        /* ================ HTTP Put /Application/id/ ================ */
        public bool UpdateApplication(int ID)
        {
            throw new NotImplementedException();
        }

        /* ================ HTTP Delete /Application/id/ ================ */
        public bool DeleteApplication(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
