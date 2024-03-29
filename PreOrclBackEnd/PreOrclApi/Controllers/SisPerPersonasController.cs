﻿using System;
using System.Collections.Generic;
using System.Linq;
using Common.BOL.BOL;
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
        BOLSisPerPersonas bol;

        public SisPerPersonasController(PreOrclApiContext context)
        {
            _context = context;
            bol = new BOLSisPerPersonas();
        }

        // GET: api/SisPerPersonas
        [HttpGet]
        public IEnumerable<Common.Entity.Models.SisPerPersona> GetSisPerPersona()
        {
                return bol.GetAllSisPerPersona();
        }

        // GET: api/SisPerPersonas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSisPerPersona([FromRoute] decimal id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
  
            var sisPerPersona = await bol.GetSisPerPersona(id);

            if (sisPerPersona == null)
            {
                return NotFound();
            }

            return Ok(sisPerPersona);
        }

        // PUT: api/SisPerPersonas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSisPerPersona([FromRoute] decimal id, [FromBody] Common.Entity.Models.SisPerPersona sisPerPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sisPerPersona.per_IDPER)
            {
                return BadRequest();
            }

            try
            {
                if (bol.GetSisPerPersona(id) == null)
                {
                    return NotFound();
                }
                await bol.UpdateSisPerPersona(id,sisPerPersona);


            }
            catch (Exception e)
            {
                if (bol.GetSisPerPersona(id) == null)
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
        public async Task<IActionResult> PostSisPerPersona([FromBody] Common.Entity.Models.SisPerPersona sisPerPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            sisPerPersona = await bol.CreatePersona(sisPerPersona);

            return CreatedAtAction("GetSisPerPersona", new { id = sisPerPersona.per_IDPER }, sisPerPersona);
        }

        // DELETE: api/SisPerPersonas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSisPerPersona([FromRoute] decimal id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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