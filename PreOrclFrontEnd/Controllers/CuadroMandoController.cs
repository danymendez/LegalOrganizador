using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PreOrclFrontEnd.Helpers;
using PreOrclFrontEnd.Utilidades;
using PreOrclFrontEnd.ViewModels;

namespace PreOrclFrontEnd.Controllers
{
    public class CuadroMandoController : Controller
    {
        GenericREST generic;
        ListaSistema listaSistema;

        public CuadroMandoController(IOptions<UriHelpers> configuration)
        {

            generic = new GenericREST(configuration.Value);
            listaSistema = new ListaSistema(configuration);
        }

        [Route("ProcesoPorEstado")]
        public async Task<IActionResult> ProcesoPorEstado()
        {
            var lista = await generic.GetAll<VwModelProcesosPorEstado>("CuadroMando/ProcesoPorEstado");

            decimal total = lista.Select(c => c.IdCaso).Count();
            decimal totalAbiertoPor = ((lista.Where(c => c.EstadoCaso == "Abierto").Count())/total)*100;
            decimal totalCerrado = ((lista.Where(c => c.EstadoCaso != "Abierto").Count()) / total) * 100;
            ViewBag.porCerrado = totalCerrado;
            ViewBag.porAbierto = totalAbiertoPor;
            ViewBag.totalAbierto = lista.Where(c => c.EstadoCaso == "Abierto").Count();
            ViewBag.totalCerrado = lista.Where(c => c.EstadoCaso != "Abierto").Count();
            return View(lista);
        }

        [Route("ActividadesPorEstado")]
        public async Task<IActionResult> ActividadesPorEstado()
        {
            var lista = await generic.GetAll<VwModelActividadesPorEstado>("CuadroMando/ActividadesPorEstado");
      
            ViewBag.Reprogramada = lista.Where(c => c.Estado == "Reprogramada").Count();
            ViewBag.Programada = lista.Where(c => c.Estado == "Programada").Count();
            ViewBag.Realizada = lista.Where(c => c.Estado == "Realizada").Count();
            return View(lista);
        }

        [Route("CargaPorUsuario")]
        public async Task<IActionResult> CargaPorUsuario()
        {
            var lista = await generic.GetAll<VwModelCargaPorUsuario>("CuadroMando/CargaPorUsuario");
          
            return View(lista);
        }

        [Route("TopCincoActividades")]
        public IActionResult TopCincoActividades() {
            return View();
        }

    }
}