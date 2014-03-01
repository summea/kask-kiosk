using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock;
using Kask.Services.Interfaces;
using System.Collections.Generic;
using Kask.DAL2.Models;


namespace TestProject
{
    [TestClass]
    public class IApplicationServiceTest
    {
       // private MockFactory sut_Farcory = new MockFactory();
        
        //private MockFactory mock = new MockFactory();
        //private Mock<IApplicationService> sut;
       

        //[TestInitialize]
        //public void Initialize()
        //{
        //    sut = mock.CreateMock<IApplicationService>();            
        //}

        //[TestMethod]
        //public void GetApplicationsIsNotNull()
        //{
        //    Assert.IsNotNull(sut.MockObject.GetApplications());
        //}

        //[TestMethod]
        //public void GetApplicationByIdValid()
        //{ 
            
        //}

 //*************************************************************************************
      
      /// <summary>
        /// using the real object instead of mocking it
      /// </summary>
      /// 
        private Services.ApplicationServiceClient sut = new Services.ApplicationServiceClient();
      
        [TestMethod]
        public void GetApplicationIsNotNull()
        {
            Assert.IsNotNull(sut.GetApplications());
        }

        [TestMethod]
        [ExpectedException(typeof(Kask.Services.Exceptions.KaskServiceExceptions)," KaskServiceException is expected")]
        public void GetApplicationByIdException()
        {
            sut.GetApplicationById(0);
        }

        [TestMethod]
        public void GetApplicationByIdValid()
        {
            sut.GetApplicationById(1);
        }
    }
}
