using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using MVCApp.Models;

// If want to add to add localization and globalization add the dependencies below
//using Microsoft.Extensions.Localization;
//using Microsoft.AspNetCore.Localization;

namespace MVCApp.Controllers
{
    // This is one of default controller in ASP NET Core Project
    // But I have added some modifications here, such as globalization-localization
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // If want to add to add localization and globalization, add this field
        //private readonly IStringLocalizer<HomeController> _localizer;

        // And add IStringLocalizer<{this controller class}> in the parameter to inject it into the class's field,
        // As an example see the constructor below
        /*public HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> localizer)
        {
            _localizer = localizer;
            _logger = logger;
        }*/

        // Instead of this one
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // These are default controller methods 
        public IActionResult Index()
        {
            // This is example command/code when we using lozalization-globalization
            //ViewData["Title"] = _localizer["Home"]; // _localizer["Home"] is fetched from Resources directory by default
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // The following methods are optional and only used when we use lozalization and globalization
        /*public IActionResult SetCulture(string id = "en")
        {
            string culture = id;
            Response.Cookies.Append(
               CookieRequestCultureProvider.DefaultCookieName,
               CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
               new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
           );

            ViewData["Message"] = "Culture set to " + culture;

            return View("About");
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }*/
    }
}
