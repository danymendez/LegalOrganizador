using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PreOrclFrontEnd.Helpers;
using PreOrclFrontEnd.Models;
using PreOrclFrontEnd.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace PreOrclFrontEnd.Controllers 
{

    [Route("[controller]")]
    public class AuthController : Controller
    {
        GenericREST generic;


        public AuthController(IOptions<ConfigurationJson> configuration)
        {
            generic = new GenericREST(configuration.Value);
        }

        public IActionResult Index() {

            return View();
        }

        [Route("Microsoft")]
        public IActionResult Microsoft()
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
            string hashPassword = Criptografia.Encrypt(Password);
            var entity = generic.PostAuth<Usuarios>("Usuarios/Autenticar", Usuario, HttpUtility.UrlEncode(hashPassword));

            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, entity.Nombre),
                        new Claim(ClaimTypes.Email,entity.Usuario),

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
            //this.SignOut();
            return RedirectToAction("Index", "Auth");
        }
    }
}
