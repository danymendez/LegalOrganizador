using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PreOrclFrontEnd.Helpers;
using PreOrclFrontEnd.Models;
using PreOrclFrontEnd.Utilidades;

namespace PreOrclFrontEnd.Controllers
{
    public class UsuariosController : Controller
    {
        GenericREST generic;

        public UsuariosController(IOptions<UriHelpers> configuration)
        {

            generic = new GenericREST(configuration.Value);

        }
        public async Task<IActionResult> Index()
        {
            List<Usuarios> listaUsuarios = await generic.GetAll<Usuarios>("Usuarios");

            ViewBag.PersonasClassCssNav = "active";
            return View(listaUsuarios);
        }
    }
}