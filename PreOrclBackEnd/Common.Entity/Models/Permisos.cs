using Common.Entity.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entity.Models
{
    [Table(Name ="PERMISOS")]
   public class Permisos
    {
        [Field(Name = "IDPERMISO")]
        [PrimaryKey]
        public int IdPermiso { get; set; }
        [Field(Name ="NOMBREPERMISO")]
        public string NombrePermiso { get; set; }
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
