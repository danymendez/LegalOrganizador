using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using PreOrclFrontEnd.Helpers;
using PreOrclFrontEnd.Utilidades;
using PreOrclFrontEnd.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.ViewComponents
{
    [ViewComponent(Name = "ActividadesPorEstadoViewComponent")]
    public class ActividadesPorEstadoViewComponents : ViewComponent
    {
        private GenericREST generic;
        private readonly IMemoryCache _memoryCache;
        private TokenT tokenT;
        private MSGraphConfiguration _msGraphConfig;

        public ActividadesPorEstadoViewComponents(IOptions<UriHelpers> configuration, IOptions<MSGraphConfiguration> msGraphConfig, IMemoryCache memoryCache)
        {
            _msGraphConfig = msGraphConfig.Value as MSGraphConfiguration;
            generic = new GenericREST(configuration.Value);
            _memoryCache = memoryCache;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var lista = await generic.GetAll<VwModelActividadesPorEstado>("CuadroMando/ActividadesPorEstado");
            Dictionary<string, int> dictionarioActividades = new Dictionary<string, int>();
            dictionarioActividades.Add("Reprogramada",lista.Where(c => c.Estado == "Reprogramada").Count());
            dictionarioActividades.Add("Programada" , lista.Where(c => c.Estado == "Programada").Count());
            dictionarioActividades.Add("Realizada",lista.Where(c => c.Estado == "Realizada").Count());

            return View("~/Views/CuadroMando/_PartialActividadesPorEstado.cshtml", dictionarioActividades);
        }

    }
}
