using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kask.Services.DAO;
using Kask.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KaskUnitTests.MockServices;

namespace KaskUnitTests.ServiceUnitTests
{
    [TestClass]
    public class SkillServiceUnitTest
    {
        public MockSkillService skillService = new MockSkillService();
        List<SkillDAO> Skills = new List<SkillDAO>();

        [TestInitialize]
        public void Initialize()
        {
            skillService.SetUp();
        }

        [TestMethod]
        [Description("Happy test")]
        public void HappyTest_GetSkillByID()
        {
            Assert.IsNotNull(skillService.GetSkillByID(1));
            Assert.AreEqual(skillService.GetSkillByID(1).SkillName, "skillname1");
        }

        [TestMethod]
        [Description("Fail GetSkillByID")]
        [ExpectedException(typeof(Exception))]
        public void Test_FailSkillByID()
        {
            Assert.IsNull(skillService.GetSkillByID(5));
        }

        [TestMethod]
        public void Test_GetSkills()
        {
            List<SkillDAO> list = skillService.GetSkills() as List<SkillDAO>;
            Assert.IsNotNull(list);
            Assert.AreEqual(list.Count, 3);
        }

        [TestMethod]
        public void Test_CreateSkill()
        {
            SkillDAO skill4 = new SkillDAO() { ID = 4, SkillID = 4, SkillName = "skillname4" };
            skillService.CreateSkill(skill4);
            Assert.IsNotNull(skillService.GetSkillByID(3));
            Assert.AreEqual(skillService.GetSkills().Count, 4);
        }

        [TestMethod]
        public void Test_UpdateSKill()
        {
            SkillDAO skill4 = new SkillDAO() { ID = 3, SkillID = 3, SkillName = "testskillname" };
            skillService.UpdateSkill(skill4);
            Assert.AreEqual(skillService.GetSkillByID(3).SkillName, "testskillname");
            Assert.AreEqual(skillService.GetSkills().Count, 3);
        }

        [TestMethod]
        public void Test_DeleteSkill()
        {
            skillService.DeleteSkill(3);
            Assert.AreEqual(skillService.GetSkills().Count, 2);
        }
    }
}
