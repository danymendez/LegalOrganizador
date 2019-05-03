using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PreOrclFrontEnd.Helpers;
using PreOrclFrontEnd.Models;
using PreOrclFrontEnd.Utilidades;
using PreOrclFrontEnd.ViewModels;

namespace PreOrclFrontEnd.Controllers
{
    public class CasosController : Controller
    {

        GenericREST generic;
        ListaSistema listaSistema;

        public CasosController(IOptions<UriHelpers> configuration)
        {

            generic = new GenericREST(configuration.Value);
            listaSistema = new ListaSistema(configuration);
        }
        public IActionResult Index()
        {
            var listViewModelCasos = generic.GetAll<VwModelCasos>("Casos/GetAllVwModelCasos").Result;
            ViewBag.listaEstadoCasos = ListaGenericaCollection.GetSelectListItemEstadoCaso();
            return View(listViewModelCasos);
        }

     
        public IActionResult Create()
        {

            ViewBag.listaPersonas = listaSistema.GetSelectListClientes();
            ViewBag.listaImputados = listaSistema.GetSelectListClientes();
            ViewBag.listaCategorias = ListaGenericaCollection.GetSelectListItemCategorias();
            ViewBag.listaTipos = ListaGenericaCollection.GetSelectListItemTipo();
            ViewBag.listaAbogados = listaSistema.GetSelectListAbogados();
            ViewBag.listaEstadoCasos = ListaGenericaCollection.GetSelectListItemEstadoCaso();

            return View();
        }

        public SelectList GetListaPersonas() {
            var lista = new SelectList(generic.GetAll<SisPerPersona>("SisPerPersonas").Result, "per_IDPER", "per_nombre_razon");
            return lista;
        }

        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.listaPersonas = listaSistema.GetSelectListClientes();
            ViewBag.listaImputados = listaSistema.GetSelectListClientes();
            ViewBag.listaCategorias = ListaGenericaCollection.GetSelectListItemCategorias();
            ViewBag.listaTipos = ListaGenericaCollection.GetSelectListItemTipo();
            ViewBag.listaAbogados = listaSistema.GetSelectListAbogados();
            ViewBag.listaEstadoCasos = ListaGenericaCollection.GetSelectListItemEstadoCaso();
            var casos = await generic.Get<VwModelCasos>("Casos/GetVwModelCasos/", id);
            if (casos == null)
            {
                return NotFound();
            }
            return View(casos);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, VwModelCasos vwModelCasos)
        {
            if (id != vwModelCasos.Casos.IdCaso)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool isSaved = await generic.Put("Casos/", id, vwModelCasos.Casos);
                    if (!isSaved)
                    {
                        return BadRequest();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    return BadRequest();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vwModelCasos.Casos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VwModelCasos vwModelCasos)
        {
            if (ModelState.IsValid)
            {
                vwModelCasos.Casos.CreatedAt = DateTime.Now;
                vwModelCasos.Casos.Cancelado = 0;
                vwModelCasos.Casos.Inactivo = 0;
                vwModelCasos = await generic.Post("Casos/PostVwModelCasos", vwModelCasos);

                if (vwModelCasos.Casos.IdCaso == 0)
                {
                    return BadRequest();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vwModelCasos);
        }


    }
}