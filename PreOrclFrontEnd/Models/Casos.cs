using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.Models
{
    public class Casos
    {
       
        public decimal IdCaso { get; set; }
        [Required]
        public DateTime FechaApertura { get; set; }
      
        [Required]
        public decimal IdCliente { get; set; }

        [MaxLength(1)]
        [Required]
        public string Categoria { get; set; }

        [MaxLength(1)]
        [Required]
        public string Tipo { get; set; }

        [Required]
        public decimal PrecioPactado { get; set; }

        [Required]
        public decimal IdAbogado { get; set; }

        [Required]
        public decimal IdCreador { get; set; }

        [MaxLength(2000)]
        public string Resolucion { get; set; }

        public decimal Cancelado { get; set; }
        public DateTime? FechaCierre { get; set; }

        public decimal? IdUsuarioCierre { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    
        public DateTime? UpdatedAt { get; set; }

        public DateTime? InactivatedAt { get; set; }

        [Required]
        public decimal Inactivo { get; set; }
    }
}
