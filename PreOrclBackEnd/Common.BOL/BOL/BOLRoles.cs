using Common.Entity.Models;
using Common.Data.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.BOL.BOL
{
   public class BOLRoles
    {
        public async Task<Roles> CreateRoles(Roles roles)
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

        public List<Roles> GetAllRoles()
        {

            List<Roles> listaRoles = null;

            using (DALDBContext context = new DALDBContext())
            {
                DALRoles dal = new DALRoles(context);
                listaRoles = dal.GetAllRoles();
            }

            return listaRoles;
        }

        public async Task<Roles> GetRol(int id)
        {
            Roles _rol = null;
            Task<Roles> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALRoles dal = new DALRoles(context);
                    _rol = dal.GetRol(id);
                }
                return _rol;
            });

            return await t;
        }

        public async Task<Roles> UpdateRoles(int id, Roles roles)
        {
            Roles _rol = null;
            Task<Roles> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALRoles dal = new DALRoles(context);
                    _rol = dal.UpdateRoles(id, roles);
                }
                return _rol;
            });

            return await t;
        }

        public async Task<Roles> DeleteRoles(int id)
        {
            Roles _rol = null;
            Task<Roles> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALRoles dal = new DALRoles(context);
                    _rol = dal.DeleteRol(id);
                }
                return _rol;
            });

            return await t;
        }
    }
}
