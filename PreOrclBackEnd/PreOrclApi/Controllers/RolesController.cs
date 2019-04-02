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
        private BOLRoles bol;

        public RolesController(PreOrclApiContext context)
        {
            _context = context;
            bol = new BOLRoles();
        }

        // GET: api/SisPerPersonas
        [HttpGet]
        public IEnumerable<Common.Entity.Models.Roles> GetRoles()
        {
            
            return bol.GetAllRoles();
        }

        // GET: api/SisPerPersonas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRol([FromRoute] decimal id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
     
            var roles = await bol.GetRol(id);

            if (roles == null)
            {
                return NotFound();
            }

            return Ok(roles);
        }

        // PUT: api/SisPerPersonas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoles([FromRoute] decimal id, [FromBody] Common.Entity.Models.Roles roles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roles.IdRol)
            {
                return BadRequest();
            }

          

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

            roles = await bol.CreateRoles(roles);

            return CreatedAtAction("GetRol", new { id = roles.IdRol }, roles);
        }

        // DELETE: api/SisPerPersonas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRol([FromRoute] decimal id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roles = await bol.DeleteRoles(id);

            if (roles == null)
            {
                return NotFound();
            }

            return Ok(roles);
        }

        private bool RolesExists(int id)
        {
            return _context.SisPerPersona.Any(e => e.per_IDPER == id);
        }
    }
}
