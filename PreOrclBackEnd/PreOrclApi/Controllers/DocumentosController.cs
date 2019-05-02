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
    public class DocumentosController : ControllerBase
    {
        private readonly PreOrclApiContext _context;
        private BOLDocumentos bol;

        public DocumentosController(PreOrclApiContext context)
        {
            _context = context;
            bol = new BOLDocumentos();
        }

        // GET: api/SisPerPersonas
        [HttpGet]
        public IEnumerable<Common.Entity.Models.Documentos> GetDocumentos()
        {
            return bol.GetAllDocumentos();
        }


        // GET: api/SisPerPersonas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumento([FromRoute] decimal id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var caso = await bol.GetDocumento(id);

            if (caso == null)
            {
                return NotFound();
            }

            return Ok(caso);
        }

        // PUT: api/SisPerPersonas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumentos([FromRoute] decimal id, [FromBody] Common.Entity.Models.Documentos pDocumentos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pDocumentos.IdDocumento)
            {
                return BadRequest();
            }



            try
            {
                if (bol.GetDocumento(id) == null)
                {
                    return NotFound();
                }
                await bol.UpdateDocumentos(id, pDocumentos);


            }
            catch (Exception e)
            {
                if (bol.GetDocumento(id) == null)
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
        public async Task<IActionResult> PostDocumentos([FromBody] Common.Entity.Models.Documentos pDocumentos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pDocumentos = await bol.CreateDocumentos(pDocumentos);

            return CreatedAtAction("GetCasoCliente", new { id = pDocumentos.IdDocumento }, pDocumentos);
        }

        // DELETE: api/SisPerPersonas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentos([FromRoute] decimal id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Documentos = await bol.DeleteDocumentos(id);

            if (Documentos == null)
            {
                return NotFound();
            }

            return Ok(Documentos);
        }

        private bool DocumentosExists(int id)
        {
            return _context.SisPerPersona.Any(e => e.per_IDPER == id);
        }
    }
}
