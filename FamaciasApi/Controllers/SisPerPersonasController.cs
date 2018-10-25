using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PreOrclApi.Models;

namespace PreOrclApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SisPerPersonasController : ControllerBase
    {
        private readonly PreOrclApiContext _context;

        public SisPerPersonasController(PreOrclApiContext context)
        {
            _context = context;
        }

        // GET: api/SisPerPersonas
        [HttpGet]
        public IEnumerable<SisPerPersona> GetSisPerPersona()
        {
            List<SisPerPersona> lista = new List<SisPerPersona>();

            using (DALBaseOrcl dal = new DALBaseOrcl()) {

                lista = dal.GetAll<SisPerPersona>();
            }

                return lista;
        }

        // GET: api/SisPerPersonas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSisPerPersona([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sisPerPersona = await _context.SisPerPersona.FindAsync(id);

            if (sisPerPersona == null)
            {
                return NotFound();
            }

            return Ok(sisPerPersona);
        }

        // PUT: api/SisPerPersonas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSisPerPersona([FromRoute] int id, [FromBody] SisPerPersona sisPerPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sisPerPersona.per_IDPER)
            {
                return BadRequest();
            }

            _context.Entry(sisPerPersona).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SisPerPersonaExists(id))
                {
                    return NotFound();
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
        public async Task<IActionResult> PostSisPerPersona([FromBody] SisPerPersona sisPerPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SisPerPersona sisPer = new SisPerPersona();

            using (DALBaseOrcl dal = new DALBaseOrcl()) {

                sisPer = dal.Create(sisPerPersona);
            }


            return CreatedAtAction("GetSisPerPersona", new { id = sisPerPersona.per_IDPER }, sisPerPersona);
        }

        // DELETE: api/SisPerPersonas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSisPerPersona([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sisPerPersona = await _context.SisPerPersona.FindAsync(id);
            if (sisPerPersona == null)
            {
                return NotFound();
            }

            _context.SisPerPersona.Remove(sisPerPersona);
            await _context.SaveChangesAsync();

            return Ok(sisPerPersona);
        }

        private bool SisPerPersonaExists(int id)
        {
            return _context.SisPerPersona.Any(e => e.per_IDPER == id);
        }
    }
}