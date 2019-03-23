using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using PreOrclFrontEnd.Extensions;
using PreOrclFrontEnd.Helpers;
using PreOrclFrontEnd.Models;
using PreOrclFrontEnd.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PreOrclFrontEnd.Controllers 
{

    [Route("[controller]")]
    public class AuthController : Controller
    {
        GenericREST generic;
        private readonly IMemoryCache _memoryCache;
        private MSGraphConfiguration _msGraphConfig;

        public AuthController(IOptions<UriHelpers> configuration, IOptions<MSGraphConfiguration> msGraphConfig,IMemoryCache memoryCache)
        {
            _msGraphConfig = msGraphConfig.Value as MSGraphConfiguration; 
            generic = new GenericREST(configuration.Value);
            _memoryCache = memoryCache;
        }

        public IActionResult Index() {

            return View();
        }
        [Route("Micro")]
        public IActionResult Micro() {

            string replyuri = HttpUtility.UrlEncode(_msGraphConfig.CallbackPath);
            string scope = HttpUtility.UrlEncode(_msGraphConfig.Scope);
            //string scope = HttpUtility.UrlEncode("https://graph.microsoft.com/.default");
            string clientId = _msGraphConfig.ClientId;
            string tenant = _msGraphConfig.Tenant;

          return new RedirectResult($"https://login.microsoftonline.com/{tenant}/oauth2/v2.0/authorize?client_id={clientId}&response_type=code&redirect_uri={replyuri}&response_mode=query&scope={scope}&state=12345");
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

            if (!ExistUsuario(Usuario)) {
                ViewData["msjLogin"] = "Usuario No existe en nuestros registros";
                return View("Index");
            }

            if (entity == null)
            {
                ViewData["msjLogin"] = "Contraseña Erronea";
                return View("Index");
            }
           
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


        [Route("LoginMicrosoft")]
        public async Task<IActionResult> LoginMicrosoftAsync(string code = null, string state = null)
        {
            GraphAuthCustom gp = new GraphAuthCustom(_memoryCache,_msGraphConfig);
            GraphServiceCustom gsc = new GraphServiceCustom();

            string token;
            gp.CreateToken(code);
            var c = _memoryCache.Get<TokenT>("TokenT");
            token = c.access_token;
            var t = gp.GetAuthenticatedClient(token);

            var name = t.Me.Request().GetAsync().Result;
            string urlImg = gsc.GetPictureBase64(t).Result;
            _memoryCache.Set("foto", Encoding.ASCII.GetBytes(urlImg));
            
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, name.GivenName),
                        new Claim(ClaimTypes.Email,name.UserPrincipalName),

                    };
            ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "usuario");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

            await HttpContext.SignInAsync(principal);

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

  

        [NonAction]
        public bool ExistUsuario(string usuario) {

            return generic.GetAll<Usuarios>("Usuarios").Where(c=>c.Usuario.Trim().Equals(usuario.Trim())).Count()>0;

        }
    }
}
