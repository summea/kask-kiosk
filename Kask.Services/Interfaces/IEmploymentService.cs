using Kask.Services.Exceptions;
using Kask.Services.DAO;

using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface IEmploymentService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        EmploymentDAO GetEmploymentByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<EmploymentDAO> GetEmployments();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        [WebGet(UriTemplate = "?first={first}&last={last}&ssn={ssn}")]
        IList<EmploymentDAO> GetEmploymentsByName(string first, string last, string ssn);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateEmployment(EmploymentDAO e);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateEmployment(EmploymentDAO newEmp);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteEmployment(int id);
    }
}