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
    public class CalendarioEventosController : ControllerBase
    {
        private BOLCalendar bol;

        public CalendarioEventosController()
        {
            bol = new BOLCalendar();
        }


        [HttpGet]
        public async Task<IEnumerable<Common.Entity.Models.GraphCalendarEvents>> GetCalendarioEventosController()
        {
            return await bol.GetCalendarEventsByUsuario();
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<Common.Entity.Models.GraphCalendarEvents>> GetCalendarioEventosController([FromRoute] decimal id) {
            return (await bol.GetCalendarEventsByUsuario()).Where(c=>c.GraphCalendar.IdUsuario==id);
        }

    }
}