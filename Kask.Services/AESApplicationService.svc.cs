using Kask.DAL2.Models;
using Kask.Services.Exceptions;
using Kask.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Kask.Services
{
    public class AESApplicationService : IApplicationService, IApplicantService, IAppliedService
    {
        public Application GetApplicationById(int id)
        {
            try
            {
                using (AESDatabasev2DataContext db = new AESDatabasev2DataContext())
                {
                    Application application = (from a in db.Applications where a.Application_ID == id select a).First();
                    return (application != null ? application : null);
                }
            }
            catch(FaultException<KaskServiceExceptions> se)
            {
                throw se;
            }
        }

        public IList<Application> GetApplications()
        {
            try
            {
                using (AESDatabasev2DataContext db = new AESDatabasev2DataContext())
                {
                    var apps = db.Applications.ToList();
                    return (apps != null ? apps : null);
                }
            }
            catch
            {
                throw new FaultException("Unhandled Exception");
            }
        }

        public bool CreateApplication(Application app)
        {
            using (AESDatabasev2DataContext db = new AESDatabasev2DataContext())
            {
                db.Applications.InsertOnSubmit(app);
                try
                {
                    db.SubmitChanges();
                }
                catch (FaultException e)
                {
                    throw e;
                }
            }

            return true;
        }

        public bool UpdateApplication(Application newApp)
        {
            using (AESDatabasev2DataContext db = new AESDatabasev2DataContext())
            {
                Application a = db.Applications.Single(app => app.Application_ID == newApp.Application_ID);
                a.ApplicationStatus = newApp.ApplicationStatus;
                try
                {
                    db.SubmitChanges();
                }
                catch (FaultException e)
                {
                    
                    throw e;
                }
            }

            return true;
        }

        public bool DeleteApplication(int ID)
        {
            using (AESDatabasev2DataContext db = new AESDatabasev2DataContext())
            {
                Application a = db.Applications.Single(app => app.Application_ID == ID);
                db.Applications.DeleteOnSubmit(a);

                try
                {
                    db.SubmitChanges();
                }
                catch (FaultException e)
                {
                    
                    throw e;
                }
            }

            return true;
        }

        public Applicant GetApplicantByID(int id)
        {
            try
            {
                using (AESDatabasev2DataContext db = new AESDatabasev2DataContext())
                {
                    Applicant applicant = (from a in db.Applicants where a.Applicant_ID == id select a).First();
                    return (applicant != null ? applicant : null);
                }
            }
            catch
            {
                throw new FaultException("Unhandled Exception");
            }
        }

        public IList<Applicant> GetApplicants()
        {
            try
            {
                using (AESDatabasev2DataContext db = new AESDatabasev2DataContext())
                {
                    var apps = db.Applicants.ToList();
                    return (apps != null ? apps : null);
                }
            }
            catch
            {
                throw new FaultException("Unhandled Exception");
            }
        }

        public bool CreateApplicant(Applicant a)
        {
            using (AESDatabasev2DataContext db = new AESDatabasev2DataContext())
            {
                db.Applicants.InsertOnSubmit(a);
                try
                {
                    db.SubmitChanges();
                }
                catch (FaultException e)
                {
                    throw e;
                }
            }

            return true;
        }

        public bool UpdateApplicant(Applicant newApp)
        {
            using (AESDatabasev2DataContext db = new AESDatabasev2DataContext())
            {
                Applicant a = db.Applicants.Single(app => app.Applicant_ID == newApp.Applicant_ID);
                a.FirstName = newApp.FirstName;
                a.LastName = newApp.LastName;
                a.Gender = newApp.Gender;
                a.SSN = newApp.SSN;

                try
                {
                    db.SubmitChanges();
                }
                catch (FaultException e)
                {
                    throw e;
                }
            }

            return true;
        }

        public bool DeleteApplicant(int ID)
        {
            using (AESDatabasev2DataContext db = new AESDatabasev2DataContext())
            {
                Applicant a = db.Applicants.Single(app => app.Applicant_ID == ID);
                db.Applicants.DeleteOnSubmit(a);

                try
                {
                    db.SubmitChanges();
                }
                catch (FaultException e)
                {

                    throw e;
                }
            }

            return true;
        }

        public Applied GetAppliedByID(int id)
        {
            throw new System.NotImplementedException();
        }

        public IList<Applied> GetApplieds()
        {
            try
            {
                using (AESDatabasev2DataContext db = new AESDatabasev2DataContext())
                {
                    var apps = db.Applieds.ToList();
                    return (apps != null ? apps : null);
                }
            }
            catch
            {
                throw new FaultException("Unhandled Exception");
            }
        }

        public bool CreateApplied(Applied a)
        {
            using (AESDatabasev2DataContext db = new AESDatabasev2DataContext())
            {
                db.Applieds.InsertOnSubmit(a);
                try
                {
                    db.SubmitChanges();
                }
                catch (FaultException e)
                {
                    throw e;
                }
            }

            return true;
        }

        public bool DeleteApplied(int id)
        {
            using (AESDatabasev2DataContext db = new AESDatabasev2DataContext())
            {
                Applied a = db.Applieds.Single(app => app.Applicant_ID == id);
                db.Applieds.DeleteOnSubmit(a);

                try
                {
                    db.SubmitChanges();
                }
                catch (FaultException e)
                {

                    throw e;
                }
            }

            return true;
        }
    }
}
