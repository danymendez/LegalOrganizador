using Common.Entity.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entity.Models
{
    [Table(Name ="ROLES")]
    public class Roles
    {
        [PrimaryKey]
        [Field(Name ="IDROL")]
        public decimal IdRol { get; set; }
        [Field(Name = "NOMBREROL")]
        public string NombreRol { get; set; }
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
