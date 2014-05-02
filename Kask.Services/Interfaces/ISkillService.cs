using Kask.Services.DAO;
using Kask.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface ISkillService
    {
        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        SkillDAO GetSkillByID(int id);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        IList<SkillDAO> GetSkills();

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool CreateSkill(SkillDAO e);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool UpdateSkill(SkillDAO newEmp);

        [OperationContract]
        [FaultContract(typeof(KaskServiceException))]
        bool DeleteSkill(int id);
    }
}
