using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Funq;
using ServiceStack;
using ServiceStack.Configuration;
using HealthServices.ServiceInterface;
using HealthServices.ServiceModel;
using System;
using HealthServices.ServiceModel.DataObject;

namespace HealthServices
{
    public class Startup : ModularStartup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public new void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseServiceStack(new AppHost
            {
                AppSettings = new NetCoreAppSettings(Configuration)
            });
        }
    }

    public class AppHost : AppHostBase
    {
        public AppHost() : base("HealthServices", typeof(MyServices).Assembly) { }

        // Configure your AppHost with the necessary configuration and dependencies your App needs
        public override void Configure(Container container)
        {

            SetConfig(new HostConfig
            {
                DebugMode = AppSettings.Get(nameof(HostConfig.DebugMode), false)
            });
            DatabaseController.Initialize(AppSettings.Get<String>("ConnectionStrings:conn_string"));
            
            //string myDb1ConnectionString = Startup._configuration.GetConnectionString("myDb1");
            //XRayActions xRayActions = new XRayActions();
            //XRayRequest xRayRequest = new XRayRequest() { Description = "perigrafiii", Priority = Priority.High, RecommendedDate = DateTime.Now.AddDays(2), DateSent = DateTime.Now, XRayType = XRayType.LowerBody };
            //XRayResponse xRayResponse = xRayActions.Post(xRayRequest);
            //Console.WriteLine(xRayResponse.Success);

        }
    }
}
