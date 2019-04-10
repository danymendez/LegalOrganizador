using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PreOrclFrontEnd.Helpers;
using PreOrclFrontEnd.Models;
using PreOrclFrontEnd.Utilidades;
using PreOrclFrontEnd.ViewModels;

namespace PreOrclFrontEnd.Controllers
{
    public class RolesController : Controller
    {

        private GenericREST generic;

        public RolesController(IOptions<UriHelpers> configuration)
        {

            generic = new GenericREST(configuration.Value);

        }

        public async Task<IActionResult> Index()
        {
            List<Roles> listaRoles = await generic.GetAll<Roles>("Roles");
            ViewBag.PersonasClassCssNav = "active";
            return View(listaRoles);
        }

        public IActionResult CreatePartial()
        {
            VwModelRolesPermisos vwModel = new VwModelRolesPermisos();
            vwModel.VwModelPermisos = generic.GetAll<VwModelPermisos>("Permisos").Result;
            return PartialView("../Roles/_CreatePartial", vwModel);
        }
        public async Task<IActionResult> Create(VwModelRolesPermisos vwModelRolesPermisos)
        {
            if (ModelState.IsValid) {
                Roles roles = new Roles { IdRol = 0, NombreRol = vwModelRolesPermisos.NombreRol, CreatedAt = DateTime.Now, Inactivo = 0 };
                roles = await generic.Post("Roles", roles);
                foreach (var item in vwModelRolesPermisos.VwModelPermisos) {

                    if (item.Seleccionado) {
                        RolesPermisos rolesPermisos = await generic.Post("RolesPermisos",
                                                                            new RolesPermisos {
                                                                                IdRolPermiso = 0,
                                                                                IdRol = roles.IdRol,
                                                                                IdPermiso = item.IdPermiso,
                                                                                CreatedAt = DateTime.Now,
                                                                                Inactivo = 0
                                                                            });
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return PartialView("../Roles/_CreatePartial", vwModelRolesPermisos);
        }

    }
}