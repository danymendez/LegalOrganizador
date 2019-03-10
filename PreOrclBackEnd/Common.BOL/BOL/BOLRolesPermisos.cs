using Common.Entity.Models;
using Data.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.BOL.BOL
{
  public class BOLRolesPermisos
    {
        public async Task<Roles> CreateRolesPermisos(Roles roles)
        {
            Roles _rol = new Roles();
            Task<Roles> t = Task.Run(() =>
            {
                using (DALDBContext context = new DALDBContext())
                {
                    DALRoles dal = new DALRoles(context);
                    _rol = dal.CreateRoles(roles);
                }

                return _rol;
            });

            return await t;
        }

        public List<RolesPermisos> GetAllRolesPermisos()
        {

            List<RolesPermisos> listaRoles = null;

            using (DALDBContext context = new DALDBContext())
            {
                DALRolesPermisos dal = new DALRolesPermisos(context);
                listaRoles = dal.GetAllRolesPermisos();
            }

            return listaRoles;
        }

        public async Task<RolesPermisos> GetRolesPermisos(int id)
        {
            RolesPermisos rolespermiso = null;
            Task<RolesPermisos> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALRolesPermisos dal = new DALRolesPermisos(context);
                    rolespermiso = dal.GetRolesPermisos(id);
                }
                return rolespermiso;
            });

            return await t;
        }

        public async Task<RolesPermisos> UpdateRoles(int id, RolesPermisos rolesPermisos)
        {
            RolesPermisos _rolesPermisos = null;
            Task<RolesPermisos> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALRolesPermisos dal = new DALRolesPermisos(context);
                    _rolesPermisos = dal.UpdateRolesPermisos(id, rolesPermisos);
                }
                return _rolesPermisos;
            });

            return await t;
        }

        public async Task<RolesPermisos> DeleteRoles(int id)
        {
            RolesPermisos _rolesPermisos = null;
            Task<RolesPermisos> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALRolesPermisos dal = new DALRolesPermisos(context);
                    _rolesPermisos = dal.DeleteRolesPermisos(id);
                }
                return _rolesPermisos;
            });

            return await t;
        }
    }
}
