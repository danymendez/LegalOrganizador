using Common.Data.DAL;
using Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.BOL.BOL
{
    public class BOLPermisos
    {
        public async Task<Permisos> CreateRolesPermisos(Permisos permisos)
        {
            Permisos _permisos = new Permisos();
            Task<Permisos> t = Task.Run(() =>
            {
                using (DALDBContext context = new DALDBContext())
                {
                    DALPermisos dal = new DALPermisos(context);
                    _permisos = dal.CreatePermisos(permisos);
                }

                return _permisos;
            });

            return await t;
        }

        public List<Permisos> GetAllPermisos()
        {

            List<Permisos> listaPermisos = null;

            using (DALDBContext context = new DALDBContext())
            {
                DALPermisos dal = new DALPermisos(context);
                listaPermisos = dal.GetAllPermisos();
            }

            return listaPermisos;
        }

        public async Task<Permisos> GetPermisos(int id)
        {
            Permisos rolespermiso = null;
            Task<Permisos> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALPermisos dal = new DALPermisos(context);
                    rolespermiso = dal.GetPermisos(id);
                }
                return rolespermiso;
            });

            return await t;
        }

        public async Task<Permisos> UpdatePermisos(int id, Permisos permisos)
        {
            Permisos _permisos = null;
            Task<Permisos> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALPermisos dal = new DALPermisos(context);
                    _permisos = dal.UpdatePermisos(id, permisos);
                }
                return _permisos;
            });

            return await t;
        }

        public async Task<Permisos> DeletePermisos(int id)
        {
            Permisos _permisos = null;
            Task<Permisos> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALPermisos dal = new DALPermisos(context);
                    _permisos = dal.DeletePermisos(id);
                }
                return _permisos;
            });

            return await t;
        }
    }
}
