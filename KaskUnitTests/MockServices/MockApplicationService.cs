using System;
using System.Collections.Generic;
using Kask.Services.DAO;
using Kask.Services.Interfaces;

namespace Kask.UnitTests
{
    class MockApplicationService: IApplicationService
    {
        List<ApplicationDAO> Applications = new List<ApplicationDAO>();
        List<ApplicantDAO> Applicants = new List<ApplicantDAO>();
       
        public ApplicationDAO GetApplicationByID(int id)
        {
            foreach (var application in Applications)
                if (application.ApplicationID == id)
                    return application;
            throw new Exception("Applicant not found");
        }

        public IList<ApplicationDAO> GetApplicationsByName(string first, string last, string ssn)
        {
            foreach (var applicant in Applicants)
                if (applicant.FirstName == first || applicant.LastName == last || applicant.SSN == ssn)
                {
                    Applications.Add(GetApplicationByID(applicant.ID));
                    return Applications;
                }
            throw new Exception("Applicant not found");
        }

        public IList<ApplicationDAO> GetApplications()
        {
            return Applications;
        }

        public bool CreateApplication(ApplicationDAO app)
        {
            Applications.Add(app);
            return true;
        }

        public bool UpdateApplication(ApplicationDAO newApp)
        {
            foreach (var application in Applications)
                if (application.ApplicationID == newApp.ApplicationID)
                {
                    Applications.Remove(application);
                    Applications.Add(newApp);
                    return true;
                }
            return false;
        }

        public bool DeleteApplication(int ID)
        {
            foreach (var application in Applications)
                if (application.ApplicationID == ID)
                {
                    Applications.Remove(application);
                    return true;
                }
            return false;
        }
    }
}
