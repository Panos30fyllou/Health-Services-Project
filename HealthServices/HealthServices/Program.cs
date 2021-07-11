using System;
using HealthServices.ServiceInterface;
using HealthServices.ServiceModel;
using HealthServices.ServiceModel.DataObject;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ServiceStack;

namespace HealthServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            XRayActions xRayActions = new XRayActions();
            XRayRequest xRayRequest = new XRayRequest() { Description = "perigrafiii", Priority = Priority.High, RecommendedDate = DateTime.Now.AddDays(2), DateSent = DateTime.Now, XRayType = XRayType.LowerBody };
            XRayResponse xRayResponse = xRayActions.Post(xRayRequest);
            Console.WriteLine(xRayResponse.Success);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseModularStartup<Startup>();
                });
    }
}
