using Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data.DAL
{
   public class DALActividades:DALBaseOrcl
    {
        private DALDBContext context;
        public DALActividades(DALDBContext context)
            : base(context)
        {
            this.context = context;

        }


        public Actividades CreateActividades(Actividades pActividades)
        {

            return Create(pActividades);

        }

        public List<Actividades> GetAllActividades()
        {
            return GetAll<Actividades>();
        }

        public Actividades DeleteActividades(decimal id)
        {
            return Delete<Actividades>(id);
        }

        public Actividades UpdateActividades(decimal id, Actividades pActividades)
        {

            return Update(id, pActividades);
        }

        public Actividades GetActividad(decimal id)
        {
            return Get<Actividades>(id);
        }
    }
}
