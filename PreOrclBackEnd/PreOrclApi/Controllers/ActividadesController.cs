using Common.BOL.BOL;
using Microsoft.AspNetCore.Mvc;
using PreOrclApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreOrclApi.Controllers
{
    [Route("api/[controller]")]
    public class ActividadesController : ControllerBase
    {
        private readonly PreOrclApiContext _context;
        private BOLActividades bol;

        public ActividadesController(PreOrclApiContext context)
        {
            _context = context;
            bol = new BOLActividades();
        }

        // GET: api/SisPerPersonas
        [HttpGet]
        public IEnumerable<Common.Entity.Models.Actividades> GetActividades()
        {
            return bol.GetAllActividades();
        }

        // GET: api/SisPerPersonas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActividad([FromRoute] decimal id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actividad = await bol.GetActividad(id);

            if (actividad == null)
            {
                return NotFound();
            }

            return Ok(actividad);
        }

        // PUT: api/SisPerPersonas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActividades([FromRoute] decimal id, [FromBody] Common.Entity.Models.Actividades actividades)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != actividades.IdActividad)
            {
                return BadRequest();
            }



            try
            {
                if (bol.GetActividad(id) == null)
                {
                    return NotFound();
                }
                await bol.UpdateActividades(id, actividades);


            }
            catch (Exception e)
            {
                if (bol.GetActividad(id) == null)
                {
                    return NotFound(e.Message);
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SisPerPersonas
        [HttpPost]
        public async Task<IActionResult> PostActividades([FromBody] Common.Entity.Models.Actividades actividades)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            actividades = await bol.CreateActividades(actividades);

            return CreatedAtAction("GetActividad", new { id = actividades.IdActividad }, actividades);
        }

        // DELETE: api/SisPerPersonas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActividad([FromRoute] decimal id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actividades = await bol.DeleteActividades(id);

            if (actividades == null)
            {
                return NotFound();
            }

            return Ok(actividades);
        }

        private bool ActividadesExists(int id)
        {
            return _context.SisPerPersona.Any(e => e.per_IDPER == id);
        }
    }
}
