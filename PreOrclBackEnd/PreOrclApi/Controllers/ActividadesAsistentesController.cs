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
    public class ActividadesAsistentesController : ControllerBase
    {
        private readonly PreOrclApiContext _context;
        private BOLActividadesAsistentes bol;

        public ActividadesAsistentesController(PreOrclApiContext context)
        {
            _context = context;
            bol = new BOLActividadesAsistentes();
        }

        // GET: api/SisPerPersonas
        [HttpGet]
        public IEnumerable<Common.Entity.Models.ActividadesAsistentes> GetActividadesAsistentes()
        {
            return bol.GetAllActividadesAsistentes();
        }

        // GET: api/SisPerPersonas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActividadAsistente([FromRoute] decimal id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actividad = await bol.GetActividadAsistente(id);

            if (actividad == null)
            {
                return NotFound();
            }

            return Ok(actividad);
        }

        // PUT: api/SisPerPersonas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActividadesAsistentes([FromRoute] decimal id, [FromBody] Common.Entity.Models.ActividadesAsistentes ActividadesAsistentes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ActividadesAsistentes.IdActividad)
            {
                return BadRequest();
            }



            try
            {
                if (bol.GetActividadAsistente(id) == null)
                {
                    return NotFound();
                }
                await bol.UpdateActividadesAsistentes(id, ActividadesAsistentes);


            }
            catch (Exception e)
            {
                if (bol.GetActividadAsistente(id) == null)
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
        public async Task<IActionResult> PostActividadesAsistentes([FromBody] Common.Entity.Models.ActividadesAsistentes ActividadesAsistentes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ActividadesAsistentes = await bol.CreateActividadesAsistentes(ActividadesAsistentes);

            return CreatedAtAction("GetActividadAsistente", new { id = ActividadesAsistentes.IdActividadAsistentes }, ActividadesAsistentes);
        }

        // DELETE: api/SisPerPersonas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActividad([FromRoute] decimal id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ActividadesAsistentes = await bol.DeleteActividadesAsistentes(id);

            if (ActividadesAsistentes == null)
            {
                return NotFound();
            }

            return Ok(ActividadesAsistentes);
        }

        private bool ActividadesAsistentesExists(int id)
        {
            return _context.SisPerPersona.Any(e => e.per_IDPER == id);
        }
    }
}
