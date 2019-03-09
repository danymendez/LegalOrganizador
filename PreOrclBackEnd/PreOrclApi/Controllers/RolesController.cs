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
    public class RolesController : ControllerBase
    {
        private readonly PreOrclApiContext _context;

        public RolesController(PreOrclApiContext context)
        {
            _context = context;
        }

        // GET: api/SisPerPersonas
        [HttpGet]
        public IEnumerable<Common.Entity.Models.Roles> GetRoles()
        {
            BOLRoles bol = new BOLRoles();
            return bol.GetAllRoles();
        }

        // GET: api/SisPerPersonas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRol([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BOLRoles bol = new BOLRoles();
            var roles = await bol.GetRol(id);

            if (roles == null)
            {
                return NotFound();
            }

            return Ok(roles);
        }

        // PUT: api/SisPerPersonas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSisRoles([FromRoute] int id, [FromBody] Common.Entity.Models.Roles roles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roles.IdRol)
            {
                return BadRequest();
            }

            BOLRoles bol = new BOLRoles();

            try
            {
                if (bol.GetRol(id) == null)
                {
                    return NotFound();
                }
                await bol.UpdateRoles(id, roles);


            }
            catch (Exception e)
            {
                if (bol.GetRol(id) == null)
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
        public async Task<IActionResult> PostRoles([FromBody] Common.Entity.Models.Roles roles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BOLRoles bol = new BOLRoles();
            roles = await bol.CreateRoles(roles);

            return CreatedAtAction("GetSisPerPersona", new { id = roles.IdRol }, roles);
        }

        // DELETE: api/SisPerPersonas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSisPerPersona([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var sisPerPersona = await _context.SisPerPersona.FindAsync(id);
            //if (sisPerPersona == null)
            //{
            //    return NotFound();
            //}

            BOLSisPerPersonas bol = new BOLSisPerPersonas();

            var sisPerPersona = await bol.DeleteSisPerPersona(id);

            if (sisPerPersona == null)
            {
                return NotFound();
            }

            return Ok(sisPerPersona);
        }

        private bool SisPerPersonaExists(int id)
        {
            return _context.SisPerPersona.Any(e => e.per_IDPER == id);
        }
    }
}
