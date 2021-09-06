using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IdentityServer.IData;
using IdentityServer.Data;
using AutoMapper;
using GMIdentityServer.Util;
using GeoMed.SqlServer;
using GeoMed.Model.Account;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace GMIdentityServer
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();

            //services.AddDbContext<GMApiContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("GMConnectionString"))
            //);


            //services.AddIdentity<GMUser, GMRole>(identity => {
            //    identity.Password.RequiredLength = 6;
            //    identity.Password.RequireNonAlphanumeric = false;
            //    identity.Password.RequireLowercase = false;
            //    identity.Password.RequireUppercase = false;
            //    identity.Password.RequireDigit = false;
            //    identity.Password.RequiredUniqueChars = 0;
            //    identity.Lockout.AllowedForNewUsers = false;
            //    identity.User.AllowedUserNameCharacters =
            //   "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            //    identity.User.RequireUniqueEmail = false;
            //})
            //    .AddEntityFrameworkStores<GMApiContext>()
            //    .AddDefaultTokenProviders();

            services.AddDbContext<GMApiContext>(options =>

           options.UseSqlServer(Configuration.GetConnectionString("GMConnectionString"))

         );


            services.AddIdentity<GMUser, GMRole>(identity =>
            {
                identity.Password.RequiredLength = 6;
                identity.Password.RequireNonAlphanumeric = false;
                identity.Password.RequireLowercase = false;
                identity.Password.RequireUppercase = false;
                identity.Password.RequireDigit = false;
                identity.Password.RequiredUniqueChars = 0;
                identity.Lockout.AllowedForNewUsers = false;
                identity.User.AllowedUserNameCharacters =
               "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                identity.User.RequireUniqueEmail = false;
            }).AddEntityFrameworkStores<GMApiContext>()
                .AddDefaultTokenProviders();
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(o =>
            //{
            //    o.Authority = "https://localhost:44351/";
            //    o.Audience = "apiResourse";
            //    o.RequireHttpsMetadata = false;
            //});





            services.AddIdentityServer(
                option =>
                {
                    option.UserInteraction.LoginUrl = "/Account/Login";
                    option.UserInteraction.ErrorUrl = "/Home/Index";
                    option.UserInteraction.LogoutUrl = "/Account/SignOut";
                }
                )
                  .AddAspNetIdentity<GMUser>()
                  .AddInMemoryClients(Config.GetClients())
                 .AddInMemoryApiResources(Config.GetApiResources())
                 .AddInMemoryApiScopes(Config.GetApiScopes())
                 .AddInMemoryIdentityResources(Config.GetIdentityResources())
                 .AddDeveloperSigningCredential();

            //services.AddAuthentication()
            // .AddFacebook(config => {
            //     config.AppId = "3396617443742614";
            //     config.AppSecret = "secret";
            // });
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IAccountRepository, AccountRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthentication();

            //app.UseAuthorization();

            app.UseIdentityServer();  //UseIdentityServer allows IdentityServer to start handling routing for OAuth and OpenID Connect endpoints
            //, such as the authorization and token endpoints.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
