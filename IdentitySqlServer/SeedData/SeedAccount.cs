using IdentityServerModels;
using IdentitySqlServer.SqlServer;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySqlServer.SeedData
{
    public static class SeedAccount
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var userManager = (UserManager<IdenServUser>)serviceProvider.GetService(typeof(UserManager<IdenServUser>));
            var context = (IdenServeContext)serviceProvider.GetService(typeof(IdenServeContext));
            await context.Database.EnsureCreatedAsync();
            var admin = await userManager.FindByNameAsync("GeoMed");

            if(admin == null)
            {
               admin = new IdenServUser()
            {
                UserName = "GeoMed",
                Email = "geomed@gmail.com",
                FirstName = "Geo" ,
                LastName = "Med"
            };

             await userManager.CreateAsync(admin, "123456");

            await context.SaveChangesAsync(); 
            }
           
           
        }
    }
}
