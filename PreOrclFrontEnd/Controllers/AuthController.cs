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
using System.Globalization;
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

        public AuthController(IOptions<UriHelpers> configuration, IOptions<MSGraphConfiguration> msGraphConfig, IMemoryCache memoryCache)
        {
            _msGraphConfig = msGraphConfig.Value as MSGraphConfiguration;
            generic = new GenericREST(configuration.Value);
            _memoryCache = memoryCache;
            
        }

        public IActionResult Index() {

            return View();
        }

        [Route("UsuarioInactivo")]
        public IActionResult UsuarioInactivo()
        {

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
            var entity = await generic.PostAuth<Usuarios>("Usuarios/Autenticar", Usuario, HttpUtility.UrlEncode(hashPassword));

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
            GraphAuthCustom graphAuthCustom = new GraphAuthCustom(_memoryCache, _msGraphConfig);
            GraphServiceCustom gsc = new GraphServiceCustom();
            Usuarios usuarioToCreateOrUpdate = new Usuarios();
            graphAuthCustom.CreateToken(code);
            var tokenT = _memoryCache.Get<TokenT>("TokenT");

            var authenticatedClient = graphAuthCustom.GetAuthenticatedClient(tokenT.access_token);

            var me = await authenticatedClient.Me.Request().GetAsync();
            string urlImg = gsc.GetPictureBase64(authenticatedClient).Result;
            if(urlImg!=null)
            _memoryCache.Set("foto", Encoding.ASCII.GetBytes(urlImg));

            

            
                usuarioToCreateOrUpdate.Usuario = me.UserPrincipalName;
                usuarioToCreateOrUpdate.Apellido = me.Surname;
                usuarioToCreateOrUpdate.Nombre = me.GivenName;
                usuarioToCreateOrUpdate.Password = "";
                usuarioToCreateOrUpdate.Token = Criptografia.Encrypt(tokenT.access_token);
                usuarioToCreateOrUpdate.TokenRefresh = Criptografia.Encrypt(tokenT.refresh_token);
                usuarioToCreateOrUpdate.CreatedAt = DateTime.Now;
                usuarioToCreateOrUpdate.Inactivo = 0;
              
    
            usuarioToCreateOrUpdate=await generic.Post("Usuarios/AutenticarInterno", usuarioToCreateOrUpdate);
            if (usuarioToCreateOrUpdate == null)
                return BadRequest("Hubo un error en el login");
            if (usuarioToCreateOrUpdate.Inactivo == 1)
                return RedirectToAction(nameof(UsuarioInactivo));
            decimal idRol = usuarioToCreateOrUpdate.IdRol;


            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, me.GivenName),
                        new Claim(ClaimTypes.Email,me.UserPrincipalName),
                         new Claim(ClaimTypes.NameIdentifier,usuarioToCreateOrUpdate.IdUsuario.ToString())
                    };

            var roles = (await generic.GetAll<RolesPermisos>("RolesPermisos")).Where(c => c.IdRol == idRol);

            var rolsistema = (await generic.GetAll<Roles>("Roles")).Where(c => c.IdRol == idRol);

            var permisos = from rolPermiso in roles
                           join permiso in await generic.GetAll<Permisos>("Permisos") on rolPermiso.IdPermiso equals permiso.IdPermiso
                         
                           select permiso;

            foreach (var item in rolsistema) {
                claims.Add(new Claim(ClaimTypes.Role, item.NombreRol));
            }

            foreach (var item in permisos)
            {
                claims.Add(new Claim(ClaimTypes.Role, item.NombrePermiso));
            }


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
            var callbackUrl = Url.Action(nameof(SignedOut), "Account", values: null, protocol: Request.Scheme);
            return SignOut(
                new AuthenticationProperties { RedirectUri = callbackUrl },
                CookieAuthenticationDefaults.AuthenticationScheme,
                OpenIdConnectDefaults.AuthenticationScheme);
      
        }

        [HttpGet]
        [Route("SignedOut")]
        public IActionResult SignedOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Redirect to home page if the user is authenticated.
                return RedirectToAction("Index", "Auth");
            }

            return RedirectToAction("Index", "Auth");
        }

        [NonAction]
        public bool ExistUsuario(string usuario) {

            var usuarios = generic.GetAll<Usuarios>("Usuarios").Result.Find(c => c.Usuario.Trim().ToLowerInvariant() == usuario.Trim().ToLowerInvariant());
            if (usuarios == null)
                return false;
            else
                return true;

        }
    }
}
