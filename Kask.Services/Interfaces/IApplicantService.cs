using Kask.DAL2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Kask.Services.Interfaces
{
    [ServiceContract]
    public interface IApplicantService
    {
        [OperationContract]
        Applicant GetApplicantByID(int id);

        [OperationContract]
        Applicant GetApplicantByAppliedID(int id);

        [OperationContract]
        Applicant GetApplicants();

    }
}
