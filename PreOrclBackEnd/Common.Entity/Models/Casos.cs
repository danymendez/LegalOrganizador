using Common.Entity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Entity.Models
{
    [Table(Name ="CASOS")]
    public class Casos
    {
        [PrimaryKey]
        [Field(Name ="IDCASO")]
        [Required]
        public decimal IdCaso { get; set; }
        [Field(Name ="FECHAAPERTURA")]
        [Required]
        public DateTime FechaApertura { get; set; }
        [Field(Name ="IDCLIENTE")]
        [Required]
        public decimal IdCliente { get; set; }
        [Field(Name = "CATEGORIA")]
        [MaxLength(1)]
        [Required]
        public string Categoria { get; set; }
        [Field(Name = "TIPO")]
        [MaxLength(1)]
        [Required]
        public string Tipo { get; set; }
        [Field(Name ="PRECIOPACTADO")]
        [Required]
        public decimal PrecioPactado { get; set; }
        [Field(Name ="IDABOGADO")]
        [Required]
        public decimal IdAbogado { get; set; }
        [Field(Name = "IDCREADOR")]
        [Required]
        public decimal IdCreador { get; set; }
        [Field(Name = "RESOLUCION")]
        [MaxLength(2000)]
        public string Resolucion { get; set; }

        [Field(Name = "CANCELADO")]
        public decimal Cancelado { get; set; }

        [Field(Name = "FECHACIERRE")]
        public DateTime? FechaCierre { get; set; }

        [Field(Name = "IDUSUARIOCIERRE")]
        public decimal? IdUsuarioCierre { get; set; }



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

    }
}
