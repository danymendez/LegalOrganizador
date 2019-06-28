using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.BOL.BOL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PreOrclApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private BOLCalendar bol;

        public EventosController()
        {
            bol = new BOLCalendar();
        }

        [HttpGet]
        public async Task<IEnumerable<Common.Entity.Models.GraphEvents>> GetEventos()
        {
            return await bol.GetEventosByIdUsuario();
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<Common.Entity.Models.GraphEvents>> GetEventos(decimal id)
        {
            return await bol.GetEventosByIdUsuario(id);
        }
    }
}