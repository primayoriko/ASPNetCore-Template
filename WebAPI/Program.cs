using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebAPI.Models;
using Swashbuckle.AspNetCore;
using Microsoft.OpenApi.Models;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            //below command used to add Seed data from SeedData class in models
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    TemplateSeedData.Initialize(services);
                }
                catch (Exception e)
                {
                    // write your exception action here, for example
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }

            // command to bootstrap/start our app
            host.Run();
        }

        // A function to customize host building, use this method as template (you can explore docs for further setting)
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
