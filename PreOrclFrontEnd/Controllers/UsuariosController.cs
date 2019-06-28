using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using PreOrclFrontEnd.Helpers;
using PreOrclFrontEnd.Models;
using PreOrclFrontEnd.Utilidades;
using PreOrclFrontEnd.ViewModels;

namespace PreOrclFrontEnd.Controllers
{
    [Authorize(Roles = "Seguridad")]

    [Route("[controller]")]
    public class UsuariosController : Controller
    {
        GenericREST generic;
        private decimal idUsuario=0;
        private readonly CacheItems cacheItems;
        public UsuariosController(IOptions<UriHelpers> configuration, IMemoryCache memoryCache)
        {

            generic = new GenericREST(configuration.Value);
        cacheItems = new CacheItems(memoryCache);
    }
        public async Task<IActionResult> Index()
        {
            List<VwModelUsuarios> listaVwModelUsuario = new List<VwModelUsuarios>();
          

            List<Usuarios> listaUsuarios = await generic.GetAll<Usuarios>("Usuarios");
            List<Roles> listaRoles = await generic.GetAll<Roles>("Roles");
            listaVwModelUsuario = (from user in listaUsuarios
                                  join rol in listaRoles on user.IdRol equals rol.IdRol
                                  select new VwModelUsuarios { Usuarios = user, Roles = rol }).ToList();

            ViewBag.PersonasClassCssNav = "active";
            return View(listaVwModelUsuario);
        }
        [HttpGet("{id}")]
        [Route("InactivateOrActivatePartial", Name ="InactivateOrActivatePartial")]
        public async Task<IActionResult> InactivateOrActivatePartial(decimal? id)
        {
            Usuarios usuarios = new Usuarios();
            
           
            usuarios = await generic.Get<Usuarios>("Usuarios/", id);

            ViewBag.Title = "Inactivar Usuario";
            ViewBag.ButtonTextSave = "Inactivar";
            if (usuarios.Inactivo == 1)
            {
                ViewBag.ButtonTextSave = "Reactivar";
                ViewBag.Title = "Reactivar Rol";
            }

            return PartialView("../Usuarios/_InactivateOrActivatePartial", usuarios);
        }
        [HttpPost("InactivateOrActivate")]
        public async Task<IActionResult> InactivateOrActivate(Usuarios usuarios)
        {
            if (usuarios.Inactivo == 1)
            {
                var usuarioToInactivated = await generic.Get<Usuarios>("Usuarios/", usuarios.IdUsuario);
                usuarioToInactivated.Inactivo = 1;
                usuarioToInactivated.InactivatedAt = DateTime.Now;
                usuarioToInactivated.UpdatedAt = DateTime.Now;

                bool Actualizado = await generic.Put("Usuarios/", usuarioToInactivated.IdUsuario, usuarioToInactivated);

                if (Actualizado)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Ha ocurrido un error al inactivar");
                }

            }
            else
            {
                var usuarioToInactivated = await generic.Get<Usuarios>("Usuarios/", usuarios.IdUsuario);
                usuarioToInactivated.Inactivo = 0;
                usuarioToInactivated.InactivatedAt = null;
                usuarioToInactivated.UpdatedAt = DateTime.Now;

                bool Actualizado = await generic.Put("Usuarios/", usuarioToInactivated.IdUsuario, usuarioToInactivated);

                if (Actualizado)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Ha ocurrido un error al inactivar");
                }
            }
        }
        [HttpGet("{id}")]
        [Route("CambiarRol", Name ="CambiarRol")]
        public async Task<IActionResult> CambiarRol(decimal id)
        {

            Usuarios usuarios = new Usuarios();


            usuarios = await generic.Get<Usuarios>("Usuarios/", id);

            ViewBag.Roles = new SelectList(await generic.GetAll<Roles>("Roles"),"IdRol","NombreRol");
            return PartialView("../Usuarios/_CambiarRol", usuarios);
        }

        [HttpPost("CambiarRol")]
   
        public async Task<IActionResult> CambiarRol(Usuarios usuarios)
        {
            
                var usuarioToEdit = await generic.Get<Usuarios>("Usuarios/", usuarios.IdUsuario);
                
                usuarioToEdit.InactivatedAt = DateTime.Now;
                usuarioToEdit.UpdatedAt = DateTime.Now;
                usuarioToEdit.IdRol = usuarios.IdRol;
                bool Actualizado = await generic.Put("Usuarios/", usuarioToEdit.IdUsuario, usuarioToEdit);

                if (Actualizado)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Ha ocurrido un error al cambiar rol");
                }

        }
            
          
        

        [Route("MiPerfil")]
        public async Task<IActionResult> MiPerfil() {
            VwModelUsuarios vwModelUsuarios = new VwModelUsuarios();

            if (User != null)
            {
                if (User.Identity.IsAuthenticated)
                    idUsuario = Convert.ToDecimal(User.Identities
                                    .Where(c => c.IsAuthenticated).FirstOrDefault()
                                    .Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            }
                vwModelUsuarios.Usuarios = await generic.Get<Usuarios>("Usuarios/", idUsuario);
            vwModelUsuarios.Roles = (await generic.Get<Roles>("Roles/", vwModelUsuarios.Usuarios.IdRol));
            return View(vwModelUsuarios);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            ViewData["img"] = cacheItems.GetImageBase64FromCache(User);
        }
    }
}