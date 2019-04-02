using Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data.DAL
{
    public class DALRolesPermisos : DALBaseOrcl
    {
        private DALDBContext context;
        public DALRolesPermisos(DALDBContext context)
            : base(context)
        {
            this.context = context;
        }


        public RolesPermisos CreateRolesPermisos(RolesPermisos roles)
        {
            return Create(roles);
        }

        public List<RolesPermisos> GetAllRolesPermisos()
        {
            return GetAll<RolesPermisos>();
        }

        public RolesPermisos DeleteRolesPermisos(decimal id)
        {
            return Delete<RolesPermisos>(id);
        }

        public RolesPermisos UpdateRolesPermisos(decimal id, RolesPermisos roles)
        {
            return Update(id, roles);
        }

        public RolesPermisos GetRolesPermisos(decimal id)
        {
            return Get<RolesPermisos>(id);
        }
    }
}
