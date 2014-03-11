using Kask.Services.Exceptions;
using Kask.Services.DAO;

using Kask.Services.Exceptions;
using System.Collections.Generic;
using System.ServiceModel;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface IEmployerService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        EmployerDAO GetEmployerByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<EmployerDAO> GetEmployers();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateEmployer(EmployerDAO e);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateEmployer(EmployerDAO newEmp);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteEmployer(int id);
    }
}