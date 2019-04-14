using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common.BOL.BOL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PreOrclApi.DTO;
using PreOrclApi.Models;
using PreOrclApi.Utilidades;

namespace PreOrclApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly PreOrclApiContext _context;
        private BOLUsuarios bolUsuarios;
        public UsuariosController(PreOrclApiContext context)
        {
            _context = context;
            bolUsuarios = new BOLUsuarios();
        }

        //GET: api/SisPerPersonas
        [HttpPost("Autenticar", Name = "Autenticar")]
        //[Route("Autenticar")]
        public async Task<IActionResult> Autenticar(string Usuario, string Password) // operationId = "Autenticar"
        {
            var usuario = await bolUsuarios.Autenticar(Usuario, Password);
            return Ok(new UsuariosDTO
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Usuario = usuario.Usuario
            });
        }

        [HttpGet]
        public async Task<IEnumerable<Common.Entity.Models.Usuarios>> GetUsuarios() {
            var usuarios = await bolUsuarios.GetUsuarios();
            foreach (var item in usuarios) {
                item.Password = null;
            }

            return usuarios;
           
        }

        // GET: api/SisPerPersonas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuariosById(decimal id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var usuario = await bolUsuarios.GetUsuario(id);
            usuario.Password = null;
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> PostRegistrar([FromBody] Common.Entity.Models.Usuarios pUsuarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bolUsuarios = new BOLUsuarios();
           
           var usuarioCreado = await bolUsuarios.CreateUsuario(pUsuarios);

            return CreatedAtAction("GetUsuarios", new { id = usuarioCreado.IdUsuario }, pUsuarios.Password=null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarios([FromRoute] decimal id, [FromBody] Common.Entity.Models.Usuarios pUsuarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pUsuarios.IdUsuario)
            {
                return BadRequest();
            }

            try
            {
                if (bolUsuarios.GetUsuario(id) == null)
                {
                    return NotFound();
                }
                await bolUsuarios.UpdateUsuarios(id, pUsuarios);


            }
            catch (Exception e)
            {
                if (bolUsuarios.GetUsuario(id) == null)
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


    }
}