using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PreOrclFrontEnd.Helpers;
using PreOrclFrontEnd.Models;
using PreOrclFrontEnd.Utilidades;

namespace PreOrclFrontEnd.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        GenericREST generic;


        public HomeController(IOptions<ConfigurationJson> configuration)
        {
            generic = new GenericREST(configuration.Value);
        }


        public IActionResult Index()
        {
            Task<List<SisPerPersona>> t = Task.Run(() => {
                List<SisPerPersona> listaSisPersona = generic.GetAll<SisPerPersona>("SisPerPersonas");
                return listaSisPersona;
            });
            t.Wait();
            var lista = t.Result;
            ViewBag.Cantidad = lista.Count(); 
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
