using Common.Data.DAL;
using Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.BOL.BOL
{
   public class BOLActividades
    {
        public async Task<Actividades> CreateActividades(Actividades pActividades)
        {
            Actividades _actividades = new Actividades();
            Task<Actividades> t = Task.Run(() =>
            {
                using (DALDBContext context = new DALDBContext())
                {
                    DALActividades dal = new DALActividades(context);
                    _actividades = dal.CreateActividades(pActividades);
                }

                return _actividades;
            });

            return await t;
        }

        public List<Actividades> GetAllActividades()
        {

            List<Actividades> listaActividades = null;

            using (DALDBContext context = new DALDBContext())
            {
                DALActividades dal = new DALActividades(context);
                listaActividades = dal.GetAllActividades();
            }

            return listaActividades;
        }

        public async Task<Actividades> GetActividad(decimal id)
        {
            Actividades actividades = null;
            Task<Actividades> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALActividades dal = new DALActividades(context);
                    actividades = dal.GetActividad(id);
                }
                return actividades;
            });

            return await t;
        }

        public async Task<Actividades> UpdateActividades(decimal id, Actividades actividades)
        {
            Actividades _actividades = null;
            Task<Actividades> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALActividades dal = new DALActividades(context);
                    _actividades = dal.UpdateActividades(id, actividades);
                }
                return _actividades;
            });

            return await t;
        }

        public async Task<Actividades> DeleteActividades(decimal id)
        {
            Actividades actividades = null;
            Task<Actividades> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALActividades dal = new DALActividades(context);
                    actividades = dal.DeleteActividades(id);
                }
                return actividades;
            });


            return await t;
        }
    }
}
