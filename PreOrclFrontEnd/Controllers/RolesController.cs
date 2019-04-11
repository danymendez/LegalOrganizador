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
            return PartialView("../Roles/_CreateOrEditPartial", vwModel);
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

            return PartialView("../Roles/_CreateOrEditPartial", vwModel);
        }
        public async Task<IActionResult> CreateOrEdit(VwModelRolesPermisos vwModelRolesPermisos)
        {
                        if (ModelState.IsValid) {
                if (vwModelRolesPermisos.IdRol == 0)
                {
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
                }
                else {



                    var IdRolPermisoToDelete = (from rolesPermisos in await generic.GetAll<RolesPermisos>("RolesPermisos")
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

                    foreach (var item in generic.GetAll<RolesPermisos>("RolesPermisos").Result.Where(c => c.IdRol == vwModelRolesPermisos.IdRol)){

                        var listIdRolPermisoToAdd = vwModelRolesPermisos.VwModelPermisos.Where(c => c.IdPermiso != item.IdPermiso).Select(c => c.IdPermiso);
                        if (listIdRolPermisoToAdd == null)
                        {
                            foreach (var id in listIdRolPermisoToAdd)
                            {
                                await generic.Post("RolesPermisos", new RolesPermisos
                                {
                                    IdRolPermiso = 0,
                                    IdRol = vwModelRolesPermisos.IdRol,
                                    IdPermiso = id,
                                    CreatedAt = DateTime.Now,
                                    Inactivo = 0
                                });
                            }
                        }
                    }
                  
                                                  



                            
                            

                    return RedirectToAction(nameof(Index));

                }
            }

            return PartialView("../Roles/_CreateOrEditPartial", vwModelRolesPermisos);
        }

    }
}