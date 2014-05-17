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
    public class AESApplicationService : IApplicationService, IApplicantService, IAppliedService, IAssessmentService, IEducationService, IEmployerService, IEmploymentService, IExpertiseService, IInterviewService,
                                        IJobService, IJobOpeningService, IJobRequirementService, IMCOptionService, IMCQuestionService, IQuestionBankService, ISAQuestionService, ISAResponseService, ISchoolService, ISkillService, ISkillQuestionBankService, IStoreService
    {
        public ApplicationDAO GetApplicationByID(int id)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    Application application = (from a in db.Applications where a.Application_ID == id select a).FirstOrDefault();
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
                    List<ApplicationDAO> result = new List<ApplicationDAO>();

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
                    List<ApplicationDAO> result = new List<ApplicationDAO>();

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

        public ApplicantDAO GetApplicantByID(int id)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    Applicant applicant = (from a in db.Applicants where a.Applicant_ID == id select a).FirstOrDefault();
                    ApplicantDAO result = new ApplicantDAO
                    {
                        ApplicantID = applicant.Applicant_ID,
                        ID = applicant.Applicant_ID,
                        FirstName = applicant.FirstName,
                        MiddleName = applicant.MiddleName,
                        LastName = applicant.LastName,
                        SSN = applicant.SSN,
                        Gender = applicant.Gender,
                        ApplicantAddress = applicant.ApplicantAddress,
                        Phone = applicant.Phone,
                        NameAlias = applicant.NameAlias
                    };

                    return (result != null ? result : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<ApplicantDAO> GetApplicants()
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    IList<Applicant> apps = db.Applicants.ToList();
                    List<ApplicantDAO> result = new List<ApplicantDAO>();
                    foreach (var applicant in apps)
                    {
                        ApplicantDAO temp = new ApplicantDAO
                        {
                            ApplicantID = applicant.Applicant_ID,
                            ID = applicant.Applicant_ID,
                            FirstName = applicant.FirstName,
                            MiddleName = applicant.MiddleName,
                            LastName = applicant.LastName,
                            SSN = applicant.SSN,
                            Gender = applicant.Gender,
                            ApplicantAddress = applicant.ApplicantAddress,
                            Phone = applicant.Phone,
                            NameAlias = applicant.NameAlias
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

        public bool CreateApplicant(ApplicantDAO app)
        {
            Applicant a = new Applicant
            {
                Applicant_ID = app.ApplicantID,
                FirstName = app.FirstName,
                MiddleName = app.MiddleName,
                LastName = app.LastName,
                SSN = app.SSN,
                Gender = app.Gender,
                ApplicantAddress = app.ApplicantAddress,
                Phone = app.Phone,
                NameAlias = app.NameAlias
            };

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

        public bool UpdateApplicant(ApplicantDAO newApp)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                Applicant a = db.Applicants.Single(app => app.Applicant_ID == newApp.ApplicantID);
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

        public AppliedDAO GetAppliedByID(int id)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    Applied applied = db.Applieds.Single(a => a.Applied_ID == id);
                    AppliedDAO result = new AppliedDAO
                    {
                        ID = applied.Applied_ID,
                        AppliedID = applied.Applied_ID,
                        ApplicantID = applied.Applicant_ID,
                        ApplicationID = applied.Application_ID,
                        JobOpeningID = applied.JobOpening_ID,
                        DateApplied = applied.DateApplied
                    };

                    return (result != null ? result : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<AppliedDAO> GetApplieds()
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    IList<Applied> applieds = (from applied in db.Applieds select applied).OrderBy(o => o.Applied_ID).ToList();
                    List<AppliedDAO> result = new List<AppliedDAO>();
                    foreach (var a in applieds)
                    {
                        AppliedDAO temp = new AppliedDAO
                        {
                            ID = a.Applied_ID,
                            AppliedID = a.Applied_ID,
                            ApplicantID = a.Applicant_ID,
                            ApplicationID = a.Application_ID,
                            JobOpeningID = a.JobOpening_ID,
                            DateApplied = a.DateApplied
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

        public IList<AppliedDAO> GetAppliedsByName(string first, string last, string ssn)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    Applicant applicant = (from a in db.Applicants where a.SSN == ssn && a.FirstName == first && a.LastName == last select a).FirstOrDefault();
                    IList<Applied> applieds = (from a in db.Applieds where a.Applicant_ID == applicant.Applicant_ID select a).ToList();
                    List<AppliedDAO> result = new List<AppliedDAO>();

                    foreach (var a in applieds)
                    {
                        AppliedDAO app = new AppliedDAO
                        {
                            ID = a.Applied_ID,
                            AppliedID = a.Applied_ID,
                            ApplicantID = a.Applicant_ID,
                            ApplicationID = a.Application_ID,
                            JobOpeningID = a.JobOpening_ID,
                            DateApplied = a.DateApplied
                        };

                        result.Add(app);
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public bool CreateApplied(AppliedDAO app)
        {
            Applied a = new Applied
            {
                Applied_ID = app.AppliedID,
                Applicant_ID = app.ApplicantID,
                Application_ID = app.ApplicationID,
                JobOpening_ID = app.JobOpeningID,
                DateApplied = app.DateApplied
            };

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

        public bool UpdateApplied(AppliedDAO newApp)
        {
            throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason("Unsupported Method"));
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

        public EmployerDAO GetEmployerByID(int id)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    Employer employer = (from em in db.Employers where em.Employer_ID == id select em).FirstOrDefault();
                    EmployerDAO result = new EmployerDAO
                    {
                        EmployerID = employer.Employer_ID,
                        Name = employer.Name,
                        EmployerAddress = employer.EmployerAddress,
                        PhoneNumber = employer.PhoneNumber
                    };
                    return (result != null ? result : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<EmployerDAO> GetEmployers()
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    IList<Employer> emps = db.Employers.ToList();
                    List<EmployerDAO> result = new List<EmployerDAO>();
                    foreach (var employer in emps)
                    {
                        EmployerDAO temp = new EmployerDAO
                        {
                            EmployerID = employer.Employer_ID,
                            Name = employer.Name,
                            EmployerAddress = employer.EmployerAddress,
                            PhoneNumber = employer.PhoneNumber
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

        public bool CreateEmployer(EmployerDAO emp)
        {
            Employer employer = new Employer
            {
                Name = emp.Name,
                EmployerAddress = emp.EmployerAddress,
                PhoneNumber = emp.PhoneNumber
            };

            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                db.Employers.InsertOnSubmit(employer);
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

        public bool UpdateEmployer(EmployerDAO newEmp)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                Employer em = db.Employers.Single(emp => emp.Employer_ID == newEmp.EmployerID);

                em.Name = newEmp.Name;
                em.EmployerAddress = newEmp.EmployerAddress;
                em.PhoneNumber = newEmp.PhoneNumber;

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

        public bool DeleteEmployer(int ID)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                Employer em = db.Employers.Single(emp => emp.Employer_ID == ID);
                db.Employers.DeleteOnSubmit(em);

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

        public EmploymentDAO GetEmploymentByID(int id)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    Employment employment = (from em in db.Employments where em.Employment_ID == id select em).FirstOrDefault();
                    EmploymentDAO result = new EmploymentDAO
                    {
                        EmploymentID = employment.Employment_ID,
                        ApplicantID = employment.Applicant_ID,
                        EmployerID = employment.Employer_ID,
                        MayWeContactCurrentEmployer = employment.MayWeContactCurrentEmployer,
                        EmployedFrom = employment.EmployedFrom,
                        EmployedTo = employment.EmployedTo,
                        Supervisor = employment.Supervisor,
                        Position = employment.Position,
                        StartingSalary = employment.StartingSalary,
                        EndingSalary = employment.EndingSalary,
                        ReasonForLeaving = employment.ReasonForLeaving,
                        Responsibilities = employment.Responsibilities
                    };
                    return (result != null ? result : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<EmploymentDAO> GetEmployments()
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    IList<Employment> emps = db.Employments.ToList();
                    List<EmploymentDAO> result = new List<EmploymentDAO>();
                    foreach (var employment in emps)
                    {
                        EmploymentDAO temp = new EmploymentDAO
                        {
                            EmploymentID = employment.Employment_ID,
                            ApplicantID = employment.Applicant_ID,
                            EmployerID = employment.Employer_ID,
                            MayWeContactCurrentEmployer = employment.MayWeContactCurrentEmployer,
                            EmployedFrom = employment.EmployedFrom,
                            EmployedTo = employment.EmployedTo,
                            Supervisor = employment.Supervisor,
                            Position = employment.Position,
                            StartingSalary = employment.StartingSalary,
                            EndingSalary = employment.EndingSalary,
                            ReasonForLeaving = employment.ReasonForLeaving,
                            Responsibilities = employment.Responsibilities
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

        public IList<EmploymentDAO> GetEmploymentsByName (string first, string last, string ssn)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    Applicant applicant = (from a in db.Applicants where a.SSN == ssn && a.FirstName == first && a.LastName == last select a).FirstOrDefault();
                    IList<Employment> employments = (from e in db.Employments where e.Applicant_ID == applicant.Applicant_ID select e).ToList();
                    List<EmploymentDAO> result = new List<EmploymentDAO>();

                    foreach (var e in employments)
                    {
                        EmploymentDAO emp = new EmploymentDAO
                        {
                            ID = e.Employment_ID,
                            EmploymentID = e.Employment_ID,
                            ApplicantID = e.Applicant_ID,
                            EmployerID = e.Employer_ID,
                            MayWeContactCurrentEmployer = e.MayWeContactCurrentEmployer,
                            EmployedFrom = e.EmployedFrom,
                            EmployedTo = e.EmployedTo,
                            Supervisor = e.Supervisor,
                            Position = e.Position,
                            StartingSalary = e.StartingSalary,
                            EndingSalary = e.EndingSalary,
                            ReasonForLeaving = e.ReasonForLeaving,
                            Responsibilities = e.Responsibilities
                        };

                        result.Add(emp);
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public bool CreateEmployment(EmploymentDAO emp)
        {
            Employment employment = new Employment
            {
                Applicant_ID = emp.ApplicantID,
                Employer_ID = emp.EmployerID,
                MayWeContactCurrentEmployer = emp.MayWeContactCurrentEmployer,
                EmployedFrom = emp.EmployedFrom,
                EmployedTo = emp.EmployedTo,
                Supervisor = emp.Supervisor,
                Position = emp.Position,
                StartingSalary = emp.StartingSalary,
                EndingSalary = emp.EndingSalary,
                ReasonForLeaving = emp.ReasonForLeaving,
                Responsibilities = emp.Responsibilities
            };

            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                db.Employments.InsertOnSubmit(employment);
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

        public bool UpdateEmployment(EmploymentDAO newEmp)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                Employment em = db.Employments.Single(emp => emp.Employment_ID == newEmp.EmploymentID);

                em.Applicant_ID = newEmp.ApplicantID;
                em.Employer_ID = newEmp.EmployerID;
                em.MayWeContactCurrentEmployer = newEmp.MayWeContactCurrentEmployer;
                em.EmployedFrom = newEmp.EmployedFrom;
                em.EmployedTo = newEmp.EmployedTo;
                em.Supervisor = newEmp.Supervisor;
                em.Position = newEmp.Position;
                em.StartingSalary = newEmp.StartingSalary;
                em.EndingSalary = newEmp.EndingSalary;
                em.ReasonForLeaving = newEmp.ReasonForLeaving;
                em.Responsibilities = newEmp.Responsibilities;

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

        public bool DeleteEmployment(int ID)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                Employment em = db.Employments.Single(emp => emp.Employment_ID == ID);
                db.Employments.DeleteOnSubmit(em);

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

        public EducationDAO GetEducationByID(int id)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    Education education = (from edu in db.Educations where edu.Education_ID == id select edu).FirstOrDefault();
                    EducationDAO result = new EducationDAO
                    {
                        EducationID = education.Education_ID,
                        ApplicantID = education.Applicant_ID,
                        SchoolID = education.School_ID,
                        YearsAttendedFrom = education.YearsAttendedFrom,
                        YearsAttendedTo = education.YearsAttendedTo,
                        Graduated = education.Graduated,
                        DegreeAndMajor = education.DegreeAndMajor
                    };
                    return (result != null ? result : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<EducationDAO> GetEducations()
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    IList<Education> educations = db.Educations.ToList();
                    List<EducationDAO> result = new List<EducationDAO>();
                    foreach (var education in educations)
                    {
                        EducationDAO temp = new EducationDAO
                        {
                            EducationID = education.Education_ID,
                            ApplicantID = education.Applicant_ID,
                            SchoolID = education.School_ID,
                            YearsAttendedFrom = education.YearsAttendedFrom,
                            YearsAttendedTo = education.YearsAttendedTo,
                            Graduated = education.Graduated,
                            DegreeAndMajor = education.DegreeAndMajor
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

        public IList<EducationDAO> GetEducationsByName (string first, string last, string ssn)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    Applicant applicant = (from a in db.Applicants where a.SSN == ssn && a.FirstName == first && a.LastName == last select a).FirstOrDefault();
                    IList<Education> educations = (from e in db.Educations where e.Applicant_ID == applicant.Applicant_ID select e).ToList();
                    List<EducationDAO> result = new List<EducationDAO>();

                    foreach (var e in educations)
                    {
                        EducationDAO edu = new EducationDAO
                        {
                            ID = e.Education_ID,
                            EducationID = e.Education_ID,
                            ApplicantID = e.Applicant_ID,
                            SchoolID = e.School_ID,
                            YearsAttendedFrom = e.YearsAttendedFrom,
                            YearsAttendedTo = e.YearsAttendedTo,
                            Graduated = e.Graduated,
                            DegreeAndMajor = e.DegreeAndMajor
                        };

                        result.Add(edu);
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public bool CreateEducation(EducationDAO edu)
        {
            Education education = new Education
            {
                Applicant_ID = edu.ApplicantID,
                School_ID = edu.SchoolID,
                YearsAttendedFrom = edu.YearsAttendedFrom,
                YearsAttendedTo = edu.YearsAttendedTo,
                Graduated = edu.Graduated,
                DegreeAndMajor = edu.DegreeAndMajor
            };

            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                db.Educations.InsertOnSubmit(education);
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

        public bool UpdateEducation(EducationDAO newEdu)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                Education education = db.Educations.Single(edu => edu.Education_ID == newEdu.EducationID);

                education.Applicant_ID = newEdu.ApplicantID;
                education.School_ID = newEdu.SchoolID;
                education.YearsAttendedFrom = newEdu.YearsAttendedFrom;
                education.YearsAttendedTo = newEdu.YearsAttendedTo;
                education.Graduated = newEdu.Graduated;
                education.DegreeAndMajor = newEdu.DegreeAndMajor;

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

        public bool DeleteEducation(int ID)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                Education education = db.Educations.Single(edu => edu.Education_ID == ID);
                db.Educations.DeleteOnSubmit(education);

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

        public SchoolDAO GetSchoolByID(int id)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    School school = (from sch in db.Schools where sch.School_ID == id select sch).FirstOrDefault();
                    SchoolDAO result = new SchoolDAO
                    {
                        SchoolID = school.School_ID,
                        SchoolName = school.School_Name,
                        SchoolAddress = school.School_Address
                    };
                    return (result != null ? result : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<SchoolDAO> GetSchools()
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    IList<School> schools = db.Schools.ToList();
                    List<SchoolDAO> result = new List<SchoolDAO>();
                    foreach (var school in schools)
                    {
                        SchoolDAO temp = new SchoolDAO
                        {
                            SchoolID = school.School_ID,
                            SchoolName = school.School_Name,
                            SchoolAddress = school.School_Address
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

        public bool CreateSchool(SchoolDAO sch)
        {
            School school = new School
            {
                School_Name = sch.SchoolName,
                School_Address = sch.SchoolAddress
            };

            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                db.Schools.InsertOnSubmit(school);
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

        public bool UpdateSchool(SchoolDAO newSch)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                School school = db.Schools.Single(sch => sch.School_ID == newSch.SchoolID);

                school.School_Name = newSch.SchoolName;
                school.School_Address = newSch.SchoolAddress;

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

        public bool DeleteSchool(int ID)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                School school = db.Schools.Single(sch => sch.School_ID == ID);
                db.Schools.DeleteOnSubmit(school);

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

        public JobDAO GetJobByID(int id)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    Job job = (from jb in db.Jobs where jb.Job_ID == id select jb).FirstOrDefault();
                    JobDAO result = new JobDAO
                    {
                        ID = job.Job_ID,
                        JobID = job.Job_ID,
                        Title = job.Title
                    };
                    return (result != null ? result : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<JobDAO> GetJobs()
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    IList<Job> jobs = db.Jobs.ToList();
                    List<JobDAO> result = new List<JobDAO>();

                    foreach (var job in jobs)
                    {
                        JobDAO temp = new JobDAO
                        {
                            ID = job.Job_ID,
                            JobID = job.Job_ID,
                            Title = job.Title
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

        public bool CreateJob(JobDAO jb)
        {
            Job job = new Job
            {
                Title = jb.Title,
            };

            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                db.Jobs.InsertOnSubmit(job);
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

        public bool UpdateJob(JobDAO newJb)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                Job job = db.Jobs.Single(jb => jb.Job_ID == newJb.JobID);
                job.Title = newJb.Title;

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

        public bool DeleteJob(int ID)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                Job job = db.Jobs.Single(jb => jb.Job_ID == ID);
                db.Jobs.DeleteOnSubmit(job);

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

        public JobOpeningDAO GetJobOpeningByID(int id)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    JobOpening jobOpening = (from jbOp in db.JobOpenings where jbOp.JobOpening_ID == id select jbOp).FirstOrDefault();
                    JobOpeningDAO result = new JobOpeningDAO
                    {
                        ID = jobOpening.JobOpening_ID,
                        JobOpeningID = jobOpening.JobOpening_ID,
                        OpenDate = jobOpening.OpenDate,
                        JobID = jobOpening.Job_ID,
                        Approved = (byte)jobOpening.Approved,
                        Description = jobOpening.Description,
                        StoreID = jobOpening.Store_ID
                    };
                    return (result != null ? result : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<JobOpeningDAO> GetJobOpeningsByStoreID (int storeID)
        {
            try 
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    //IList<Store> stores = (from store in db.Stores where store.Store_ID == storeID select store).ToList();
                    IList<JobOpening> jobOpenings = (from jobOpening in db.JobOpenings where jobOpening.Store_ID == storeID select jobOpening).ToList();
                    List<JobOpeningDAO> result = new List<JobOpeningDAO>();

                    foreach (var str in jobOpenings)
                    {
                        result.Add(GetJobOpeningByID(str.JobOpening_ID));
                    }

                    return (result != null ? result : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<JobOpeningDAO> GetJobOpenings()
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    IList<JobOpening> jobOpenings = db.JobOpenings.ToList();
                    List<JobOpeningDAO> result = new List<JobOpeningDAO>();

                    foreach (var jobOpening in jobOpenings)
                    {
                        JobOpeningDAO temp = new JobOpeningDAO
                        {
                            ID = jobOpening.JobOpening_ID,
                            JobOpeningID = jobOpening.JobOpening_ID,
                            OpenDate = jobOpening.OpenDate,
                            JobID = jobOpening.Job_ID,
                            Approved = (byte)jobOpening.Approved,
                            Description = jobOpening.Description,
                            StoreID = jobOpening.Store_ID
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

        public bool CreateJobOpening(JobOpeningDAO jbOp)
        {
            JobOpening jobOpening = new JobOpening
            {
                JobOpening_ID = jbOp.JobOpeningID,
                OpenDate = (DateTime)jbOp.OpenDate,
                Job_ID = jbOp.JobID,
                Approved = (byte)jbOp.Approved,
                Description = jbOp.Description,
                Store_ID = jbOp.StoreID
            };

            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                db.JobOpenings.InsertOnSubmit(jobOpening);
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

        public bool UpdateJobOpening(JobOpeningDAO newJbOp)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                JobOpening jobOpening = db.JobOpenings.Single(jbOp => jbOp.JobOpening_ID == newJbOp.JobOpeningID);

                if (newJbOp.JobOpeningID > 0)
                    jobOpening.JobOpening_ID = newJbOp.JobOpeningID;

                if (newJbOp.OpenDate != null)
                    jobOpening.OpenDate = (DateTime)newJbOp.OpenDate;

                if (newJbOp.JobID > 0)
                    jobOpening.Job_ID = newJbOp.JobID;

                if (newJbOp.Approved == 0 || newJbOp.Approved == 1)
                    jobOpening.Approved = (byte)newJbOp.Approved;

                jobOpening.Description = newJbOp.Description;

                if (newJbOp.StoreID > 0)
                    jobOpening.Store_ID = newJbOp.StoreID;

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

        public bool DeleteJobOpening(int ID)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                JobOpening jobOpening = db.JobOpenings.Single(jbOp => jbOp.JobOpening_ID == ID);
                db.JobOpenings.DeleteOnSubmit(jobOpening);

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

        public SkillDAO GetSkillByID(int id)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    Skill skill = (from sk in db.Skills where sk.Skill_ID == id select sk).FirstOrDefault();
                    SkillDAO result = new SkillDAO
                    {
                        ID = skill.Skill_ID,
                        SkillID = skill.Skill_ID,
                        SkillName = skill.SkillName
                    };
                    return (result != null ? result : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<SkillDAO> GetSkills()
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    IList<Skill> skills = (from sk in db.Skills select sk).OrderBy(o => o.Skill_ID).ToList();
                    List<SkillDAO> result = new List<SkillDAO>();

                    foreach (var skill in skills)
                    {
                        SkillDAO temp = new SkillDAO
                        {
                            ID = skill.Skill_ID,
                            SkillID = skill.Skill_ID,
                            SkillName = skill.SkillName
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

        public bool CreateSkill(SkillDAO s)
        {
            Skill skill = new Skill
            {
                Skill_ID = s.SkillID,
                SkillName = s.SkillName
            };

            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                db.Skills.InsertOnSubmit(skill);

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

        public bool UpdateSkill(SkillDAO newSkill)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                Skill skill = db.Skills.Single(s => s.Skill_ID == newSkill.SkillID);
                skill.SkillName = newSkill.SkillName;

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

        public bool DeleteSkill(int id)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                Skill skill = db.Skills.Single(sk => sk.Skill_ID == id);
                db.Skills.DeleteOnSubmit(skill);

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

        public ExpertiseDAO GetExpertiseByID(int id)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    Expertise expertise = (from exp in db.Expertises where exp.Expertise_ID == id select exp).FirstOrDefault();
                    ExpertiseDAO result = new ExpertiseDAO
                    {
                        ID = expertise.Expertise_ID,
                        ExpertiseID = expertise.Expertise_ID,
                        ApplicantID = expertise.Applicant_ID,
                        SkillID = expertise.Skill_ID
                    };
                    return (result != null ? result : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<ExpertiseDAO> GetExpertises()
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    IList<Expertise> exps = db.Expertises.ToList();
                    List<ExpertiseDAO> result = new List<ExpertiseDAO>();
                    foreach (var e in exps)
                    {
                        ExpertiseDAO temp = new ExpertiseDAO
                        {
                            ID = e.Expertise_ID,
                            ExpertiseID = e.Expertise_ID,
                            ApplicantID = e.Applicant_ID,
                            SkillID = e.Skill_ID
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

        public IList<ExpertiseDAO> GetExpertisesByName(string first, string last, string ssn)
        {
            throw new NotImplementedException();
        }        

        public bool CreateExpertise(ExpertiseDAO exp)
        {
            Expertise e = new Expertise
            {
                Applicant_ID = exp.ApplicantID,
                Expertise_ID = exp.ExpertiseID,
                Skill_ID = exp.SkillID
            };

            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                db.Expertises.InsertOnSubmit(e);

                try
                {
                    db.SubmitChanges();
                }
                catch (Exception ex)
                {
                    throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(ex.Message));
                }
            }

            return true;
        }

        public bool UpdateExpertise(ExpertiseDAO newExp)
        {
            throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason("Unsupported Method"));
        }

        public bool DeleteExpertise(int id)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                Expertise e = db.Expertises.Single(app => app.Applicant_ID == id);
                db.Expertises.DeleteOnSubmit(e);

                try
                {
                    db.SubmitChanges();
                }
                catch (Exception ex)
                {
                    throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(ex.Message));
                }
            }

            return true;
        }

        public JobRequirementDAO GetJobRequirementByID(int id)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    JobRequirement jobRequirement = (from jbReqt in db.JobRequirements where jbReqt.JobRequirement_ID == id select jbReqt).FirstOrDefault();
                    JobRequirementDAO result = new JobRequirementDAO
                    {
                        ID = jobRequirement.JobRequirement_ID,
                        JobRequirementID = jobRequirement.JobRequirement_ID,
                        JobOpeningID = jobRequirement.JobOpening_ID,
                        SkillID = jobRequirement.Skill_ID,
                        Notes = jobRequirement.Notes
                    };
                    return (result != null ? result : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<JobRequirementDAO> GetJobRequirements()
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    IList<JobRequirement> jobRequirements = db.JobRequirements.ToList();
                    List<JobRequirementDAO> result = new List<JobRequirementDAO>();

                    foreach (var jobRequirement in jobRequirements)
                    {
                        JobRequirementDAO temp = new JobRequirementDAO
                        {
                            ID = jobRequirement.JobRequirement_ID,
                            JobRequirementID = jobRequirement.JobRequirement_ID,
                            JobOpeningID = jobRequirement.JobOpening_ID,
                            SkillID = jobRequirement.Skill_ID,
                            Notes = jobRequirement.Notes
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

        public bool CreateJobRequirement(JobRequirementDAO jbReqt)
        {
            JobRequirement jobRequirement = new JobRequirement
            {
                JobOpening_ID = jbReqt.JobOpeningID,
                Skill_ID = jbReqt.SkillID,
                Notes = jbReqt.Notes
            };

            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                db.JobRequirements.InsertOnSubmit(jobRequirement);
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

        public bool UpdateJobRequirement(JobRequirementDAO newJb)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                JobRequirement jobRequirement = db.JobRequirements.Single(jbReqt => jbReqt.JobRequirement_ID == newJb.JobRequirementID);
                jobRequirement.JobOpening_ID = newJb.JobOpeningID;
                jobRequirement.Skill_ID = newJb.SkillID;
                jobRequirement.Notes = newJb.Notes;

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

        public bool DeleteJobRequirement(int ID)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                JobRequirement jobRequirement = db.JobRequirements.Single(jbReqt => jbReqt.JobRequirement_ID == ID);
                db.JobRequirements.DeleteOnSubmit(jobRequirement);

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

        public StoreDAO GetStoreByID(int id)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    Store store = (from st in db.Stores where st.Store_ID == id select st).FirstOrDefault();
                    StoreDAO result = new StoreDAO
                    {
                        ID = store.Store_ID,
                        StoreID = store.Store_ID,
                        Location = store.Location,
                        Manager_ID = store.Manager_ID
                    };
                    return (result != null ? result : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<StoreDAO> GetStores()
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    IList<Store> stores = db.Stores.ToList();
                    List<StoreDAO> result = new List<StoreDAO>();

                    foreach (var store in stores)
                    {
                        StoreDAO temp = new StoreDAO
                        {
                            ID = store.Store_ID,
                            StoreID = store.Store_ID,
                            Location = store.Location,
                            Manager_ID = store.Manager_ID
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

        public bool CreateStore(StoreDAO s)
        {
            Store store = new Store
            {
                Store_ID = s.StoreID,
                Location = s.Location,
                Manager_ID = s.Manager_ID
            };

            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                db.Stores.InsertOnSubmit(store);

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

        public bool UpdateStore(StoreDAO newStore)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                Store store = db.Stores.Single(s => s.Store_ID == newStore.StoreID);
                store.Location = newStore.Location;
                store.Manager_ID = newStore.Manager_ID;

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

        public bool DeleteStore(int id)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                Store store = db.Stores.Single(st => st.Store_ID == id);
                db.Stores.DeleteOnSubmit(store);

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

        public AssessmentDAO GetAssessmentByID(int id)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    Assessment assessment = (from asmt in db.Assessments where asmt.Assessment_ID == id select asmt).FirstOrDefault();
                    AssessmentDAO result = new AssessmentDAO
                    {
                        ID = assessment.Assessment_ID,
                        AssessmentID = assessment.Assessment_ID,
                        ApplicantID = assessment.Applicant_ID,
                        QuestionBankID = assessment.QuestionBank_ID
                    };
                    return (result != null ? result : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<AssessmentDAO> GetAssessments()
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    IList<Assessment> assessments = db.Assessments.ToList();
                    List<AssessmentDAO> result = new List<AssessmentDAO>();

                    foreach (var assessment in assessments)
                    {
                        AssessmentDAO temp = new AssessmentDAO
                        {
                            ID = assessment.Assessment_ID,
                            AssessmentID = assessment.Assessment_ID,
                            ApplicantID = assessment.Applicant_ID,
                            QuestionBankID = assessment.QuestionBank_ID
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

        public bool CreateAssessment(AssessmentDAO s)
        {
            Assessment assessment = new Assessment
            {
                Assessment_ID = s.AssessmentID,
                Applicant_ID = s.ApplicantID,
                QuestionBank_ID = s.QuestionBankID
            };

            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                db.Assessments.InsertOnSubmit(assessment);

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

        public bool UpdateAssessment(AssessmentDAO newAssessment)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                Assessment assessment = db.Assessments.Single(s => s.Assessment_ID == newAssessment.AssessmentID);
                assessment.Assessment_ID = newAssessment.AssessmentID;
                assessment.Applicant_ID = newAssessment.ApplicantID;
                assessment.QuestionBank_ID = newAssessment.QuestionBankID;

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

        public bool DeleteAssessment(int id)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                Assessment assessment = db.Assessments.Single(asmt => asmt.Assessment_ID == id);
                db.Assessments.DeleteOnSubmit(assessment);

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

        public QuestionBankDAO GetQuestionBankByID(int id)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    QuestionBank questionBank = (from qubk in db.QuestionBanks where qubk.QuestionBank_ID == id select qubk).FirstOrDefault();
                    QuestionBankDAO result = new QuestionBankDAO
                    {
                        ID = questionBank.QuestionBank_ID,
                        QuestionBankID = questionBank.QuestionBank_ID,
                        MCQuestionID = questionBank.MCQuestion_ID,
                        MCOptionID = questionBank.MCOption_ID,
                        MCCorrectOption = questionBank.MCCorrectOption
                    };
                    return (result != null ? result : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public QuestionBankDAO GetQuestionBankByMCQuestionIDAndMCOptionID(int MCQuestionID, int MCOptionID)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    QuestionBank questionBank = (from qubk in db.QuestionBanks where qubk.MCQuestion_ID == MCQuestionID && qubk.MCOption_ID == MCOptionID select qubk).FirstOrDefault();
                    QuestionBankDAO result = new QuestionBankDAO
                    {
                        ID = questionBank.QuestionBank_ID,
                        QuestionBankID = questionBank.QuestionBank_ID,
                        MCQuestionID = questionBank.MCQuestion_ID,
                        MCOptionID = questionBank.MCOption_ID,
                        MCCorrectOption = questionBank.MCCorrectOption
                    };
                    return (result != null ? result : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<QuestionBankDAO> GetQuestionBanks()
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    IList<QuestionBank> questionBanks = db.QuestionBanks.ToList();
                    List<QuestionBankDAO> result = new List<QuestionBankDAO>();

                    foreach (var questionBank in questionBanks)
                    {
                        QuestionBankDAO temp = new QuestionBankDAO
                        {
                            ID = questionBank.QuestionBank_ID,
                            QuestionBankID = questionBank.QuestionBank_ID,
                            MCQuestionID = questionBank.MCQuestion_ID,
                            MCOptionID = questionBank.MCOption_ID,
                            MCCorrectOption = questionBank.MCCorrectOption
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

        public bool CreateQuestionBank(QuestionBankDAO s)
        {
            QuestionBank questionBank = new QuestionBank
            {
                QuestionBank_ID = s.QuestionBankID,
                MCQuestion_ID = s.MCQuestionID,
                MCOption_ID = s.MCOptionID,
                MCCorrectOption = s.MCCorrectOption
            };

            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                db.QuestionBanks.InsertOnSubmit(questionBank);

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

        public bool UpdateQuestionBank(QuestionBankDAO newQuestionBank)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                QuestionBank questionBank = db.QuestionBanks.Single(s => s.QuestionBank_ID == newQuestionBank.QuestionBankID);
                questionBank.QuestionBank_ID = newQuestionBank.QuestionBankID;
                questionBank.MCQuestion_ID = newQuestionBank.MCQuestionID;
                questionBank.MCOption_ID = newQuestionBank.MCOptionID;
                questionBank.MCCorrectOption = newQuestionBank.MCCorrectOption;

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

        public bool DeleteQuestionBank(int id)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                QuestionBank questionBank = db.QuestionBanks.Single(qubk => qubk.QuestionBank_ID == id);
                db.QuestionBanks.DeleteOnSubmit(questionBank);

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

        public MCQuestionDAO GetMCQuestionByID(int id)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    MCQuestion mCQuestion = (from mcqu in db.MCQuestions where mcqu.MCQuestion_ID == id select mcqu).FirstOrDefault();
                    MCQuestionDAO result = new MCQuestionDAO
                    {
                        ID = mCQuestion.MCQuestion_ID,
                        MCQuestionID = mCQuestion.MCQuestion_ID,
                        MCQuestionDescription = mCQuestion.MCQuestionDescription
                    };
                    return (result != null ? result : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<MCQuestionDAO> GetMCQuestions()
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    IList<MCQuestion> mCQuestions = db.MCQuestions.ToList();
                    List<MCQuestionDAO> result = new List<MCQuestionDAO>();

                    foreach (var mCQuestion in mCQuestions)
                    {
                        MCQuestionDAO temp = new MCQuestionDAO
                        {
                            ID = mCQuestion.MCQuestion_ID,
                            MCQuestionID = mCQuestion.MCQuestion_ID,
                            MCQuestionDescription = mCQuestion.MCQuestionDescription
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

        public bool CreateMCQuestion(MCQuestionDAO s)
        {
            MCQuestion mCQuestion = new MCQuestion
            {
                MCQuestion_ID = s.MCQuestionID,
                MCQuestionDescription = s.MCQuestionDescription
            };

            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                db.MCQuestions.InsertOnSubmit(mCQuestion);

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

        public bool UpdateMCQuestion(MCQuestionDAO newMCQuestion)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                MCQuestion mCQuestion = db.MCQuestions.Single(s => s.MCQuestion_ID == newMCQuestion.MCQuestionID);
                mCQuestion.MCQuestion_ID = newMCQuestion.MCQuestionID;
                mCQuestion.MCQuestionDescription = newMCQuestion.MCQuestionDescription;

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

        public bool DeleteMCQuestion(int id)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                MCQuestion mCQuestion = db.MCQuestions.Single(mcqu => mcqu.MCQuestion_ID == id);
                db.MCQuestions.DeleteOnSubmit(mCQuestion);

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

        public MCOptionDAO GetMCOptionByID(int id)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    MCOption mCOption = (from mcopt in db.MCOptions where mcopt.MCOption_ID == id select mcopt).FirstOrDefault();
                    MCOptionDAO result = new MCOptionDAO
                    {
                        ID = mCOption.MCOption_ID,
                        MCOptionID = mCOption.MCOption_ID,
                        MCOptionDescription = mCOption.MCOptionDescription
                    };
                    return (result != null ? result : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<MCOptionDAO> GetMCOptions()
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    IList<MCOption> mCOptions = db.MCOptions.ToList();
                    List<MCOptionDAO> result = new List<MCOptionDAO>();

                    foreach (var mCOption in mCOptions)
                    {
                        MCOptionDAO temp = new MCOptionDAO
                        {
                            ID = mCOption.MCOption_ID,
                            MCOptionID = mCOption.MCOption_ID,
                            MCOptionDescription = mCOption.MCOptionDescription
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

        public bool CreateMCOption(MCOptionDAO s)
        {
            MCOption mCOption = new MCOption
            {
                MCOption_ID = s.MCOptionID,
                MCOptionDescription = s.MCOptionDescription
            };

            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                db.MCOptions.InsertOnSubmit(mCOption);

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

        public bool UpdateMCOption(MCOptionDAO newMCOption)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                MCOption mCOption = db.MCOptions.Single(s => s.MCOption_ID == newMCOption.MCOptionID);
                mCOption.MCOption_ID = newMCOption.MCOptionID;
                mCOption.MCOptionDescription = newMCOption.MCOptionDescription;

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

        public bool DeleteMCOption(int id)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                MCOption mCOption = db.MCOptions.Single(mcopt => mcopt.MCOption_ID == id);
                db.MCOptions.DeleteOnSubmit(mCOption);

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

        public InterviewDAO GetInterviewByID(int id)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    Interview interview = (from intvw in db.Interviews where intvw.Interview_ID == id select intvw).FirstOrDefault();
                    InterviewDAO result = new InterviewDAO
                    {
                        ID = interview.Interview_ID,
                        InterviewID = interview.Interview_ID,
                        ApplicantID = interview.Applicant_ID,
                        SAQuestionID = interview.SAQuestion_ID,
                        SAResponseID = interview.SAResponse_ID,
                        UserID = interview.UserID
                    };
                    return (result != null ? result : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<InterviewDAO> GetInterviews()
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    IList<Interview> interviews = db.Interviews.ToList();
                    List<InterviewDAO> result = new List<InterviewDAO>();

                    foreach (var interview in interviews)
                    {
                        InterviewDAO temp = new InterviewDAO
                        {
                            ID = interview.Interview_ID,
                            InterviewID = interview.Interview_ID,
                            ApplicantID = interview.Applicant_ID,
                            SAQuestionID = interview.SAQuestion_ID,
                            SAResponseID = interview.SAResponse_ID,
                            UserID = interview.UserID
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

        public bool CreateInterview(InterviewDAO s)
        {
            Interview interview = new Interview
            {
                Interview_ID = s.InterviewID,
                Applicant_ID = s.ApplicantID,
                SAQuestion_ID = s.SAQuestionID,
                SAResponse_ID = s.SAResponseID,
                UserID = s.UserID
            };

            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                db.Interviews.InsertOnSubmit(interview);

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

        public bool UpdateInterview(InterviewDAO newInterview)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                Interview interview = db.Interviews.Single(s => s.Interview_ID == newInterview.InterviewID);
                interview.Interview_ID = newInterview.InterviewID;
                interview.Applicant_ID = newInterview.ApplicantID;
                interview.SAQuestion_ID = newInterview.SAQuestionID;
                interview.SAResponse_ID = newInterview.SAResponseID;
                interview.UserID = newInterview.UserID;

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

        public bool DeleteInterview(int id)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                Interview interview = db.Interviews.Single(intvw => intvw.Interview_ID == id);
                db.Interviews.DeleteOnSubmit(interview);

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

        public SAQuestionDAO GetSAQuestionByID(int id)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    SAQuestion sAQuestion = (from saqu in db.SAQuestions where saqu.SAQuestion_ID == id select saqu).FirstOrDefault();
                    SAQuestionDAO result = new SAQuestionDAO
                    {
                        ID = sAQuestion.SAQuestion_ID,
                        SAQuestionID = sAQuestion.SAQuestion_ID,
                        SAQuestionDescription = sAQuestion.SAQuestionDescription
                    };
                    return (result != null ? result : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<SAQuestionDAO> GetSAQuestions()
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    IList<SAQuestion> sAQuestions = db.SAQuestions.ToList();
                    List<SAQuestionDAO> result = new List<SAQuestionDAO>();

                    foreach (var sAQuestion in sAQuestions)
                    {
                        SAQuestionDAO temp = new SAQuestionDAO
                        {
                            ID = sAQuestion.SAQuestion_ID,
                            SAQuestionID = sAQuestion.SAQuestion_ID,
                            SAQuestionDescription = sAQuestion.SAQuestionDescription
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

        public bool CreateSAQuestion(SAQuestionDAO s)
        {
            SAQuestion sAQuestion = new SAQuestion
            {
                SAQuestion_ID = s.SAQuestionID,
                SAQuestionDescription = s.SAQuestionDescription
            };

            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                db.SAQuestions.InsertOnSubmit(sAQuestion);

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

        public bool UpdateSAQuestion(SAQuestionDAO newSAQuestion)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                SAQuestion sAQuestion = db.SAQuestions.Single(s => s.SAQuestion_ID == newSAQuestion.SAQuestionID);
                sAQuestion.SAQuestion_ID = newSAQuestion.SAQuestionID;
                sAQuestion.SAQuestionDescription = newSAQuestion.SAQuestionDescription;

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

        public bool DeleteSAQuestion(int id)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                SAQuestion sAQuestion = db.SAQuestions.Single(saqu => saqu.SAQuestion_ID == id);
                db.SAQuestions.DeleteOnSubmit(sAQuestion);

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

        public SAResponseDAO GetSAResponseByID(int id)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    SAResponse sAResponse = (from sarpns in db.SAResponses where sarpns.SAResponse_ID == id select sarpns).FirstOrDefault();
                    SAResponseDAO result = new SAResponseDAO
                    {
                        ID = sAResponse.SAResponse_ID,
                        SAResponseID = sAResponse.SAResponse_ID,
                        SAResponseDescription = sAResponse.SAResponseDescription
                    };
                    return (result != null ? result : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<SAResponseDAO> GetSAResponses()
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    IList<SAResponse> sAResponses = db.SAResponses.ToList();
                    List<SAResponseDAO> result = new List<SAResponseDAO>();

                    foreach (var sAResponse in sAResponses)
                    {
                        SAResponseDAO temp = new SAResponseDAO
                        {
                            ID = sAResponse.SAResponse_ID,
                            SAResponseID = sAResponse.SAResponse_ID,
                            SAResponseDescription = sAResponse.SAResponseDescription
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

        public bool CreateSAResponse(SAResponseDAO s)
        {
            SAResponse sAResponse = new SAResponse
            {
                SAResponse_ID = s.SAResponseID,
                SAResponseDescription = s.SAResponseDescription
            };

            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                db.SAResponses.InsertOnSubmit(sAResponse);

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

        public bool UpdateSAResponse(SAResponseDAO newSAResponse)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                SAResponse sAResponse = db.SAResponses.Single(s => s.SAResponse_ID == newSAResponse.SAResponseID);
                sAResponse.SAResponse_ID = newSAResponse.SAResponseID;
                sAResponse.SAResponseDescription = newSAResponse.SAResponseDescription;

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

        public bool DeleteSAResponse(int id)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                SAResponse sAResponse = db.SAResponses.Single(sarpns => sarpns.SAResponse_ID == id);
                db.SAResponses.DeleteOnSubmit(sAResponse);

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

        public SkillQuestionBankDAO GetSkillQuestionBankByID(int id)
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    SkillQuestionBank skillQuestionBank = (from sk in db.SkillQuestionBanks where sk.SkillQuestionBank_ID == id select sk).FirstOrDefault();
                    SkillQuestionBankDAO result = new SkillQuestionBankDAO
                    {
                        ID = skillQuestionBank.SkillQuestionBank_ID,
                        SkillQuestionBankID = skillQuestionBank.SkillQuestionBank_ID,
                        SkillID = skillQuestionBank.Skill_ID,
                        QuestionBankID = skillQuestionBank.QuestionBank_ID
                    };
                    return (result != null ? result : null);
                }
            }
            catch (Exception e)
            {
                throw new FaultException<KaskServiceException>(new KaskServiceException(), new FaultReason(e.Message));
            }
        }

        public IList<SkillQuestionBankDAO> GetSkillQuestionBanks()
        {
            try
            {
                using (AESDatabaseDataContext db = new AESDatabaseDataContext())
                {
                    IList<SkillQuestionBank> skillQuestionBanks = db.SkillQuestionBanks.ToList();
                    List<SkillQuestionBankDAO> result = new List<SkillQuestionBankDAO>();

                    foreach (var skillQuestionBank in skillQuestionBanks)
                    {
                        SkillQuestionBankDAO temp = new SkillQuestionBankDAO
                        {
                            ID = skillQuestionBank.SkillQuestionBank_ID,
                            SkillQuestionBankID = skillQuestionBank.SkillQuestionBank_ID,
                            SkillID = skillQuestionBank.Skill_ID,
                            QuestionBankID = skillQuestionBank.QuestionBank_ID
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

        public bool CreateSkillQuestionBank(SkillQuestionBankDAO s)
        {
            SkillQuestionBank skillQuestionBank = new SkillQuestionBank
            {
                SkillQuestionBank_ID = s.SkillQuestionBankID,
                Skill_ID = s.SkillID,
                QuestionBank_ID = s.QuestionBankID
            };

            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                db.SkillQuestionBanks.InsertOnSubmit(skillQuestionBank);

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

        public bool UpdateSkillQuestionBank(SkillQuestionBankDAO newSkillQuestionBank)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                SkillQuestionBank skillQuestionBank = db.SkillQuestionBanks.Single(s => s.SkillQuestionBank_ID == newSkillQuestionBank.SkillQuestionBankID);
                skillQuestionBank.Skill_ID = newSkillQuestionBank.SkillID;
                skillQuestionBank.QuestionBank_ID = newSkillQuestionBank.QuestionBankID;

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

        public bool DeleteSkillQuestionBank(int id)
        {
            using (AESDatabaseDataContext db = new AESDatabaseDataContext())
            {
                SkillQuestionBank skillQuestionBank = db.SkillQuestionBanks.Single(sk => sk.SkillQuestionBank_ID == id);
                db.SkillQuestionBanks.DeleteOnSubmit(skillQuestionBank);

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
    }
}