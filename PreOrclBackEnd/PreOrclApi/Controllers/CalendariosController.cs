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
    public class CalendariosController : ControllerBase
    {
        private BOLCalendar bol;

        public CalendariosController() {
            bol = new BOLCalendar();
        }

        [HttpGet]
        public async Task<IEnumerable<Common.Entity.Models.GraphCalendar>> GetCalendarios()
        {
            return await bol.GetCalendarByUsuario();
        }

    }
}