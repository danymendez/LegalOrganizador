using Common.Entity.Models;
using Data.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.BOL.BOL
{
    public class BOLSisPerPersonas
    {
        public Task<SisPerPersona> CreatePersona(SisPerPersona sisPerPersona) {
            SisPerPersona sis = new SisPerPersona();
            Task<SisPerPersona> t = Task.Run(() =>
            {
                using (DALDBContext context = new DALDBContext()) {
                    DALSisPerPersona dal = new DALSisPerPersona(context);
                    sis = dal.CreateSisPerPersona(sisPerPersona);   
                }

                return sis;
            });

            return t;
        }

        public List<SisPerPersona> GetAllSisPerPersona() {

            List<SisPerPersona> listaSisPerPersona = null;

            using (DALDBContext context = new DALDBContext())
            {
                DALSisPerPersona dal = new DALSisPerPersona(context);
                listaSisPerPersona = dal.GetAllSisPerPersona();
            }

            return listaSisPerPersona;
        }

        public Task<SisPerPersona> GetSisPerPersona(int id) {
            SisPerPersona sis = null;
            Task<SisPerPersona> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALSisPerPersona dal = new DALSisPerPersona(context);
                    sis = dal.GetPersona(id);
                }
                return sis;
            });
            
            return t;
        }

        public Task<SisPerPersona> UpdateSisPerPersona(int id, SisPerPersona sisPerPersona) {
            SisPerPersona sis = null;
            Task<SisPerPersona> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALSisPerPersona dal = new DALSisPerPersona(context);
                    sis = dal.UpdateSisPerPersona(id,sisPerPersona);
                }
                return sis;
            });

            return t;
        }

    }

    
}
