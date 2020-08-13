using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVCApp.Models;

// If use localization-globalization service
/*using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using System.Globalization;*/

// If use authentication (user system like login, etc) or Identity service
//using Microsoft.AspNetCore.Identity;

namespace MVCApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TemplateDbContext>(options =>
            {
                // Add configuration to connect DB here, such as connectionstring as below

                // Basic usage
                // Note: Here we use MySQL database, but you should remember that you can change the code to implement
                //          with other DB such as Microsoft MSSQL or InMemory DB
                options.UseMySql(Configuration["ConnectionStrings:DefaultConnection"], b => b.MigrationsAssembly("MVCApp"));

                // Or if use app secret (you can read about it in the docs from Microsoft ) in dev mode, you can use code as below
                /*if (_env.IsDevelopment())
                {
                    // this Configuration["dbpass"] is from the app secret
                    var connnectionStrings = $"{Configuration["ConnectionStrings:DefaultConnection"]};password={Configuration["dbpass"]}";
                    options.UseMySql(connnectionStrings, b => b.MigrationsAssembly("MVCApp"));
                }
                else
                {
                    var connnectionStrings = $"{Configuration["ConnectionStrings:DefaultConnection"]}";
                    options.UseMySql(connnectionStrings, b => b.MigrationsAssembly("MVCApp"));
                }*/
            });

            services.AddHttpClient();

            // This is Identity service that we added for authentication (user system like login, etc) purpose
            /*services.AddDbContext<TemplateIdentityDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("AuthConnection"),
                x => x.MigrationsAssembly("Agate_View")));

            services.AddIdentity<TemplateIdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<TemplateIdentityDbContext>()
                    .AddDefaultTokenProviders();*/


            // These are lines/commands that we used to configure Identity service
            /*services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });*/

            services.AddControllersWithViews();

            // These are lines/commands that we used to make Localization-Globalization service
            //services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

            // Used to configure Localization-Globalization service
            /*services.Configure<RequestLocalizationOptions>(
                opts =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en-GB"),
                        new CultureInfo("en-US"),
                        new CultureInfo("en"),
                        new CultureInfo("fr-FR"),
                        new CultureInfo("fr"),
                        new CultureInfo("id")
                    };

                    opts.DefaultRequestCulture = new RequestCulture("en-US");
                    // Formatting numbers, dates, etc.
                    opts.SupportedCultures = supportedCultures;
                    // UI strings that we have localized.
                    opts.SupportedUICultures = supportedCultures;
                });*/

            services.AddMvc();
                // Line below are added when we use Localization-Globalization service
                /*.AddViewLocalization(
                    LanguageViewLocationExpanderFormat.Suffix
                 //, opts => { opts.ResourcesPath = "Resources";}
                 )
                .AddDataAnnotationsLocalization();*/

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // You can Add middleware for pipelining of request here (before routing)
            // for example UseDeveloperExceptionPage below

            // Below is a type of conditional pipelining
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // The lines/commands below are added when we use localization-globalization feature/service
            /*var supportedCultures = new[] { "en-US", "fr", "id" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            var cookieProvider = localizationOptions.RequestCultureProviders
                                    .OfType<CookieRequestCultureProvider>()
                                    .First();
            cookieProvider.CookieName = "UserCulture";

            app.UseRequestLocalization(localizationOptions);*/

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRequestLocalization();

            app.UseRouting();

            // You can Add middleware for pipelining of request here (after routing)

            // Line below is added when we use Identity service for authentication
            //app.UseAuthentication();

            app.UseAuthorization();

            // The lines/commands below are added when we use localization-globalization feature/service

            /*var requestProvider = new RouteDataRequestCultureProvider();
            localizationOptions.RequestCultureProviders.Insert(0, requestProvider);

            app.UseRouter(routes =>
            {
                routes.MapMiddlewareRoute("{culture=en-US}/{*mvcRoute}", subApp =>
                {
                    subApp.UseRequestLocalization(localizationOptions);

                    subApp.UseRouting();

                    subApp.UseAuthorization();

                    subApp.UseEndpoints(mvcRoutes =>
                    {
                        // localization-globalization setting using route as parameter
                        mvcRoutes.MapControllerRoute(
                            name: "default",
                            pattern: "{culture=en-US}/{controller=Home}/{action=Index}/{id?}");
                    });
                });
            });*/

            // Or else you add this line (NOT use localization-globalization feature/service) 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
