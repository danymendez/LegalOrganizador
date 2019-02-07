﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PreOrclFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.Controllers 
{

    [Route("[controller]")]
    public class AuthController : Controller
    {
        GenericREST generic;


        public AuthController()
        {
            generic = new GenericREST();
        }

        public IActionResult Index() {

            return View();
        }

        [Route("Microsoft")]
        public IActionResult Google()
        {
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("LoginMicrosoft", "Auth")
            };

            return Challenge(authenticationProperties, "Microsoft");
        }


        [HttpPost("{Usuario,Password}")]
        [ValidateAntiForgeryToken]
        [Route("Login")]
        public async Task<IActionResult> Login(string Usuario, string Password)
        {

            if (String.IsNullOrEmpty(Usuario) || String.IsNullOrEmpty(Password)) {
                return BadRequest();
            }


            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, Usuario)
                    };
            ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "usuario");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

            await HttpContext.SignInAsync(principal);

            return RedirectToAction("Index", "Home");
        }

        //  private readonly SignInManager<IdentityUser> _signInManager;

        [Route("LoginMicrosoft")]
        public IActionResult LoginMicrosoft(string returnUrl = null, string remoteError = null)
        {


            var a = this.User.Claims;

            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, "")
                    };
            ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "usuario");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Auth");
        }
    }
}