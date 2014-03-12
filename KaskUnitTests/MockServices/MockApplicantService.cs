using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kask.DAL.Models;
using Kask.Services.Interfaces;


namespace KaskUnitTests
{
    internal sealed class MockApplicantService : IApplicantService
    {
        List<Applicant> Applications = new List<Applicant>();

        public Applicant GetApplicantByID(int id)
        {
            foreach (var applicant in Applications)
                if (applicant.Applicant_ID == id)
                    return applicant;

            throw new Exception("Application not found");
        }


        public IList<Applicant> GetApplicants()
        {
            throw new NotImplementedException();
        }

        public bool CreateApplicant(Applicant a)
        {
            throw new NotImplementedException();
        }

        public bool UpdateApplicant(Applicant newApp)
        {
            throw new NotImplementedException();
        }

        public bool DeleteApplicant(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
