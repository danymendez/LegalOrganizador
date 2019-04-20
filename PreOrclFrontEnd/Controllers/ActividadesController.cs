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
using PreOrclFrontEnd.ViewModels;

namespace PreOrclFrontEnd.Controllers
{
    public class ActividadesController : Controller
    {

        GenericREST generic;
        ListaSistema listaSistema;

        public ActividadesController(IOptions<UriHelpers> configuration)
        {

            generic = new GenericREST(configuration.Value);
            listaSistema = new ListaSistema(configuration);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.listaAbogados = listaSistema.GetSelectListAbogados();
            return View();
        }

        public JsonResult GetCalendariosByIdUsuario(decimal id)
        {
            var lista = generic.GetAll<GraphCalendar>("Calendarios/" + id).Result;
            return Json(lista);
        }

        public JsonResult GetAsistenteWithOutAbogado(decimal id)
        {
            var lista = listaSistema.GetSelectListAbogados().Where(c => c.Value != id.ToString());
            return Json(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VwModelActividadesAsistentes casos)
        {
            if (ModelState.IsValid) {
                //{
                //    casos.CreatedAt = DateTime.Now;
                //    casos.Cancelado = 0;
                //    casos.Inactivo = 0;
                //    casos = await generic.Post("Casos", casos);

                //    if (casos.IdCaso == 0)
                //    {
                //        return BadRequest();
                //    }
                //    return RedirectToAction(nameof(Index));
                return View(casos);
            }
            return View(casos);
        }
    }
}