using Common.Entity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Entity.Models
{
    [Table(Name ="ACTIVIDADESASISTENTES")]
    public class ActividadesAsistentes
    {
        [PrimaryKey]
        [Field(Name ="IDACTIVIDADASISTENTE")]
        public decimal IdActividadAsistentes { get; set; }
        [Field(Name ="IDACTIVIDAD")]
        public decimal IdActividad { get; set; }
        [Field(Name = "IDASISTENTE")]
        public decimal IdAsistente { get; set; }
        [Field(Name = "CREATEDAT")]
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
