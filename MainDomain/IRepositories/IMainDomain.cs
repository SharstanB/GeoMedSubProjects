using GM.Base;
using MainDomain.MainDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MainDomain.IRepositories
{
    public interface IMainDomain
    {

        Task<OperationResult<bool>> ActionPatiants(PatientDto patientDto); 
    }
}
