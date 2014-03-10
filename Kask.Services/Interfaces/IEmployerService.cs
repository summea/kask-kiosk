using Kask.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Kask.DAL.Models;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface IEmployerService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        Employer GetEmployerByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<Employer> GetEmployers();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateEmployer(Employer e);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateEmployer(Employer newEmp);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteEmployer(int id);
    }
}
