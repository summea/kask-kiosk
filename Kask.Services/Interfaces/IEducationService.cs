using Kask.Services.Exceptions;
using Kask.Services.DAO;

using System.Collections.Generic;
using System.ServiceModel;

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
        bool CreateEducation(EducationDAO e);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateEducation(EducationDAO newEmp);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteEducation(int id);
    }
}