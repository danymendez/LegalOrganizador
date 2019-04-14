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

        public AuthController(IOptions<UriHelpers> configuration, IOptions<MSGraphConfiguration> msGraphConfig, IMemoryCache memoryCache)
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
            Usuarios usuarios = new Usuarios();
            graphAuthCustom.CreateToken(code);
            var tokenT = _memoryCache.Get<TokenT>("TokenT");

            var authenticatedClient = graphAuthCustom.GetAuthenticatedClient(tokenT.access_token);

            var me = authenticatedClient.Me.Request().GetAsync().Result;
            string urlImg = gsc.GetPictureBase64(authenticatedClient).Result;
            _memoryCache.Set("foto", Encoding.ASCII.GetBytes(urlImg));

            decimal idRol = 0;

            if (!ExistUsuario(me.UserPrincipalName))
            {
                usuarios.Usuario = me.UserPrincipalName;
                usuarios.Apellido = me.Surname;
                usuarios.Nombre = me.GivenName;
                usuarios.Password = "";
                usuarios.Token = Criptografia.Encrypt(tokenT.access_token);
                usuarios.TokenRefresh = Criptografia.Encrypt(tokenT.refresh_token);
                usuarios.CreatedAt = DateTime.Now;
                usuarios.Inactivo = 0;
                usuarios.IdRol = 1;
                idRol = 1;
                await generic.Post("Usuarios", usuarios);
            }
            else {
                var _usuarios = (await generic.GetAll<Usuarios>("Usuarios")).Find(l => l.Usuario.Trim().ToUpper() == me.UserPrincipalName.Trim().ToUpper());
                _usuarios.Usuario = me.UserPrincipalName;
                _usuarios.Apellido = me.Surname;
                _usuarios.Nombre = me.GivenName;
                _usuarios.Password = "";
                _usuarios.Token = Criptografia.Encrypt(tokenT.access_token);
                _usuarios.TokenRefresh = Criptografia.Encrypt(tokenT.refresh_token);
                _usuarios.CreatedAt = DateTime.Now;
                _usuarios.Inactivo = 0;
               
                idRol = _usuarios.IdRol;
                await generic.Put("Usuarios/",_usuarios.IdUsuario,usuarios);
            }

            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, me.GivenName),
                        new Claim(ClaimTypes.Email,me.UserPrincipalName),
                     //   new Claim(ClaimTypes.Role, "Seguridad")

                    };

            var permisos = from rolPermiso in await generic.GetAll<RolesPermisos>("RolesPermisos")
                           join permiso in await generic.GetAll<Permisos>("Permisos") on rolPermiso.IdPermiso equals permiso.IdPermiso
                           where rolPermiso.IdRol == idRol
                           select permiso;

            foreach (var item in permisos) {
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

            //this.SignOut();
            return RedirectToAction("Index", "Auth");
        }

  

        [NonAction]
        public bool ExistUsuario(string usuario) {

            return generic.GetAll<Usuarios>("Usuarios").Result.Where(c=>c.Usuario.Trim().ToUpper().Equals(usuario.Trim().ToUpper())).Count()>0;

        }
    }
}
