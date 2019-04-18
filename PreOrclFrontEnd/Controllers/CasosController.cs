using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using PreOrclFrontEnd.Helpers;
using PreOrclFrontEnd.Models;
using PreOrclFrontEnd.Utilidades;

namespace PreOrclFrontEnd.Controllers
{
    public class CasosController : Controller
    {

        GenericREST generic;

        public CasosController(IOptions<UriHelpers> configuration)
        {

            generic = new GenericREST(configuration.Value);

        }
        public IActionResult Index()
        {
            return View();
        }

     
        public IActionResult Create()
        {
            
            ViewBag.listaPersonas = new SelectList(generic.GetAll<SisPerPersona>("SisPerPersonas").Result, "per_IDPER", "per_nombre_razon");
            ViewBag.listaCategorias = ListaGenericaCollection.GetListCategorias();
            ViewBag.listaTipos = ListaGenericaCollection.GetListTipo();
            ViewBag.listaAbogados = new SelectList(generic.GetAll<Usuarios>("Usuarios").Result, "IdUsuario", "Nombre");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Casos casos)
        {
            if (ModelState.IsValid)
            {

                casos = await generic.Post("Casos", casos);

                if (casos.IdCaso == 0)
                {
                    return BadRequest();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(casos);
        }


    }
}