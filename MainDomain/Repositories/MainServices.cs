using GeoMed.SqlServer;
using GM.Base;
using MainDomain.IRepositories;
using MainDomain.MainDTO;
using System;
using System.Threading.Tasks;

namespace MainDomain.Repositories
{
    public class MainServices : BaseRepository , IMainDomain 
    {
        public MainServices(GMApiContext context):base(context)
        {

        }
        public async Task<OperationResult<bool>> ActionPatiants(PatientDto patientDto)
        {
            var result = new OperationResult<bool>();

            Context.Users.Add(new GeoMed.Model.Account.GMUser()
            {
                BirthDate = patientDto.BirthDay,
                LastName = patientDto.LastName,
                FirstName = patientDto.FirsthName,
                Email = "sd@gmail.com",
                Gender = 1,
                PhoneNumber = "3204348349",
            });

            Context.SaveChanges();
            return result;
        }
    }
}
