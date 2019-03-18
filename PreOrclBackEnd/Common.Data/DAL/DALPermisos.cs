using Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data.DAL
{
    public class DALPermisos : DALBaseOrcl
    {

        private DALDBContext context;
        public DALPermisos(DALDBContext context)
            : base(context)
        {
            this.context = context;

        }


        public Permisos CreatePermisos(Permisos permisos)
        {

            return Create(permisos);

        }

        public List<Permisos> GetAllPermisos()
        {
            return GetAll<Permisos>();
        }

        public Permisos DeleteRol(int id)
        {
            return Delete<Permisos>(id);
        }

        public Permisos UpdatePermisos(long id, Permisos permisos)
        {

            return Update(id, permisos);
        }

        public Permisos GetPermisos(long id)
        {
            return Get<Permisos>(id);
        }
    }
}
