using Kask.Services.Interfaces;
using Kask.Services.Exceptions;
using Kask.Services.DAO;

using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System;
using Kask.DAL.Models;

namespace Kask.Services
{
    public class AESApplicationService : IApplicationService //, IApplicantService, IAppliedService, IEmployerService
    {
        public ApplicationDAO GetApplicationByID(int id)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    Application application = (from a in db.Applications where a.Application_ID == id select a).First();
                    ApplicationDAO result = new ApplicationDAO
                    {
                        ApplicationID = application.Application_ID,
                        ID = application.Application_ID,
                        ApplicationStatus = application.ApplicationStatus,
                        SalaryExpectation = application.SalaryExpectation,
                        FullTime = application.FullTime,
                        AvailableForDays = application.AvailableForDays,
                        AvailableForEvenings = application.AvailableForEvenings,
                        AvailableForWeekends = application.AvailableForWeekends,
                        MondayFrom = application.MondayFrom,
                        TuesdayFrom = application.TuesdayFrom,
                        WednesdayFrom = application.WednesdayFrom,
                        ThursdayFrom = application.ThursdayFrom,
                        FridayFrom = application.FridayFrom,
                        SaturdayFrom = application.SaturdayFrom,
                        SundayFrom = application.SundayFrom,
                        MondayTo = application.MondayTo,
                        TuesdayTo = application.TuesdayTo,
                        WednesdayTo = application.WednesdayTo,
                        ThursdayTo = application.ThursdayTo,
                        FridayTo = application.FridayTo,
                        SaturdayTo = application.SaturdayTo,
                        SundayTo = application.SundayTo
                    };
                    return (result != null ? result : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<ApplicationDAO> GetApplicationsByName(string first, string last, string ssn)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    IList<Applicant> applicants = (from a in db.Applicants where a.FirstName == first && a.LastName == last && a.SSN == ssn select a).ToList();
                    IList<ApplicationDAO> result = null;

                    foreach (Applicant a in applicants)
                    {
                        result.Add(GetApplicationByID(a.Applicant_ID));
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<ApplicationDAO> GetApplications()
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    IList<Application> apps = db.Applications.ToList();
                    List<ApplicationDAO> result = new List<ApplicationDAO> ();

                    foreach (var application in apps)
                    {
                        ApplicationDAO temp = new ApplicationDAO
                        {
                            ApplicationID = application.Application_ID,
                            ID = application.Application_ID,
                            ApplicationStatus = application.ApplicationStatus,
                            SalaryExpectation = application.SalaryExpectation,
                            FullTime = application.FullTime,
                            AvailableForDays = application.AvailableForDays,
                            AvailableForEvenings = application.AvailableForEvenings,
                            AvailableForWeekends = application.AvailableForWeekends,
                            MondayFrom = application.MondayFrom,
                            TuesdayFrom = application.TuesdayFrom,
                            WednesdayFrom = application.WednesdayFrom,
                            ThursdayFrom = application.ThursdayFrom,
                            FridayFrom = application.FridayFrom,
                            SaturdayFrom = application.SaturdayFrom,
                            SundayFrom = application.SundayFrom,
                            MondayTo = application.MondayTo,
                            TuesdayTo = application.TuesdayTo,
                            WednesdayTo = application.WednesdayTo,
                            ThursdayTo = application.ThursdayTo,
                            FridayTo = application.FridayTo,
                            SaturdayTo = application.SaturdayTo,
                            SundayTo = application.SundayTo                            
                        };

                        result.Add(temp);
                    }
                    return (result != null ? result : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public bool CreateApplication(ApplicationDAO app)
        {
            Application a = new Application
            {
                Application_ID = app.ApplicationID,
                ApplicationStatus = app.ApplicationStatus,
                SalaryExpectation = app.SalaryExpectation,
                FullTime = app.FullTime,
                AvailableForDays = app.AvailableForDays,
                AvailableForEvenings = app.AvailableForEvenings,
                AvailableForWeekends = app.AvailableForWeekends,
                MondayFrom = app.MondayFrom,
                TuesdayFrom = app.TuesdayFrom,
                WednesdayFrom = app.WednesdayFrom,
                ThursdayFrom = app.ThursdayFrom,
                FridayFrom = app.FridayFrom,
                SaturdayFrom = app.SaturdayFrom,
                SundayFrom = app.SundayFrom,
                MondayTo = app.MondayTo,
                TuesdayTo = app.TuesdayTo,
                WednesdayTo = app.WednesdayTo,
                ThursdayTo = app.ThursdayTo,
                FridayTo = app.FridayTo,
                SaturdayTo = app.SaturdayTo,
                SundayTo = app.SundayTo
            };

            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                db.Applications.InsertOnSubmit(a);
                try
                {
                    db.SubmitChanges();
                }
                catch (Exception e)
                {
                    throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
                }
            }

            return true;
        }

        public bool UpdateApplication(ApplicationDAO newApp)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                Application a = db.Applications.Single(app => app.Application_ID == newApp.ApplicationID);
                a.ApplicationStatus = newApp.ApplicationStatus;
                a.AvailableForDays = newApp.AvailableForDays;
                a.AvailableForEvenings = newApp.AvailableForEvenings;
                a.AvailableForWeekends = newApp.AvailableForWeekends;
                a.FridayFrom = newApp.FridayFrom;
                a.FridayTo = newApp.FridayTo;
                a.FullTime = newApp.FullTime;
                a.MondayFrom = newApp.MondayFrom;
                a.MondayTo = newApp.MondayTo;
                a.SalaryExpectation = newApp.SalaryExpectation;
                a.SaturdayFrom = newApp.SaturdayFrom;
                a.SaturdayTo = newApp.SaturdayTo;
                a.SundayFrom = newApp.SundayFrom;
                a.SundayTo = newApp.SundayTo;
                a.ThursdayFrom = newApp.ThursdayFrom;
                a.ThursdayTo = newApp.ThursdayTo;
                a.TuesdayFrom = newApp.TuesdayFrom;
                a.TuesdayTo = newApp.TuesdayTo;
                a.WednesdayFrom = newApp.WednesdayFrom;
                a.WednesdayTo = newApp.WednesdayTo;

                try
                {
                    db.SubmitChanges();
                }
                catch (Exception e)
                {
                    throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
                }
            }

            return true;
        }

        public bool DeleteApplication(int ID)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                Application a = db.Applications.Single(app => app.Application_ID == ID);
                db.Applications.DeleteOnSubmit(a);

                try
                {
                    db.SubmitChanges();
                }
                catch (Exception e)
                {
                    throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
                }
            }

            return true;
        }

        /// TODO: UNCOMMENT ME
        /*
        public Applicant GetApplicantByID(int id)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    Applicant applicant = (from a in db.Applicants where a.Applicant_ID == id select a).First();
                    return (applicant != null ? applicant : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<Applicant> GetApplicants()
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    var apps = db.Applicants.ToList();
                    return (apps != null ? apps : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public bool CreateApplicant(Applicant a)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                db.Applicants.InsertOnSubmit(a);
                try
                {
                    db.SubmitChanges();
                }
                catch (Exception e)
                {
                    throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
                }
            }

            return true;
        }

        public bool UpdateApplicant(Applicant newApp)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                Applicant a = db.Applicants.Single(app => app.Applicant_ID == newApp.Applicant_ID);
                a.FirstName = newApp.FirstName;
                a.MiddleName = newApp.MiddleName;
                a.LastName = newApp.LastName;
                a.NameAlias = newApp.NameAlias;
                a.Gender = newApp.Gender;
                a.SSN = newApp.SSN;
                a.ApplicantAddress = newApp.ApplicantAddress;
                a.Phone = newApp.Phone;

                try
                {
                    db.SubmitChanges();
                }
                catch (Exception e)
                {
                    throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
                }
            }

            return true;
        }

        public bool DeleteApplicant(int ID)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                Applicant a = db.Applicants.Single(app => app.Applicant_ID == ID);
                db.Applicants.DeleteOnSubmit(a);

                try
                {
                    db.SubmitChanges();
                }
                catch (Exception e)
                {
                    throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
                }
            }

            return true;
        }

        public Applied GetAppliedByID(int id)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    var applied = db.Applieds.Single(a => a.Applicant_ID == id);
                    return (applied != null ? applied : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<Applied> GetApplieds()
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    var apps = db.Applieds.ToList();
                    return (apps != null ? apps : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public bool CreateApplied(Applied a)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                db.Applieds.InsertOnSubmit(a);
                try
                {
                    db.SubmitChanges();
                }
                catch (Exception e)
                {
                    throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
                }
            }

            return true;
        }

        public bool DeleteApplied(int id)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                Applied a = db.Applieds.Single(app => app.Applicant_ID == id);
                db.Applieds.DeleteOnSubmit(a);

                try
                {
                    db.SubmitChanges();
                }
                catch (Exception e)
                {
                    throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
                }
            }

            return true;
        }

        public Employer GetEmployerByID(int id)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    var emps = db.Employers.Single(a => a.Employer_ID == id);
                    return (emps != null ? emps : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<Employer> GetEmployers()
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    var emps = db.Employers.ToList();
                    return (emps != null ? emps : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public bool CreateEmployer(Employer emp)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                db.Employers.InsertOnSubmit(emp);
                try
                {
                    db.SubmitChanges();
                }
                catch (Exception e)
                {
                    throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
                }
            }

            return true;
        }

        public bool UpdateEmployer(Employer newEmp)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                Employer emp = db.Employers.Single(e => e.Employer_ID == newEmp.Employer_ID);
                emp.EmployerAddress = newEmp.EmployerAddress;
                emp.Name = newEmp.Name;
                emp.PhoneNumber = newEmp.PhoneNumber;

                try
                {
                    db.SubmitChanges();
                }
                catch (Exception e)
                {
                    throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
                }
            }

            return true;

        }

        public bool DeleteEmployer(int id)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                Employer emp = db.Employers.Single(e => e.Employer_ID == id);
                db.Employers.DeleteOnSubmit(emp);

                try
                {
                    db.SubmitChanges();
                }
                catch (Exception e)
                {
                    throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
                }
            }

            return true;
        }
    */
        /// TODO: UNCOMMENT THIS BLOCK
    }
}