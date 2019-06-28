using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DAL;
using PreOrclApi.Models;
using Common.Data.SQLBuilders;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace PreOrclApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IHostingEnvironment _hostingEnvironment;

        public ValuesController(IHostingEnvironment hostingEnvironment) {
            _hostingEnvironment = hostingEnvironment;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
           
            return new string[] { "va", System.TimeZoneInfo.Local.StandardName };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        //[Route("Descargar")]
        //public PhysicalFileProvider DescargarDocumento()
        //{
            
        //    string FullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "logs","ErrorLog.txt");

        //    return new PhysicalFileProvider(FullPath);
        //}

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
