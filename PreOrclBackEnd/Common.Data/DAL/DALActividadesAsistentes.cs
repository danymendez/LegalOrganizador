using Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data.DAL
{
   public class DALActividadesAsistentes:DALBaseOrcl
    {
        private DALDBContext context;
        public DALActividadesAsistentes(DALDBContext context)
            : base(context)
        {
            this.context = context;

        }


        public ActividadesAsistentes CreateActividadesAsistentes(ActividadesAsistentes pActividadesAsistentes)
        {

            return Create(pActividadesAsistentes);

        }

        public List<ActividadesAsistentes> GetAllActividadesAsistentes()
        {
            return GetAll<ActividadesAsistentes>();
        }

        public ActividadesAsistentes DeleteActividadesAsistentes(decimal id)
        {
            return Delete<ActividadesAsistentes>(id);
        }

        public ActividadesAsistentes UpdateActividadesAsistentes(decimal id, ActividadesAsistentes pActividadesAsistentes)
        {

            return Update(id, pActividadesAsistentes);
        }

        public ActividadesAsistentes GetActividadAsistente(decimal id)
        {
            return Get<ActividadesAsistentes>(id);
        }
    }
}
