using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamaciasApi.DAL;
using FamaciasApi.Models;
using FamaciasApi.SQLBuilders;
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
            //using (DALBaseOrcl baseorc = new DALBaseOrcl("")) {

            //    string a = baseorc.SqlInsertQueryBuilder(new Usuarios());

            //    //baseorc.Create(new Usuarios {
            //    //    IdUsuario = 0, Usuario="DanieListo",Password="1234"
            //    //});
            //}
            SqlQueryBuilder sqlQueryBuilder = new SqlQueryBuilder();

            string insert = sqlQueryBuilder.InsertQuery<Usuarios>();
            string update = sqlQueryBuilder.UpdateQuery<Usuarios>();
            string selectAll = sqlQueryBuilder.SelectAllQuery<Usuarios>();
            string select = sqlQueryBuilder.SelectQuery<Usuarios>();
            string delete = sqlQueryBuilder.DeleteQuery<Usuarios>();

            SqlParameterBuilder sqlParameter = new SqlParameterBuilder();

            var sqlParamInser = sqlParameter.InsertParametersBuilder(new Usuarios { IdUsuario = 0, Usuario = "", Password = "" });
            var sqlParamUpdate = sqlParameter.SelectOrUpdateParametersBuilder(new Usuarios { IdUsuario = 0, Usuario = "", Password = "" });
            var sqlParamDelete = sqlParameter.DeleteParameterBuilder(new Usuarios { IdUsuario = 0, Usuario = "", Password = "" });

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
