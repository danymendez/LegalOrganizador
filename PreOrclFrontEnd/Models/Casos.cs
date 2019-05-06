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
        [Display(Name = "Fecha de apertura")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        public DateTime FechaApertura { get; set; }

        [Required]
        public string NombreCaso { get; set; }

        [Display(Name ="Cliente")]
        [Required]
        public decimal IdCliente { get; set; }

        [MaxLength(1)]
        [Required]
        public string Categoria { get; set; }

        [MaxLength(1)]
        [Required]
        public string Tipo { get; set; }

        [Required]
        [Display(Name = "Precio pactado")]
        public decimal PrecioPactado { get; set; }

        [Required]
        [Display(Name = "Abogado")]
        public decimal IdAbogado { get; set; }

        [Required]
        public decimal IdCreador { get; set; }

        [MaxLength(2000)]
        [Display(Name = "Resolución")]
        public string Resolucion { get; set; }

        public decimal Cancelado { get; set; }

        [Display(Name = "Fecha de cierre")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime? FechaCierre { get; set; }

        [Display(Name = "Usuario que cierra")]
        public decimal? IdUsuarioCierre { get; set; }

        [Required]
        [MaxLength(1)]
        [Display(Name = "Estado del caso")]
        public string EstadoCaso { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    
        public DateTime? UpdatedAt { get; set; }

        public DateTime? InactivatedAt { get; set; }

        [Required]
        public decimal Inactivo { get; set; }
    }
}
