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
    public class AESApplicationService : IApplicationService, IApplicantService, IAppliedService, IEducationService, IEmployerService, IEmploymentService, IJobService, IJobOpeningService, ISchoolService
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
                        JobID = applied.Job_ID,
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
                    IList<Applied> apps = db.Applieds.ToList();
                    List<AppliedDAO> result = new List<AppliedDAO>();
                    foreach (var a in apps)
                    {
                        AppliedDAO temp = new AppliedDAO
                        {
                            ID = a.Applied_ID,
                            AppliedID = a.Applied_ID,
                            ApplicantID = a.Applicant_ID,
                            ApplicationID = a.Application_ID,
                            JobID = a.Job_ID,
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

        public bool CreateApplied(AppliedDAO app)
        {
            Applied a = new Applied
            {
                Applied_ID = app.AppliedID,
                Applicant_ID = app.ApplicantID,
                Application_ID = app.ApplicationID,
                Job_ID = app.JobID,
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
                        JobOpeningID = jobOpening.JobOpening_ID,
                        OpenDate = jobOpening.OpenDate,
                        JobID = jobOpening.Job_ID,
                        Approved = (byte)jobOpening.Approved

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
                    IList<Store> stores = (from store in db.Stores where store.Store_ID == storeID select store).ToList();
                    List<JobOpeningDAO> result = new List<JobOpeningDAO>();

                    foreach (var str in stores)
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
                            JobOpeningID = jobOpening.JobOpening_ID,
                            OpenDate = jobOpening.OpenDate,
                            JobID = jobOpening.Job_ID,
                            Approved = (byte)jobOpening.Approved
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
                Approved = (byte)jbOp.Approved
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
    }
}