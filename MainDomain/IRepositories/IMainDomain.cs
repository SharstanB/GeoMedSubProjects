using GM.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MainDomain.IRepositories
{
    public interface IMainDomain
    {

        Task<OperationResult<bool>> ActionPatiants(); 
    }
}
