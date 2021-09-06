using GeoMed.SqlServer;
using GM.Base;
using MainDomain.IRepositories;
using System;
using System.Threading.Tasks;

namespace MainDomain.Repositories
{
    public class MainServices : BaseRepository , IMainDomain 
    {
        public MainServices(GMApiContext context):base(context)
        {

        }
        public async Task<OperationResult<bool>> ActionPatiants()
        {
            var result = new OperationResult<bool>();

            Context.Users.Add(new GeoMed.Model.Account.GMUser()
            {
                //BirthDate =
            });
            return result;
        }
    }
}
