using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PreOrclFrontEnd.Helpers;
using PreOrclFrontEnd.Models;
using PreOrclFrontEnd.Utilidades;
using PreOrclFrontEnd.ViewModels;

namespace PreOrclFrontEnd.Controllers
{
    public class RolesController : Controller
    {

        private GenericREST generic;

        public RolesController(IOptions<UriHelpers> configuration)
        {

            generic = new GenericREST(configuration.Value);

        }

        public async Task<IActionResult> Index()
        {
            List<Roles> listaRoles = await generic.GetAll<Roles>("Roles");
            ViewBag.PersonasClassCssNav = "active";
            return View(listaRoles);
        }

        public IActionResult CreatePartial()
        {
            VwModelRolesPermisos vwModel = new VwModelRolesPermisos();
            vwModel.VwModelPermisos = generic.GetAll<VwModelPermisos>("Permisos").Result;
            ViewBag.Title = "Crear Rol";
            return PartialView("../Roles/_CreateOrEditPartial", vwModel);
        }

        public async Task<IActionResult> InactivateOrActivatePartial(decimal? id)
        {
            VwModelRolesPermisos vwModel = new VwModelRolesPermisos();
            vwModel.VwModelPermisos = new List<VwModelPermisos>();
            Roles roles = new Roles();
            roles = await generic.Get<Roles>("Roles/", id);
            vwModel.IdRol = roles.IdRol;
            vwModel.NombreRol = roles.NombreRol;
            vwModel.InactivatedAt = roles.InactivatedAt;
            vwModel.Inactivo = roles.Inactivo;
            vwModel.UpdatedAt = roles.UpdatedAt;
            vwModel.CreatedAt = roles.CreatedAt;
            List<Permisos> listaPermisos = await generic.GetAll<Permisos>("Permisos");
            var rolesPermisos = from rolPermiso in await generic.GetAll<RolesPermisos>("RolesPermisos")
                                join permiso in listaPermisos on rolPermiso.IdPermiso equals permiso.IdPermiso
                                where rolPermiso.IdRol == id
                                select new Permisos
                                {
                                    //  Seleccionado = permiso.Inactivo==0?false:true,
                                    IdPermiso = permiso.IdPermiso,
                                    NombrePermiso = permiso.NombrePermiso,
                                    CreatedAt = permiso.CreatedAt,
                                    UpdatedAt = permiso.UpdatedAt,
                                    InactivatedAt = permiso.InactivatedAt,
                                    Inactivo = permiso.Inactivo

                                };



            foreach (var item in listaPermisos)
            {

                VwModelPermisos vwModelPermisos = new VwModelPermisos
                {
                    IdPermiso = item.IdPermiso,
                    NombrePermiso = item.NombrePermiso,
                    CreatedAt = item.CreatedAt,
                    UpdatedAt = item.UpdatedAt,
                    InactivatedAt = item.InactivatedAt,
                    Inactivo = item.Inactivo,
                    Seleccionado = rolesPermisos.Where(c => c.IdPermiso == item.IdPermiso).Count() == 0 ? false : true,
                };

                vwModel.VwModelPermisos.Add(vwModelPermisos);

            }
            ViewBag.Title = "Inactivar Rol";
            ViewBag.ButtonTextSave = "Inactivar";
            if (vwModel.Inactivo == 1)
            {
                ViewBag.ButtonTextSave = "Reactivar";
                ViewBag.Title = "Reactivar Rol";
            }
          
           
            return PartialView("../Roles/_InactivateOrActivatePartial", vwModel);
        }

        public async Task<IActionResult> InactivateOrActivate(VwModelRolesPermisos vwModelRolesPermisos)
        {
            if (vwModelRolesPermisos.Inactivo == 1)
            {
                var rolToInactivated = await generic.Get<Roles>("Roles/", vwModelRolesPermisos.IdRol);
                rolToInactivated.Inactivo = 1;
                rolToInactivated.InactivatedAt = DateTime.Now;
                rolToInactivated.UpdatedAt = DateTime.Now;

                bool Actualizado = await generic.Put("Roles/", rolToInactivated.IdRol, rolToInactivated);

                if (Actualizado)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Ha ocurrido un error al inactivar");
                }

            }
            else {
                var rolToInactivated = await generic.Get<Roles>("Roles/", vwModelRolesPermisos.IdRol);
                rolToInactivated.Inactivo = 0;
                rolToInactivated.InactivatedAt = null;
                rolToInactivated.UpdatedAt = DateTime.Now;

                bool Actualizado = await generic.Put("Roles/", rolToInactivated.IdRol, rolToInactivated);

                if (Actualizado)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Ha ocurrido un error al inactivar");
                }
            }
        }

        public async Task<IActionResult> EditPartial(decimal? id)
        {
            VwModelRolesPermisos vwModel = new VwModelRolesPermisos();
            vwModel.VwModelPermisos = new List<VwModelPermisos>();
            Roles roles = new Roles();
            roles = await generic.Get<Roles>("Roles/", id);
            vwModel.IdRol = roles.IdRol;
            vwModel.NombreRol = roles.NombreRol;
            List<Permisos> listaPermisos = await generic.GetAll<Permisos>("Permisos");
            var rolesPermisos = from rolPermiso in await generic.GetAll<RolesPermisos>("RolesPermisos")
                                join permiso in listaPermisos on rolPermiso.IdPermiso equals permiso.IdPermiso
                                where rolPermiso.IdRol == id select new Permisos
                                                                                {
                                                                                 //  Seleccionado = permiso.Inactivo==0?false:true,
                                                                                   IdPermiso = permiso.IdPermiso,
                                                                                   NombrePermiso = permiso.NombrePermiso,
                                                                                   CreatedAt = permiso.CreatedAt,
                                                                                   UpdatedAt = permiso.UpdatedAt,
                                                                                   InactivatedAt = permiso.InactivatedAt,
                                                                                   Inactivo = permiso.Inactivo
                                                                                   
                                                                                };



            foreach (var item in listaPermisos){

                VwModelPermisos vwModelPermisos = new VwModelPermisos
                {
                    IdPermiso = item.IdPermiso,
                    NombrePermiso = item.NombrePermiso,
                    CreatedAt = item.CreatedAt,
                    UpdatedAt = item.UpdatedAt,
                    InactivatedAt = item.InactivatedAt,
                    Inactivo = item.Inactivo,
                    Seleccionado = rolesPermisos.Where(c => c.IdPermiso == item.IdPermiso).Count() == 0 ? false : true,
                };

                vwModel.VwModelPermisos.Add(vwModelPermisos);

            }
            ViewBag.Title = "Editar Rol";
            return PartialView("../Roles/_CreateOrEditPartial", vwModel);
        }
        public async Task<IActionResult> CreateOrEdit(VwModelRolesPermisos vwModelRolesPermisos)
        {
               if (ModelState.IsValid) {

                if (vwModelRolesPermisos.IdRol == 0)
                {
                    if (RolNombreExist(vwModelRolesPermisos.NombreRol))
                    {
                        return BadRequest("El registro ya existe");
                    }
                    Roles roles = new Roles { IdRol = 0, NombreRol = vwModelRolesPermisos.NombreRol, CreatedAt = DateTime.Now, Inactivo = 0 };
                    roles = await generic.Post("Roles", roles);
                    foreach (var item in vwModelRolesPermisos.VwModelPermisos)
                    {

                        if (item.Seleccionado)
                        {
                            RolesPermisos rolesPermisos = await generic.Post("RolesPermisos",
                                                                                new RolesPermisos
                                                                                {
                                                                                    IdRolPermiso = 0,
                                                                                    IdRol = roles.IdRol,
                                                                                    IdPermiso = item.IdPermiso,
                                                                                    CreatedAt = DateTime.Now,
                                                                                    Inactivo = 0
                                                                                });
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }else {

                    Roles roles = vwModelRolesPermisos as Roles;

                    if (RolNombreExist(roles.NombreRol, roles.IdRol)) {


                        return BadRequest("El registro ya existe");
                    }

                    bool Actualizado = await generic.Put("Roles/", roles.IdRol, roles);

                    if (!Actualizado) {
                        return BadRequest("Ha ocurrido un error no se pudo actualizar la información");
                    }
                    var _rolesPermisos = await generic.GetAll<RolesPermisos>("RolesPermisos");

                    var IdRolPermisoToDelete = (from rolesPermisos in _rolesPermisos
                                                join vwPermisos in vwModelRolesPermisos.VwModelPermisos on rolesPermisos.IdPermiso equals vwPermisos.IdPermiso
                                                where rolesPermisos.IdRol == vwModelRolesPermisos.IdRol && vwPermisos.Seleccionado == false
                                                select rolesPermisos).ToList();


                    if (IdRolPermisoToDelete != null)
                    {
                        foreach (var item in IdRolPermisoToDelete)
                        {
                            await generic.Delete<RolesPermisos>("RolesPermisos/", item.IdRolPermiso);
                        }

                    }

                    foreach (var item in vwModelRolesPermisos.VwModelPermisos.Where(c => c.Seleccionado == true)) {

                        var idRolesPermisos = _rolesPermisos.Where(c => c.IdPermiso == item.IdPermiso && c.IdRol == vwModelRolesPermisos.IdRol).FirstOrDefault();

                        if (idRolesPermisos is null)
                        {
                            await generic.Post("RolesPermisos", new RolesPermisos
                            {
                                IdRolPermiso = 0,
                                IdRol = vwModelRolesPermisos.IdRol,
                                IdPermiso = item.IdPermiso,
                                CreatedAt = DateTime.Now,
                                Inactivo = 0
                            });
                        }
                    }

                    return RedirectToAction(nameof(Index));

                }
            }
            ViewBag.Title = "Editar Rol";

            return PartialView("../Roles/_CreateOrEditPartial", vwModelRolesPermisos);
        }

        public bool RolNombreExist(string nombre, decimal? id) {
            var rol = generic.GetAll<Roles>("Roles").Result.Find(c => c.NombreRol.Trim().ToLowerInvariant() == nombre.Trim().ToLowerInvariant() && c.IdRol!=id);
            if (rol == null)
                return false;
            else
                return true;
        }

        public bool RolNombreExist(string nombre) {
            var rol = generic.GetAll<Roles>("Roles").Result.Find(c => c.NombreRol.Trim().ToLowerInvariant() == nombre.Trim().ToLowerInvariant());
            if (rol == null)
                return false;
            else
                return true;
        }

    }
}