using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

// If you use Swagger API for documentation
//using Swashbuckle.AspNetCore;
//using Microsoft.OpenApi.Models;

namespace WebAPI
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
                options.UseMySql(Configuration["ConnectionStrings:DefaultConnection"], b => b.MigrationsAssembly("WebAPI"));

                // Or if use app secret (you can read about it in the docs from Microsoft ) in dev mode, you can use code as below
                /*if (_env.IsDevelopment())
                {
                    // this Configuration["dbpass"] is from the app secret
                    var connnectionStrings = $"{Configuration["ConnectionStrings:DefaultConnection"]};password={Configuration["dbpass"]}";
                    options.UseMySql(connnectionStrings, b => b.MigrationsAssembly("WebAPI"));
                }
                else
                {
                    var connnectionStrings = $"{Configuration["ConnectionStrings:DefaultConnection"]}";
                    options.UseMySql(connnectionStrings, b => b.MigrationsAssembly("WebAPI"));
                }*/
            });

            // You can add service to be available here, so the program could inject the service later (Dependency Injection)
            // for example this one

            // Swagger Service
            /*services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "AgateSwagger",
                    Description = "API Documentation for Agate Project Training",
                    TermsOfService = new Uri("http://github.com/primayoriko/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Prima Yoriko",
                        Email = "prima.yoriko@gmail.com",
                        Url = new Uri("http://primayoriko.github.io"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "AGATE",
                        Url = new Uri("https://agate.id"),
                    }
                });
            });*/

            services.AddControllers().AddNewtonsoftJson(); // this newtonsoftjson are neccessary for PATCH method in controller
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            // You can Add middleware for pipelining of request here (before routing)
            // for example UseDeveloperExceptionPage below

            // Below is a type of conditional pipelining
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
            // for example UseAuthorization, UseSwagger or your own custom middleware as below

            // You can access this TemplateMiddleware class in Controllers/TemplateMiddleware.cs
            //app.UseMiddleware<TemplateMiddleware>();
            
            /* app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AgateSwagger");

            }); */

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
