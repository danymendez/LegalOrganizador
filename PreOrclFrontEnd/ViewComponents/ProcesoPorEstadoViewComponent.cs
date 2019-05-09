using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using PreOrclFrontEnd.Helpers;
using PreOrclFrontEnd.Utilidades;
using PreOrclFrontEnd.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.ViewComponents
{
    [ViewComponent(Name = "ProcesoPorEstadoVwComponent")]
    public class ProcesoPorEstadoViewComponent : ViewComponent
    {

        private GenericREST generic;
        private readonly IMemoryCache _memoryCache;
        private TokenT tokenT;
        private MSGraphConfiguration _msGraphConfig;

        public ProcesoPorEstadoViewComponent(IOptions<UriHelpers> configuration, IOptions<MSGraphConfiguration> msGraphConfig, IMemoryCache memoryCache)
        {
            _msGraphConfig = msGraphConfig.Value as MSGraphConfiguration;
            generic = new GenericREST(configuration.Value);
            _memoryCache = memoryCache;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Dictionary<string, decimal> dictionarioCaso = new Dictionary<string, decimal>();
            var lista = await generic.GetAll<VwModelProcesosPorEstado>("CuadroMando/ProcesoPorEstado");
            try
            {
                decimal total = lista.Select(c => c.IdCaso).Count();
                decimal totalAbiertoPor = ((lista.Where(c => c.EstadoCaso == "Abierto").Count()) / total) * 100;
                decimal totalCerrado = ((lista.Where(c => c.EstadoCaso != "Abierto").Count()) / total) * 100;
             

                dictionarioCaso.Add("totalCerrado", totalCerrado);
                dictionarioCaso.Add("totalAbierto", totalAbiertoPor);
                dictionarioCaso.Add("porAbierto", lista.Where(c => c.EstadoCaso == "Abierto").Count());
                dictionarioCaso.Add("porCerrado", lista.Where(c => c.EstadoCaso != "Abierto").Count());
               
            }
            catch {
                dictionarioCaso.Add("porCerrado", 0);
                dictionarioCaso.Add("porAbierto", 0);
                dictionarioCaso.Add("totalAbierto", 0);
                dictionarioCaso.Add("totalCerrado", 0);
            }
            return View("~/Views/CuadroMando/_PartialProcesoPorEstado.cshtml", dictionarioCaso);
        }

    }

}
