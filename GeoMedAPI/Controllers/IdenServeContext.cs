
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace IdentitySqlServer.SqlServer
{
    public class IdenServeContext : IdentityDbContext
    {
        #region == Constructer ==

        public IdenServeContext(DbContextOptions<IdenServeContext> options)
           : base(options)
        {

        }
        #endregion


    

    }
}
