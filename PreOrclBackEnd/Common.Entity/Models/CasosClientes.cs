using Common.Entity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Entity.Models
{
    [Table(Name ="CASOSCLIENTES")]
   public class CasosClientes
    {
        [PrimaryKey]
        [Field(Name = "IDCASOCLIENTE")]
        public decimal IdCasoCliente { get; set; }
        [Field(Name = "IDCASO")]
        public decimal IdCaso { get; set; }
        [Field(Name = "IDCLIENTE")]
        public decimal IdCliente { get; set; }
        [Field(Name = "CREATEDAT")]
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
