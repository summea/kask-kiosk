using Kask.DAL2.Models;
using Kask.ServiceLayer.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Kask.ServiceLayer
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
        public ObservableCollection<Application> GetApplicationsByApplicantId(int applicantID)
        {
            AESDatabasev2DataContext db = new AESDatabasev2DataContext();
            ObservableCollection<Application> result = new ObservableCollection<Application>();
            var query = (from a in db.Applieds where a.Applicant_ID == applicantID select a).ToList();
            
            
            throw new Exception("Invalid Applicant ID");
        }

        /* ================ HTTP Get /Application/ ================ */
        public ObservableCollection<Application> GetApplications()
        {
            AESDatabasev2DataContext db = new AESDatabasev2DataContext();
            ObservableCollection<Application> results = new ObservableCollection<Application>();
            var apps = db.Applications.ToList();

            if (apps != null)
            {
                foreach (var item in apps)
                {
                    results.Add(item);
                }

                return results;
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
