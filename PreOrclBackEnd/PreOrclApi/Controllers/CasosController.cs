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
    public class CasosController : ControllerBase
    {
        private readonly PreOrclApiContext _context;
        private BOLCasos bol;

        public CasosController(PreOrclApiContext context)
        {
            _context = context;
            bol = new BOLCasos();
        }

        // GET: api/SisPerPersonas
        [HttpGet]
        public IEnumerable<Common.Entity.Models.Casos> GetCasos()
        {
            return bol.GetAllCasos();
        }

        [HttpGet("GetAllVwModelCasos", Name = "GetAllVwModelCasos")]
        public async Task<IEnumerable<Common.Entity.ViewModels.VwModelCasos>> GetVwModelCasos()
        {
            return await bol.GetAllVwModelCasos();
        }

        [HttpGet("GetVwModelCasos/{id}", Name = "GetVwModelCasos")]
        public async Task<Common.Entity.ViewModels.VwModelCasos> GetVwModelCasos(decimal id)
        {
            return await bol.GetVwModelCasos(id);
        }

        // GET: api/SisPerPersonas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCaso([FromRoute] decimal id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var caso = await bol.GetCaso(id);

            if (caso == null)
            {
                return NotFound();
            }

            return Ok(caso);
        }

        // PUT: api/SisPerPersonas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCasos([FromRoute] decimal id, [FromBody] Common.Entity.Models.Casos pCasos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pCasos.IdCaso)
            {
                return BadRequest();
            }



            try
            {
                if (bol.GetCaso(id) == null)
                {
                    return NotFound();
                }
                await bol.UpdateCasos(id, pCasos);


            }
            catch (Exception e)
            {
                if (bol.GetCaso(id) == null)
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
        public async Task<IActionResult> PostCasos([FromBody] Common.Entity.Models.Casos pCasos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pCasos = await bol.CreateCasos(pCasos);

            return CreatedAtAction("GetCaso", new { id = pCasos.IdCaso }, pCasos);
        }

        [HttpPost("PostVwModelCasos", Name = "PostVwModelCasos")]
        public async Task<IActionResult> PostVwModelCasos([FromBody] Common.Entity.ViewModels.VwModelCasos vwModelCasos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           var casos = await bol.CreateVwModelCasos(vwModelCasos);
            vwModelCasos =await GetVwModelCasos(casos.IdCaso);
            return CreatedAtAction("Casos/GetVwModelCasos", new { id = vwModelCasos.Casos.IdCaso }, vwModelCasos);
        }

        // DELETE: api/SisPerPersonas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCasos([FromRoute] decimal id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var casos = await bol.DeleteCasos(id);

            if (casos == null)
            {
                return NotFound();
            }

            return Ok(casos);
        }

        private bool CasosExists(int id)
        {
            return _context.SisPerPersona.Any(e => e.per_IDPER == id);
        }
    }
}
