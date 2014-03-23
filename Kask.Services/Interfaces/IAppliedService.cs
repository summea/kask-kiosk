<<<<<<< HEAD
﻿using Kask.DAL2.Models;
=======
﻿using Kask.Services.DAO;

>>>>>>> origin/views
using Kask.Services.Exceptions;
using System.Collections.Generic;
using System.ServiceModel;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface IAppliedService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
<<<<<<< HEAD
        Applied GetAppliedByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<Applied> GetApplieds();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateApplied(Applied a);

=======
        AppliedDAO GetAppliedByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<AppliedDAO> GetApplieds();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateApplied(AppliedDAO a);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateApplied(AppliedDAO newApp);

>>>>>>> origin/views
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteApplied(int id);
    }
}
