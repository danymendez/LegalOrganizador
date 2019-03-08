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
        public UsuariosController(PreOrclApiContext context)
        {
            _context = context;

        }

        //GET: api/SisPerPersonas
       [HttpPost("Autenticar", Name ="Autenticar")]
       //[Route("Autenticar")]
        public async Task<IActionResult> Autenticar(string Usuario, string Password) // operationId = "Autenticar"
        {
            BOLUsuarios bolUsuarios = new BOLUsuarios();
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
        public async Task <IEnumerable<UsuariosDTO>> GetUsuarios() {
            BOLUsuarios bolUsuarios = new BOLUsuarios();
            var listaUsuarios = await bolUsuarios.GetUsuarios();
            var listaDto = from usuarios in listaUsuarios
                           select new UsuariosDTO
                           {
                               IdUsuario=usuarios.IdUsuario,
                               Nombre=usuarios.Nombre,
                               Apellido=usuarios.Apellido,
                               Usuario=usuarios.Usuario,
                               Token = usuarios.Token,
                               TokenExpired = usuarios.TokenExpiredAt,
                               TokenRefresh = usuarios.TokenRefresh,
                               CreatedAt = usuarios.CreatedAt,
                               UpdatedAt = usuarios.UpdatedAt,
                               InactivatedAt = usuarios.InactivatedAt,
                               Inactivo = usuarios.Inactivo
                           };
            return listaDto;
        }

        // GET: api/SisPerPersonas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuariosById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BOLUsuarios bol = new BOLUsuarios();
            var usuario = await bol.GetUsuario(id);
            UsuariosDetailsDTO usuariosDetailsDTO = new UsuariosDetailsDTO
            {
                IdUsuario=usuario.IdUsuario,
                Nombre=usuario.Nombre,
                Apellido=usuario.Apellido,
                Usuario=usuario.Usuario,
                FechaNac=usuario.FechaNac,
                Token = usuario.Token,
                TokenExpiredAt = usuario.TokenExpiredAt,
                TokenRefresh = usuario.TokenRefresh,
                CreatedAt =usuario.CreatedAt,
                IdRol = usuario.IdRol,
                UpdatedAt = usuario.UpdatedAt,
                InactivatedAt = usuario.InactivatedAt,
                Inactivo = usuario.Inactivo
            };
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuariosDetailsDTO);
        }

        [HttpPost]
        public async Task<IActionResult> PostRegistrar([FromBody] UsuariosDetailsDTO pUsuariosDetailsDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BOLUsuarios bol = new BOLUsuarios();
            Common.Entity.Models.Usuarios usuarioEntity = new Common.Entity.Models.Usuarios {
                IdUsuario=pUsuariosDetailsDTO.IdUsuario,
                Nombre=pUsuariosDetailsDTO.Nombre,
                Apellido=pUsuariosDetailsDTO.Apellido,
                Usuario=pUsuariosDetailsDTO.Usuario,
                Password=pUsuariosDetailsDTO.Password,
            };
           var usuarioCreado = await bol.CreateUsuario(usuarioEntity);

            return CreatedAtAction("GetUsuarios", new { id = usuarioCreado.IdUsuario }, pUsuariosDetailsDTO.Password=null);
        }


    }
}