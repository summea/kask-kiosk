using Kask.DAL2.Models;
using Kask.ServiceLayer.Services.Interfaces;
using System;
using System.Linq;

namespace Kask.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
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
        public Application GetApplicationByApplicantId(int applicantID)
        {
            throw new NotImplementedException();
        }

        /* ================ HTTP Get /Application/ ================ */
        public Application GetApplications()
        {
            throw new NotImplementedException();
        }

        /* ================ HTTP Post /Application/ ================ */
        public Application CreateApplication(Application app)
        {
            throw new NotImplementedException();
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
