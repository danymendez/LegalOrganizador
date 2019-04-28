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
            var listVwModelActividades = generic.GetAll<VwModelActividadesAsistentes>("ActividadesAsistentes/GetAllVwModelActividadesAsistentes").Result;
            return View(listVwModelActividades);
        }

        public IActionResult Create()
        {
            ViewBag.listaAbogados = listaSistema.GetSelectListAbogados();
            ViewBag.listaEstadoActividad = ListaGenericaCollection.GetSelectListItemEstadoActividad();
            ViewBag.listaCasos = listaSistema.GetSelectListCasos();
            return View();
        }

        public async Task<IActionResult> Edit(decimal id)
        {
            VwModelActividadesAsistentes vwModelActividadesAsistentes = new VwModelActividadesAsistentes();
            vwModelActividadesAsistentes = await generic.Get<VwModelActividadesAsistentes>("ActividadesAsistentes/GetVwModelActividadesAsistentes/", id);
            ViewBag.listaAbogados = listaSistema.GetSelectListAbogados();
            var calendarios = from calendario in generic.GetAll<GraphCalendar>("Calendarios/" + vwModelActividadesAsistentes.Responsable.IdUsuario).Result select calendario.Calendar;
            ViewBag.listaCalendario = new SelectList(calendarios.ToList(), "Id", "Name");
            ViewBag.listaEstadoActividad = ListaGenericaCollection.GetSelectListItemEstadoActividad();
            ViewBag.listaCasos = listaSistema.GetSelectListCasos();
            return View(vwModelActividadesAsistentes);
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
        public async Task<IActionResult> Create(VwModelActividadesAsistentes vwModelActividadesAsistentes)
        {
            if (ModelState.IsValid)
            {
               
                vwModelActividadesAsistentes.Actividades.CreatedAt = DateTime.Now;
                vwModelActividadesAsistentes.Actividades.Inactivo = 0;
                vwModelActividadesAsistentes.Actividades.TimeZone = "UTC";
                vwModelActividadesAsistentes.ListVwModelAsistentes = new List<VwModelAsistentes>();
                var listaAbogados = await generic.GetAll<Usuarios>("Usuarios");
                foreach (var itemIdAsistente in vwModelActividadesAsistentes.IdAsistentes) {
                    string emailToSave = listaAbogados.Find(c => c.IdUsuario==itemIdAsistente).Usuario;
                    vwModelActividadesAsistentes.ListVwModelAsistentes.Add(new VwModelAsistentes { IdAsistente = itemIdAsistente, Correo = emailToSave });
                }
                
               bool isSaved = await generic.PostIsSaved("ActividadesAsistentes/PostVwActividadesAsistentes", vwModelActividadesAsistentes);

                if (!isSaved)
                {
                    return BadRequest();
                }
                return RedirectToAction(nameof(Index));
                // return View(vwModelActividadesAsistentes);
            }
            return View(vwModelActividadesAsistentes);
        }
    }
}