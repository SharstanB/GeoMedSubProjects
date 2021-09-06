using GeoMed.Model.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.SqlServer.SeedData
{
    public static class SeedAccount
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var userManager = (UserManager<GMUser>)serviceProvider.GetService(typeof(UserManager<GMUser>));
            var context = (GMApiContext)serviceProvider.GetService(typeof(GMApiContext));

            var admin = await userManager.FindByNameAsync("GeoMed");

            if(admin == null)
            {
               admin = new GMUser()
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
