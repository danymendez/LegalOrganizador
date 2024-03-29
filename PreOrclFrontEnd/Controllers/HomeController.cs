﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PreOrclFrontEnd.Extensions;
using PreOrclFrontEnd.Helpers;
using PreOrclFrontEnd.Models;
using PreOrclFrontEnd.Utilidades;

namespace PreOrclFrontEnd.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        GenericREST generic;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _env;
        private readonly IGraphSdkHelper _graphSdkHelper;
        private readonly IMemoryCache _memoryCache;

        public HomeController(IOptions<UriHelpers> configuration, IConfiguration configurations, IHostingEnvironment hostingEnvironment, IGraphSdkHelper graphSdkHelper, IMemoryCache memoryCache)
        {
            _configuration = configurations;
            _env = hostingEnvironment;
            _graphSdkHelper = graphSdkHelper;
            generic = new GenericREST(configuration.Value);
            _memoryCache = memoryCache;
        }

    
        public async Task<IActionResult> Index(string email)
        {

            if (User.Identity.IsAuthenticated)
            {
                if(_memoryCache.Get("foto") !=null)
                ViewData["img"] = Encoding.ASCII.GetString(_memoryCache.Get("foto") as byte[]);
            }
            List<SisPerPersona> listaSisPersona = await generic.GetAll<SisPerPersona>("SisPerPersonas");
            ViewBag.Cantidad = listaSisPersona.Count(); 
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
