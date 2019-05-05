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
    public class CuadroMandoController : ControllerBase
    {
        private readonly PreOrclApiContext _context;
        private BOLCasos bol;
        private BOLUsuarios bolUsuarios;
        private BOLActividades bolActividades;

        public CuadroMandoController(PreOrclApiContext context)
        {
            _context = context;
            bol = new BOLCasos();
            bolUsuarios = new BOLUsuarios();
            bolActividades = new BOLActividades();
        }

        // GET: api/SisPerPersonas
        [HttpGet("ProcesoPorEstado", Name = "ProcesoPorEstado")]
        public IEnumerable<Common.Entity.ViewModels.VwModelProcesosPorEstado> ProcesoPorEstado()
        {

            var caso = bol.GetAllCasos();
            var responsables = bolUsuarios.GetUsuarios().Result;
            List<Common.Entity.ViewModels.VwModelProcesosPorEstado> listadoCaso = new List<Common.Entity.ViewModels.VwModelProcesosPorEstado>();
            foreach (var itemsCasos in caso) {
                listadoCaso.Add(new Common.Entity.ViewModels.VwModelProcesosPorEstado
                {
                    IdCaso = itemsCasos.IdCaso,
                    NombreCaso = itemsCasos.NombreCaso,
                    Responsable = responsables.Where(c=>c.IdUsuario==itemsCasos.IdAbogado).Select(c=>c.Nombre).FirstOrDefault() + " "+ responsables.Where(c => c.IdUsuario == itemsCasos.IdAbogado).Select(c => c.Apellido).FirstOrDefault(),
                    EstadoCaso = itemsCasos.EstadoCaso=="A"?"Abierto":"Cerrado"
                    
                });
            }
            return listadoCaso;
        }

        // GET: api/SisPerPersonas
        [HttpGet("ActividadesPorEstado", Name = "ActividadesPorEstado")]
        public IEnumerable<Common.Entity.ViewModels.VwModelActividadesPorEstado> ActividadesPorEstado()
        {

            var actividades = bolActividades.GetAllActividades();
            var responsables = bolUsuarios.GetUsuarios().Result;
            List<Common.Entity.ViewModels.VwModelActividadesPorEstado> listadoActividades = new List<Common.Entity.ViewModels.VwModelActividadesPorEstado>();
            foreach (var itemsActividades in actividades)
            {
                listadoActividades.Add(new Common.Entity.ViewModels.VwModelActividadesPorEstado
                {
                    IdActividad = itemsActividades.IdActividad,
                    NombreActividad = itemsActividades.NombreActividad,
                    EndDate=itemsActividades.StartTime,
                    StartDate=itemsActividades.EndTime,
                    Responsable = responsables.Where(c => c.IdUsuario == itemsActividades.IdResponsable).Select(c => c.Nombre).FirstOrDefault() + " " + responsables.Where(c => c.IdUsuario == itemsActividades.IdResponsable).Select(c => c.Apellido).FirstOrDefault(),
                    Estado = itemsActividades.Estado == "R" ? "Realizada" : itemsActividades.Estado == "E"?"Reprogramada":"Programada"

                });
            }
            return listadoActividades;
        }


        // GET: api/SisPerPersonas
        [HttpGet("CargaPorUsuario", Name = "CargaPorUsuario")]
        public IEnumerable<Common.Entity.ViewModels.VwModelCargaPorUsuario> CargaPorUsuario()
        {

            var actividades = bolActividades.GetAllActividades();
            var responsables = bolUsuarios.GetUsuarios().Result.Where(c=>c.TipoUsuario.Trim().ToUpperInvariant()=="I");
            List<Common.Entity.ViewModels.VwModelCargaPorUsuario> listadoPorCargaTrabajo = new List<Common.Entity.ViewModels.VwModelCargaPorUsuario>();
            foreach (var itemsResponsables in responsables)
            {
                listadoPorCargaTrabajo.Add(new Common.Entity.ViewModels.VwModelCargaPorUsuario
                {
                    IdUsuario = itemsResponsables.IdUsuario,
                    Nombre = itemsResponsables.Nombre,
                    Apellido = itemsResponsables.Apellido,
                    Usuario= itemsResponsables.Usuario,
                    CantidadCasoAbierto = actividades.Where(c => c.IdResponsable == itemsResponsables.IdUsuario).Count()

                });
            }
            return listadoPorCargaTrabajo;
        }

    }
}