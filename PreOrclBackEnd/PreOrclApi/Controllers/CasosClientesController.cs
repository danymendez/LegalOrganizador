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
    public class CasosClientesController : ControllerBase
    {
        private readonly PreOrclApiContext _context;
        private BOLCasosClientes bol;

        public CasosClientesController(PreOrclApiContext context)
        {
            _context = context;
            bol = new BOLCasosClientes();
        }

        // GET: api/SisPerPersonas
        [HttpGet]
        public IEnumerable<Common.Entity.Models.CasosClientes> GetCasosClientes()
        {
            return bol.GetAllCasosClientes();
        }


        // GET: api/SisPerPersonas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCasoCliente([FromRoute] decimal id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var caso = await bol.GetCasoCliente(id);

            if (caso == null)
            {
                return NotFound();
            }

            return Ok(caso);
        }

        // PUT: api/SisPerPersonas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCasosClientes([FromRoute] decimal id, [FromBody] Common.Entity.Models.CasosClientes pCasosClientes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pCasosClientes.IdCasoCliente)
            {
                return BadRequest();
            }



            try
            {
                if (bol.GetCasoCliente(id) == null)
                {
                    return NotFound();
                }
                await bol.UpdateCasosClientes(id, pCasosClientes);


            }
            catch (Exception e)
            {
                if (bol.GetCasoCliente(id) == null)
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
        public async Task<IActionResult> PostCasosClientes([FromBody] Common.Entity.Models.CasosClientes pCasosClientes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pCasosClientes = await bol.CreateCasosClientes(pCasosClientes);

            return CreatedAtAction("GetCasoCliente", new { id = pCasosClientes.IdCasoCliente }, pCasosClientes);
        }

        // DELETE: api/SisPerPersonas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCasosClientes([FromRoute] decimal id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var CasosClientes = await bol.DeleteCasosClientes(id);

            if (CasosClientes == null)
            {
                return NotFound();
            }

            return Ok(CasosClientes);
        }

        private bool CasosClientesExists(int id)
        {
            return _context.SisPerPersona.Any(e => e.per_IDPER == id);
        }
    }
}
