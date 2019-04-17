using Common.Entity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [MaxLength(150)]
        public string NombreActividad { get; set; }
        [Field(Name = "IDCALENDARIO")]
        [MaxLength(1000)]
        public string IdCalendario { get; set; }
        [Field(Name = "IDEVENTO")]
        [MaxLength(1000)]
        public string IdEvento { get; set; }
        [Field(Name = "ESTADO")]
        [MaxLength(1)]
        public string Estado { get; set; }
        [Field(Name = "COSTO")]
        public decimal Costo { get; set; }
        [Field(Name = "IDRESPONSABLE")]
        public decimal IdResponsable { get; set; }
        [Field(Name = "IDCASO")]
        public decimal IdCaso { get; set; }
        [Field(Name = "CREATEDAT")]
        [Required]
        public DateTime CreatedAt { get; set; }
        [Field(Name = "UPDATEDAT")]
        public DateTime? UpdatedAt { get; set; }
        [Field(Name = "INACTIVATEDAT")]
        public DateTime? InactivatedAt { get; set; }
        [Field(Name = "INACTIVO")]
        [Required]
        public decimal Inactivo { get; set; }

        [Field(Name = "STARTTIME")]
        [Required]
        public DateTime StartTime { get; set; }
        [Field(Name = "ENDTIME")]
        [Required]
        public DateTime EndTime { get; set; }
        [Field(Name = "TIMEZONE")]
        [Required]
        public string TimeZone { get; set; }
    }
}
