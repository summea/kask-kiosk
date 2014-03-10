using Kask.DAL.Models;
using Kask.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface IEducationService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        Education GetEducationtByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<Education> GetEducationsByName(string first, string last, string SSN);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateApplicant(Applicant a);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateApplicant(Applicant newApp);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteApplicant(int ID);

    }
}
