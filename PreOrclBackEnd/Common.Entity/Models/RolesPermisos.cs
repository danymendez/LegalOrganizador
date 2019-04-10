using Common.Entity.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entity.Models
{ 
    [Table(Name ="ROLESPERMISOS")]
    public class RolesPermisos
    {
        [PrimaryKey]
        [Field(Name = "IDROLPERMISO")]

        public decimal IdRolPermiso { get; set; }
        [Field(Name = "IDROL")]
        public decimal IdRol { get; set; }
        [Field(Name = "IDPERMISO")]
        public decimal IdPermiso { get; set; }

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
