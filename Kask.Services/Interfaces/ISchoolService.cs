using Kask.Services.Exceptions;
using Kask.Services.DAO;

using Kask.Services.Exceptions;
using System.Collections.Generic;
using System.ServiceModel;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface ISchoolService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        SchoolDAO GetSchoolByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<SchoolDAO> GetSchools();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateSchool(SchoolDAO e);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateSchool(SchoolDAO newEmp);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteSchool(int id);
    }
}