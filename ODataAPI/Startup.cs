using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ODataAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;

namespace ODataAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        // Static method for defining model for OData API, such as Table relation, etc
        // This is needed for OData service, as it wouldn't read relation from OnModelCreating method in DBContext 
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<TemplateClass>("TemplateClasses").EntityType.HasKey(e => e.Id);
            builder.EntitySet<Template2Class>("Template2Classes").EntityType.HasKey(e => e.Grade);
            return builder.GetEdmModel();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Adding service to connect database with EF Core, make sure to migrate and update databse first, read Models/TemplateDbContext carefully
            services.AddDbContext<TemplateDbContext>(options =>
            {
                // Add configuration to connect DB here, such as connectionstring as below

                // Basic usage
                // Note: Here we use MySQL database, but you should remember that you can change the code to implement
                //          with other DB such as Microsoft MSSQL or InMemory DB
                options.UseMySql(Configuration["ConnectionStrings:DefaultConnection"], b => b.MigrationsAssembly("Agate_API"));

                // Or if use app secret (you can read about it in the docs from Microsoft ) in dev mode, you can use code as below
                /*if (_env.IsDevelopment())
                {
                    // this Configuration["dbpass"] is from the app secret
                    var connnectionStrings = $"{Configuration["ConnectionStrings:DefaultConnection"]};password={Configuration["dbpass"]}";
                    options.UseMySql(connnectionStrings, b => b.MigrationsAssembly("Agate_API"));
                }
                else
                {
                    var connnectionStrings = $"{Configuration["ConnectionStrings:DefaultConnection"]}";
                    options.UseMySql(connnectionStrings, b => b.MigrationsAssembly("Agate_API"));
                }*/
            });

            // You can add service to be available here, so the program could inject the service later (Dependency Injection)
            // Below are the examples

            services.AddControllers(options =>
            {
                // To Use Odata service, routing is highly recommended to use UseMvc (not UseEndpoint) as in v2.x, because all of the tutorial I read use this configuration
                // So this config is neccessary 
                options.EnableEndpointRouting = false;
            })
                    .AddNewtonsoftJson();

            services.AddOData(); // this newtonsoftjson are neccessary for PATCH method in controller
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            // You can Add middleware for pipelining of request here (before routing)
            // for example UseDeveloperExceptionPage below

            // A type of conditional pipelining
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            // You can Add middleware for pipelining of request here (after routing)

            app.UseAuthorization();

            // As said before, routing in OData Service using UseMvc, not UseEndpoint
            app.UseMvc(routeBuilder =>
            {
                // This is setting that needed for setup an OData Service controller
                routeBuilder.Select().Expand().Count().Filter().OrderBy().SkipToken().Build();
                routeBuilder.MapODataServiceRoute("odata", "odata", GetEdmModel());
            });
        }
    }
}
