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
            vwModel.Permisos = generic.GetAll<Permisos>("Permisos").Result;
            return PartialView("../Roles/_CreatePartial", vwModel);
        }
    }
}