using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PreOrclFrontEnd.Helpers;
using PreOrclFrontEnd.Utilidades;

namespace PreOrclFrontEnd.Controllers
{
    public class RolesController : Controller
    {

        private GenericREST generic;

        public RolesController(IOptions<UriHelpers> configuration)
        {

            generic = new GenericREST(configuration.Value);

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}