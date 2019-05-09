using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using PreOrclFrontEnd.Helpers;
using PreOrclFrontEnd.Models;
using PreOrclFrontEnd.Utilidades;
using PreOrclFrontEnd.ViewModels;

namespace PreOrclFrontEnd.Controllers
{
    [Authorize(Roles = "Seguridad")]
    public class UsuariosController : Controller
    {
        GenericREST generic;
        private readonly CacheItems cacheItems;
        public UsuariosController(IOptions<UriHelpers> configuration, IMemoryCache memoryCache)
        {

            generic = new GenericREST(configuration.Value);
        cacheItems = new CacheItems(memoryCache);
    }
        public async Task<IActionResult> Index()
        {
            List<Usuarios> listaUsuarios = await generic.GetAll<Usuarios>("Usuarios");

            ViewBag.PersonasClassCssNav = "active";
            return View(listaUsuarios);
        }

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

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            ViewData["img"] = cacheItems.GetImageBase64FromCache(User);
        }
    }
}