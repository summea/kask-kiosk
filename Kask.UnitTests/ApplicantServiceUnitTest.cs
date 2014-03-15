using Kask.Services.DAO;
using Kask.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Kask.UnitTests
{
    [TestClass]
    public class ApplicantServiceUnitTest
    {
        public IApplicantService applicantService;
       
        // private ApplicantDAO applicant;
        public IList<Kask.Services.DAO.ApplicantDAO> listOfApplicants;
        ApplicantDAO applicant1 = new ApplicantDAO() { ApplicantID = 1, FirstName = "Sam1", LastName = "Edy1", Phone = "000 000 0000", SSN = "1111" };
        ApplicantDAO applicant2 = new ApplicantDAO() { ApplicantID = 2, FirstName = "Sam2", LastName = "Edy2", Phone = "222 222 2222", SSN = "2222" };
        ApplicantDAO applicant3 = new ApplicantDAO() { ApplicantID = 3, FirstName = "Sam3", LastName = "Edy3", Phone = "333 333 3333", SSN = "3333" };
        //public MockApplicantService mockApplicantsObj = new MockApplicantService();
        

        [TestInitialize]
        public void Initialize()
        {
            applicantService = new MockApplicantService();
            
            //applicant = new ApplicantDAO();                      
        }


        [TestMethod]

        public void Test_GetApplicantByID()
        {
            applicantService.CreateApplicant(applicant1);   //(new Kask.Services.DAO.ApplicantDAO() { ApplicantID = 1, FirstName = "Sam1", LastName = "Edy1", Phone = "000 000 0000", SSN = "1111" });
            applicantService.CreateApplicant(applicant2);   //(new Kask.Services.DAO.ApplicantDAO() { ApplicantID = 2, FirstName = "Sam2", LastName = "Edy2", Phone = "222 222 2222", SSN = "2222" });
            Assert.AreEqual(applicantService.GetApplicantByID(1).ApplicantID, 1);
            Assert.AreEqual(applicantService.GetApplicantByID(2).ApplicantID, 2);
            
        }

        [TestMethod]
        public void Test_GetApplicants()
        {

            IList<ApplicantDAO> list = new List<ApplicantDAO>();
            IList<ApplicantDAO> list2 = new List<ApplicantDAO>();

            list.Add(applicant1);
            list.Add(applicant2);
            list.Add(applicant3);

            applicantService.CreateApplicant(applicant1);
            applicantService.CreateApplicant(applicant2);
            applicantService.CreateApplicant(applicant3);

            list2 = applicantService.GetApplicants();

            bool same = true;
            for(int i = 0; i < list.Count; ++i)
            {
                if (list[i] != list2[i])
                {
                    same = false;
                }
      
            }
          
            Assert.IsNotNull(list);
            Assert.AreEqual(same, true);

            Assert.AreEqual(list.Count, 3);   
        }


        [TestMethod]
        public void Test_UpdateApplicant()
        {
            ApplicantDAO applicant4 = new ApplicantDAO() { ApplicantID = 4, FirstName = "Sam4", LastName = "Edy4", Phone = "444 444 4444", SSN = "4444" };

            applicantService.UpdateApplicant(applicant4);
            Assert.AreEqual(applicant4.ApplicantID, 4);
            Assert.AreEqual(applicant4.FirstName, "Sam4");
            Assert.AreEqual(applicant4.SSN, "4444");
            Assert.AreNotEqual(applicant4.Phone, applicant3.Phone);
            
        }

        [TestMethod]
        public void Test_DeleteApplicant()
        {
            applicantService.CreateApplicant(applicant1);
            applicantService.CreateApplicant(applicant2);
            applicantService.CreateApplicant(applicant3);

            applicantService.DeleteApplicant(2);
             IList<ApplicantDAO> list = new List<ApplicantDAO>();
            list = applicantService.GetApplicants();

            Assert.AreEqual(list.Count, 2);
            Assert.IsNotNull(applicantService.GetApplicantByID(1), "Applicant 1 exist");
            Assert.IsNotNull(applicantService.GetApplicantByID(3), "Applicant 3 exist");


        }
    }
}
