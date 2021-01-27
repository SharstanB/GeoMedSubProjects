using IdentityServerModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentitySqlServer.SqlServer
{
    public class IdenServeContext : IdentityDbContext<IdenServUser, IdenServRole, int>
    {
        #region == Constructer ==

        public IdenServeContext(DbContextOptions<IdenServeContext> options)
           : base(options)
        {

        }
        #endregion


    

    }
}
