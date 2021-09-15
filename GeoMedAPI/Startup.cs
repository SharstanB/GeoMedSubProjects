using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using GeoMed.SqlServer;
using GM.QueueService.QueueDTO;
//using GM.QueueService.IRepositories;
//using GM.QueueService.QueueDTO;
using MainDomain.IRepositories;
using MainDomain.Repositories;
//using GM.QueueService.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using QueueService;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace GeoMedAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
           // QueueService = queueService;
        }

        public IConfiguration Configuration { get; }
       // public IMessageQueueService<QueueMessage> QueueService { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = "https://localhost:44351/";
                o.Audience = "apiResourse";
                o.RequireHttpsMetadata = false;
            });



            services.AddSingleton<IBus>(RabbitHutch.CreateBus("host=localhost;virtualHost=/;username=guest;password=guest;timeout=120"));
            services.AddSingleton<MessageDispatcher>();

            //services.AddSingleton<AutoSubscriber>(provider =>
            //{
            //    var subscriber = new AutoSubscriber(provider.GetRequiredService<IBus>(), "example")
            //    {
            //        AutoSubscriberMessageDispatcher = provider.GetRequiredService<MessageDispatcher>();}
            //});
            services.AddSingleton<AutoSubscriber>(provider =>
            {
                return new AutoSubscriber(provider.GetRequiredService<IBus>(), "example")
                {
                    AutoSubscriberMessageDispatcher = provider.GetRequiredService<MessageDispatcher>()
                };
            });

            services.AddScoped<IMainDomain, MainServices>();

            services.AddScoped<MessageQueueService>();

            //var bus = RabbitHutch.CreateBus("host=localhost");
            //services.AddSingleton(bus);

            services.AddDbContext<GMApiContext>(options =>

            options.UseSqlServer(Configuration.GetConnectionString("GMConnectionString")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GeoMedAPI", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GeoMedAPI v1"));
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseSubscribe("ClientMessageService",  AppDomain.CurrentDomain.GetAssemblies()
            //    .Where(x => x.FullName.Contains("QueueService")).FirstOrDefault());

            app.ApplicationServices.GetRequiredService<AutoSubscriber>().SubscribeAsync(new Assembly[] {
            AppDomain.CurrentDomain.GetAssemblies()
               .Where(x => x.FullName.Contains("QueueService")).FirstOrDefault()});


            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
