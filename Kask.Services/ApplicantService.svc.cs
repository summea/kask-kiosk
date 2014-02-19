using Kask.DAL2.Models;
using Kask.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Kask.Services
{
    public class ApplicantService : IApplicationService
    {
        public Application GetApplicationById(int id)
        {
            AESDatabasev2DataContext db = new AESDatabasev2DataContext();
            Application application = (from a in db.Applications where a.Application_ID == id select a).First();

            if (application != null)
                return application;
            else return null;
        }

        public IList<Application> GetApplicationsByApplicantId(int applicantID)
        {
            AESDatabasev2DataContext db = new AESDatabasev2DataContext();
            IQueryable<Applied> query = (from a in db.Applieds where a.Applicant_ID == applicantID select a).AsQueryable();
            

            throw new FaultException ("Unknown Error");
        }

        public IList<Application> GetApplications()
        {
            AESDatabasev2DataContext db = new AESDatabasev2DataContext();
            var apps = db.Applications.ToList();

            if (apps != null)
                return apps;

            throw new FaultException("Unknown Error");
        }

        public bool CreateApplication(Application app)
        {
            AESDatabasev2DataContext db = new AESDatabasev2DataContext();
            db.Applications.InsertOnSubmit(app);
            try
            {
                db.SubmitChanges();
            }
            catch (FaultException e)
            {
                throw e;
            }

            return true;
        }

        public bool UpdateApplication(Application oldApp, Application newApp)
        {
            throw new NotImplementedException();
        }

        public bool DeleteApplication(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
