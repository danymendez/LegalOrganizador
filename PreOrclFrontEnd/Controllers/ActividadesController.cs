using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
    [Authorize(Roles = "Actividades")]

    public class ActividadesController : Controller
    {

        GenericREST generic;
        ListaSistema listaSistema;
        private readonly CacheItems cacheItems;
        public ActividadesController(IOptions<UriHelpers> configuration, IMemoryCache memoryCache)
        {

            generic = new GenericREST(configuration.Value);
            listaSistema = new ListaSistema(configuration);
            cacheItems = new CacheItems(memoryCache);
        }
        public IActionResult Index()
        {
            var listVwModelActividades = generic.GetAll<VwModelActividadesAsistentes>("ActividadesAsistentes/GetAllVwModelActividadesAsistentes").Result;
            return View(listVwModelActividades);
        }

        public IActionResult Create()
        {
            ViewBag.listaAbogados = listaSistema.GetSelectListAbogados();

            List<SelectListItem> listItemEstado = new List<SelectListItem>();

            foreach (var items in ListaGenericaCollection.GetSelectListItemEstadoActividad()) {

                listItemEstado.Add(new SelectListItem { Text = items.Text, Value = items.Value, Selected = (items.Text == "Programada") });


            }

            ViewBag.listaEstadoActividad = listItemEstado;
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
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VwModelActividadesAsistentes vwModelActividadesAsistentes)
        {
            Regex regex = new Regex(@"^[0-9]{1,3}(,[0-9]{3}){0,2}(\.[0-9]{2})$");
            if(regex.IsMatch(ModelState["Actividades.Costo"].AttemptedValue))
            ModelState["Actividades.Costo"].ValidationState= Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
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
            
          
            return BadRequest(vwModelActividadesAsistentes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VwModelActividadesAsistentes vwModelActividadesAsistentes)
        {
            Regex regex = new Regex(@"^[0-9]{1,3}(,[0-9]{3}){0,2}(\.[0-9]{2})$");
            if (regex.IsMatch(ModelState["Actividades.Costo"].AttemptedValue))
                ModelState["Actividades.Costo"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            if (ModelState.IsValid)
            {

                vwModelActividadesAsistentes.Actividades.UpdatedAt = DateTime.Now;
                vwModelActividadesAsistentes.Actividades.Inactivo = 0;
                vwModelActividadesAsistentes.Actividades.TimeZone = "UTC";
                vwModelActividadesAsistentes.ListVwModelAsistentes = new List<VwModelAsistentes>();
                var listaAbogados = await generic.GetAll<Usuarios>("Usuarios");
                foreach (var itemIdAsistente in vwModelActividadesAsistentes.IdAsistentes)
                {
                    string emailToSave = listaAbogados.Find(c => c.IdUsuario == itemIdAsistente).Usuario;
                    vwModelActividadesAsistentes.ListVwModelAsistentes.Add(new VwModelAsistentes { IdAsistente = itemIdAsistente, Correo = emailToSave });
                }

                bool isSaved = await generic.PutIsSaved("ActividadesAsistentes/PutVwActividadesAsistentes", vwModelActividadesAsistentes);

                if (!isSaved)
                {
                    return BadRequest();
                }
                return RedirectToAction(nameof(Index));
                // return View(vwModelActividadesAsistentes);
            }
            return View(vwModelActividadesAsistentes);
        }


        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            ViewData["img"] = cacheItems.GetImageBase64FromCache(User);
        }
    }
}