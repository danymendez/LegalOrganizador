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
    public class RolesPermisosController : ControllerBase
    {
        private readonly PreOrclApiContext _context;
        private BOLRolesPermisos bol;

        public RolesPermisosController(PreOrclApiContext context)
        {
            _context = context;
            bol = new BOLRolesPermisos();
        }

        // GET: api/SisPerPersonas
        [HttpGet]
        public IEnumerable<Common.Entity.Models.RolesPermisos> GetRolesPermisos()
        {

            return bol.GetAllRolesPermisos();
        }

        // GET: api/SisPerPersonas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRolesPermisos([FromRoute] decimal id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rolesPermisos = await bol.GetRolesPermisos(id);

            if (rolesPermisos == null)
            {
                return NotFound();
            }

            return Ok(rolesPermisos);
        }

        // PUT: api/SisPerPersonas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRolesPermisos([FromRoute] decimal id, [FromBody] Common.Entity.Models.RolesPermisos rolesPermisos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rolesPermisos.IdRolPermiso)
            {
                return BadRequest();
            }



            try
            {
                if (bol.GetRolesPermisos(id) == null)
                {
                    return NotFound();
                }
                await bol.UpdateRolesPermisos(id, rolesPermisos);


            }
            catch (Exception e)
            {
                if (bol.GetRolesPermisos(id) == null)
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
        public async Task<IActionResult> PostRolesPermisos([FromBody] Common.Entity.Models.RolesPermisos rolesPermisos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            rolesPermisos = await bol.CreateRolesPermisos(rolesPermisos);

            return CreatedAtAction("GetRolesPermisos", new { id = rolesPermisos.IdRolPermiso }, rolesPermisos);
        }

        // DELETE: api/SisPerPersonas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRolesPermisos([FromRoute] decimal id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roles = await bol.DeleteRolesPermisos(id);

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