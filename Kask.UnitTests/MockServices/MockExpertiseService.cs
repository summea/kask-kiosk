using Kask.Services.DAO;
using Kask.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace KaskUnitTests.MockServices
{
    public class MockExpertiseService : IExpertiseService
    {
        List<ExpertiseDAO> Expertises = new List<ExpertiseDAO>();

        internal void SetUp()
        {
            ExpertiseDAO expertise1 = new ExpertiseDAO() { ID = 1, ExpertiseID = 1, ApplicantID = 1, SkillID = 1 };
            ExpertiseDAO expertise2 = new ExpertiseDAO() { ID = 2, ExpertiseID = 2, ApplicantID = 2, SkillID = 2 };
            ExpertiseDAO expertise3 = new ExpertiseDAO() { ID = 3, ExpertiseID = 3, ApplicantID = 3, SkillID = 3 };

            Expertises.Add(expertise1);
            Expertises.Add(expertise2);
            Expertises.Add(expertise3);
        }

        public ExpertiseDAO GetExpertiseByID(int id)
        {
            foreach (var expertise in Expertises)
                if (expertise.ExpertiseID == id)
                    return expertise;
            throw new Exception("No Expertise Found");
        }

        public IList<ExpertiseDAO> GetExpertises()
        {
            return Expertises;
        }

        public IList<ExpertiseDAO> GetExpertisesByName(string first, string last, string ssn)
        {
            throw new NotImplementedException();
        }

        public bool CreateExpertise(ExpertiseDAO e)
        {
            Expertises.Add(e);
            return true;
        }

        public bool UpdateExpertise(ExpertiseDAO newExp)
        {
            foreach(var expertise in Expertises)
                if(expertise.ExpertiseID == newExp.ExpertiseID)
                {
                    Expertises.Remove(expertise);
                    Expertises.Add(newExp);
                    return true;
                }
            return false;
        }

        public bool DeleteExpertise(int id)
        {
            foreach(var expertise in Expertises)
                if(expertise.ExpertiseID == id)
                {
                    Expertises.Remove(expertise);
                    return true;
                }
            return false;
        }

        private void Initialize( ExpertiseDAO expertise, int id, int expertiseID, int applicantID, int skillID )
        {
            expertise.ID = id;
            expertise.ExpertiseID = expertiseID;
            expertise.ApplicantID = applicantID;
            expertise.SkillID = skillID;
        }
    }
}
