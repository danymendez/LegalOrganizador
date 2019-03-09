using Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DAL
{
   public class DALRoles : DALBaseOrcl
    {
        private DALDBContext context;
        public DALRoles(DALDBContext context)
            : base(context)
        {
            this.context = context;

        }


        public Roles CreateRoles(Roles roles)
        {

            return Create(roles);

        }

        public List<Roles> GetAllRoles()
        {
            return GetAll<Roles>();
        }

        public Roles DeleteRol(long id)
        {


            return Delete<Roles>(id);
        }

        public SisPerPersona UpdateRoles(long id, SisPerPersona roles)
        {

            return Update(id, roles);
        }

        public Roles GetRol(long id)
        {
            return Get<Roles>(id);
        }
    }
}
