using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.BOL.BOL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PreOrclApi.Models;

namespace PreOrclApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisosController : ControllerBase
    {
        private readonly PreOrclApiContext _context;
        private BOLPermisos bol;

        public PermisosController(PreOrclApiContext context)
        {
            _context = context;
            bol = new BOLPermisos();
        }

        // GET: api/SisPerPersonas
        [HttpGet]
        public IEnumerable<Common.Entity.Models.Permisos> GetPermisos()
        {

            return bol.GetAllPermisos();
        }

        // GET: api/SisPerPersonas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPermisos([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var permisos = await bol.GetPermisos(id);

            if (permisos == null)
            {
                return NotFound();
            }

            return Ok(permisos);
        }

        // PUT: api/SisPerPersonas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPermisos([FromRoute] int id, [FromBody] Common.Entity.Models.Permisos permisos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != permisos.IdPermiso)
            {
                return BadRequest();
            }



            try
            {
                if (bol.GetPermisos(id) == null)
                {
                    return NotFound();
                }
                await bol.UpdatePermisos(id, permisos);


            }
            catch (Exception e)
            {
                if (bol.GetPermisos(id) == null)
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
        public async Task<IActionResult> PostPermisos([FromBody] Common.Entity.Models.Permisos permisos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            permisos = await bol.CreatePermisos(permisos);

            return CreatedAtAction("GetPermisos", new { id = permisos.IdPermiso }, permisos);
        }

        // DELETE: api/SisPerPersonas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermisos([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roles = await bol.DeletePermisos(id);

            if (roles == null)
            {
                return NotFound();
            }

            return Ok(roles);
        }

        private bool RolesPermisosExists(int id)
        {
            return _context.SisPerPersona.Any(e => e.per_IDPER == id);
        }
    }
}