using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamaciasApi.DAL;
using FamaciasApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FamaciasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            using (DALBaseOrcl baseorc = new DALBaseOrcl()) {

                baseorc.Create(new Usuarios {
                    IdUsuario = 0, Usuario="DanieListo",Password="1234"
                });
            }

                ////    var datos = dal.GetTableAttributes<Usuarios>();
                ////string valores = dal.SqlInsertQueryBuilder<Usuarios>(new Usuarios());

                return new string[] { "va", "ve" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

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
