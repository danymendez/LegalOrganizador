using Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data.DAL
{
   public class DALCasos:DALBaseOrcl
    {
        private DALDBContext context;
        public DALCasos(DALDBContext context)
            : base(context)
        {
            this.context = context;

        }


        public Casos CreateCasos(Casos pCasos)
        {

            return Create(pCasos);

        }

        public List<Casos> GetAllCasos()
        {
            return GetAll<Casos>();
        }

        public Casos DeleteCasos(decimal id)
        {
            return Delete<Casos>(id);
        }

        public Casos UpdateCasos(decimal id, Casos pCasos)
        {

            return Update(id, pCasos);
        }

        public Casos GetCaso(decimal id)
        {
            return Get<Casos>(id);
        }
    }
}
