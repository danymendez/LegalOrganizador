using Common.Data.DAL;
using Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.BOL.BOL
{
   public class BOLCasos
    {
        public async Task<Casos> CreateCasos(Casos pCasos)
        {
            Casos _casos = new Casos();
            Task<Casos> t = Task.Run(() =>
            {
                using (DALDBContext context = new DALDBContext())
                {
                    DALCasos dal = new DALCasos(context);
                    _casos = dal.CreateCasos(pCasos);
                }

                return _casos;
            });

            return await t;
        }

        public List<Casos> GetAllCasos()
        {

            List<Casos> listaCasos = null;

            using (DALDBContext context = new DALDBContext())
            {
                DALCasos dal = new DALCasos(context);
                listaCasos = dal.GetAllCasos();
            }

            return listaCasos;
        }

        public async Task<Casos> GetCaso(decimal id)
        {
            Casos casos = null;
            Task<Casos> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALCasos dal = new DALCasos(context);
                    casos = dal.GetCaso(id);
                }
                return casos;
            });

            return await t;
        }

        public async Task<Casos> UpdateCasos(decimal id, Casos pCasos)
        {
            Casos _casos = null;
            Task<Casos> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALCasos dal = new DALCasos(context);
                    _casos = dal.UpdateCasos(id, pCasos);
                }
                return _casos;
            });

            return await t;
        }

        public async Task<Casos> DeleteCasos(decimal id)
        {
            Casos Casos = null;
            Task<Casos> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALCasos dal = new DALCasos(context);
                    Casos = dal.DeleteCasos(id);
                }
                return Casos;
            });


            return await t;
        }
    }
}
