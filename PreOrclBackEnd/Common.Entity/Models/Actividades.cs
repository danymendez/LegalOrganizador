using Common.Entity.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entity.Models
{
    [Table(Name = "ACTIVIDADES")]
    public class Actividades
    {
        [PrimaryKey]
        [Field(Name = "IDACTIVIDAD")]
        public decimal IdActividad { get; set; }
        [Field(Name = "NOMBREACTIVIDAD")]
        public string NombreActividad { get; set; }
        [Field(Name = "IDCALENDARIO")]
        public string IdCalendario { get; set; }
        [Field(Name = "IDEVENTO")]
        public string IdEvento { get; set; }
        [Field(Name = "ESTADO")]
        public string Estado { get; set; }
        [Field(Name = "COSTO")]
        public decimal Costo { get; set; }
        [Field(Name = "IDRESPONSABLE")]
        public decimal IdResponsable { get; set; }
        [Field(Name = "IDCASO")]
        public decimal IdCaso { get; set; }
        [Field(Name = "CREATEDAT")]
        public DateTime CreatedAt { get; set; }
        [Field(Name = "UPDATEDAT")]
        public DateTime? UpdatedAt { get; set; }
        [Field(Name = "INACTIVATEDAT")]
        public DateTime? InactivatedAt { get; set; }
        [Field(Name = "INACTIVO")]
        public decimal Inactivo { get; set; }
    }
}
