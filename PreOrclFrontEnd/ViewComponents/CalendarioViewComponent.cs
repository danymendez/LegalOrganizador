using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using PreOrclFrontEnd.Helpers;
using PreOrclFrontEnd.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.ViewComponents
{
    [ViewComponent(Name = "ListaComponent")]
    public class CalendarioViewComponent : ViewComponent
    { 

        private GenericREST generic;
        private readonly IMemoryCache _memoryCache;
        private  TokenT tokenT;
        private MSGraphConfiguration _msGraphConfig;

        public CalendarioViewComponent(IOptions<UriHelpers> configuration, IOptions<MSGraphConfiguration> msGraphConfig, IMemoryCache memoryCache) {
            _msGraphConfig = msGraphConfig.Value as MSGraphConfiguration;
            generic = new GenericREST(configuration.Value);
            _memoryCache = memoryCache;
           
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
             tokenT = _memoryCache.Get<TokenT>("TokenT");          
            var items = await GetItemsAsync(tokenT.access_token);
            return View("~/Views/Shared/Components/Calendario/ListCalendario.cshtml",items);
        }

        private Task<List<Event>> GetItemsAsync(string tokenacces)
        {
            GraphAuthCustom a = new GraphAuthCustom(_memoryCache,_msGraphConfig);
            var c = a.GetAuthenticatedClient(tokenacces);
            GraphServiceCustom sv = new GraphServiceCustom();
            Task<List<Event>> listas = Task.Run(() => {
                List<Event> lista = new List<Event>();
                return sv.GetMyCalendarView(c);
            });

            
            
            return listas;
        }

    }

    public class Items {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
