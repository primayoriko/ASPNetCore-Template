using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Models;

namespace MVCApp.Controllers
{
    // This controller template class is optional, and only used when you use Identity service to make authentication in ASP NET Core
    // You can remove it if not needed
    // Used as controlling flow of your authentication service
    public class TemplateAuthenticationController : Controller
    {
        // This template is used for basic functionality in authentication service
        // You can extend/costumize as your will, this is the example

        private UserManager<TemplateIdentityUser> _userManager;
        private SignInManager<TemplateIdentityUser> _signInManager;

        public TemplateAuthenticationController(UserManager<TemplateIdentityUser> userManager, SignInManager<TemplateIdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm]TemplateRegisterViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var std = new TemplateIdentityUser { 
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                UserName = user.Username,
            };

            var res = await _userManager.CreateAsync(std, user.Password);

            if (res.Succeeded)
            {
                return Redirect("/");

            } else
            {
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] TemplateLoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach(var i in errors)
                {
                    ModelState.AddModelError("", i.ErrorMessage);
                }
                return View();
            }

            var res = await _signInManager.PasswordSignInAsync(user.Username, user.Password, user.Remember, false);
            if (!res.Succeeded)
            {
                ModelState.AddModelError("", "Invalid login attempt");
                return View(user);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
