using Common.Data.DAL;
using Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.BOL.BOL
{
   public class BOLActividadesAsistentes
    {
        public async Task<ActividadesAsistentes> CreateActividadesAsistentes(ActividadesAsistentes pActividadesAsistentes)
        {
            ActividadesAsistentes _actividadesAsistentes = new ActividadesAsistentes();
            Task<ActividadesAsistentes> t = Task.Run(() =>
            {
                using (DALDBContext context = new DALDBContext())
                {
                    DALActividadesAsistentes dal = new DALActividadesAsistentes(context);
                    _actividadesAsistentes = dal.CreateActividadesAsistentes(pActividadesAsistentes);
                }

                return _actividadesAsistentes;
            });

            return await t;
        }

        public List<ActividadesAsistentes> GetAllActividadesAsistentes()
        {

            List<ActividadesAsistentes> listaActividadesAsistentes = null;

            using (DALDBContext context = new DALDBContext())
            {
                DALActividadesAsistentes dal = new DALActividadesAsistentes(context);
                listaActividadesAsistentes = dal.GetAllActividadesAsistentes();
            }

            return listaActividadesAsistentes;
        }

        public async Task<ActividadesAsistentes> GetActividadAsistente(decimal id)
        {
            ActividadesAsistentes actividadesAsistentes = null;
            Task<ActividadesAsistentes> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALActividadesAsistentes dal = new DALActividadesAsistentes(context);
                    actividadesAsistentes = dal.GetActividadAsistente(id);
                }
                return actividadesAsistentes;
            });

            return await t;
        }

        public async Task<ActividadesAsistentes> UpdateActividadesAsistentes(decimal id, ActividadesAsistentes ActividadesAsistentes)
        {
            ActividadesAsistentes _actividadesAsistentes = null;
            Task<ActividadesAsistentes> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALActividadesAsistentes dal = new DALActividadesAsistentes(context);
                    _actividadesAsistentes = dal.UpdateActividadesAsistentes(id, ActividadesAsistentes);
                }
                return _actividadesAsistentes;
            });

            return await t;
        }

        public async Task<ActividadesAsistentes> DeleteActividadesAsistentes(decimal id)
        {
            ActividadesAsistentes actividadesAsistentes = null;
            Task<ActividadesAsistentes> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALActividadesAsistentes dal = new DALActividadesAsistentes(context);
                    actividadesAsistentes = dal.DeleteActividadesAsistentes(id);
                }
                return actividadesAsistentes;
            });


            return await t;
        }
    }
}
