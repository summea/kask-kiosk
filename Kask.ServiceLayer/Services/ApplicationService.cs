﻿using Kask.ServiceLayer.Services.Interfaces;
using Kask.DAL.Models;
using System;

namespace Kask.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ApplicationService : IApplicationService
    {
        /* ================ HTTP GET /Application/id ================ */
        public Applications GetApplication(int id)
        {
            throw new NotImplementedException();
        }

        /* ================ HTTP Get /Application/ ================ */
        public Applications GetApplications()
        {
            throw new NotImplementedException();
        }

        /* ================ HTTP Post /Application/ ================ */
        public Applications CreateApplication(Applications app)
        {
            throw new NotImplementedException();
        }

        /* ================ HTTP Put /Application/id/ ================ */
        public bool UpdateApplication(int ID)
        {
            throw new NotImplementedException();
        }

        /* ================ HTTP Delete /Application/id/ ================ */
        public bool DeleteApplication(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
