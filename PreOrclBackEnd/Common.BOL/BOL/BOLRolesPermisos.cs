using Common.Entity.Models;
using Common.Data.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.BOL.BOL
{
  public class BOLRolesPermisos
    {
        public async Task<RolesPermisos> CreateRolesPermisos(RolesPermisos rolesPermisos)
        {
            RolesPermisos _rolesPermisos = new RolesPermisos();
            Task<RolesPermisos> t = Task.Run(() =>
            {
                using (DALDBContext context = new DALDBContext())
                {
                    DALRolesPermisos dal = new DALRolesPermisos(context);
                    _rolesPermisos = dal.CreateRolesPermisos(rolesPermisos);
                }

                return _rolesPermisos;
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

        public async Task<RolesPermisos> UpdateRolesPermisos(int id, RolesPermisos rolesPermisos)
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

        public async Task<RolesPermisos> DeleteRolesPermisos(int id)
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
