using Kask.Services.DAO;
using Kask.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace KaskUnitTests.MockServices
{
    public class MockSkillService : ISkillService
    {
        List<SkillDAO> Skills = new List<SkillDAO>();

        internal void SetUp()
        {
            SkillDAO skill1 = new SkillDAO() { ID = 1, SkillID = 1, SkillName = "skillname1" };
            SkillDAO skill2 = new SkillDAO() { ID = 2, SkillID = 2, SkillName = "skillname2" };
            SkillDAO skill3 = new SkillDAO() { ID = 3, SkillID = 3, SkillName = "skillname3" };

            Skills.Add(skill1);
            Skills.Add(skill2);
            Skills.Add(skill3);
        }
        public SkillDAO GetSkillByID(int id)
        {
            foreach (var skill in Skills)
                if (skill.SkillID == id)
                    return skill;
            throw new Exception("Skill not found");
        }

        public IList<SkillDAO> GetSkills()
        {
            return Skills;
        }

        public bool CreateSkill(SkillDAO e)
        {
            Skills.Add(e);
            return true;
        }

        public bool UpdateSkill(SkillDAO newEmp)
        {
            foreach(var skill in Skills)
                if(skill.SkillID == newEmp.SkillID)
                {
                    Skills.Remove(skill);
                    Skills.Add(newEmp);
                    return true;
                }
            return false;
        }

        public bool DeleteSkill(int id)
        {
            foreach(var skill in Skills)
            {
                if(skill.SkillID == id)
                {
                    Skills.Remove(skill);
                    return true;
                }
            }
            return false;
        }

        private void Initialize( SkillDAO skill, int id, int skillID, string skillName )
        {
            skill.ID = id;
            skill.SkillID = skillID;
            skill.SkillName = skillName;
        }
    }
}
