using Kask.Services.DAO;

using Kask.Services.Exceptions;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface IExpertiseService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        ExpertiseDAO GetExpertiseByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<ExpertiseDAO> GetExpertises();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        [WebGet(UriTemplate = "Expertise?first={first}&last={last}&ssn={ssn}")]
        IList<ExpertiseDAO> GetExpertisesByName(string first, string last, string ssn);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateExpertise(ExpertiseDAO e);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateExpertise(ExpertiseDAO newExp);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteExpertise(int id);
    }
}
