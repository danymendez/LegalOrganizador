using Common.Entity.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entity.Models
{ 
    [Table(Name ="ROLESPERMISOS")]
    public class RolesPermisos
    {
        [Field(Name = "IDROLPERMISO")]
        [PrimaryKey]
        public int IdRolPermiso { get; set; }
        [Field(Name = "IDROL")]
        public int IdRol { get; set; }
        [Field(Name = "IDPERMISO")]
        public int IdPermiso { get; set; }

        [Field(Name = "CREATEDAT")]
        public DateTime CreatedAt { get; set; }
        [Field(Name = "UPDATEDAT")]
        public DateTime? UpdatedAt { get; set; }
        [Field(Name = "INACTIVATEDAT")]
        public DateTime? InactivatedAt { get; set; }
        [Field(Name = "INACTIVO")]
        public int Inactivo { get; set; }
    }
}
