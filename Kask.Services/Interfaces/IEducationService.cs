using Kask.Services.Exceptions;
using Kask.Services.DAO;

using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface IEducationService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        EducationDAO GetEducationByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<EducationDAO> GetEducations();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        [WebGet(UriTemplate = "?first={first}&last={last}&ssn={ssn}")]
        IList<EducationDAO> GetEducationsByName(string first, string last, string ssn);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateEducation(EducationDAO e);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateEducation(EducationDAO newEmp);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteEducation(int id);
    }
}