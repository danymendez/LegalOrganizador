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

        [Required(ErrorMessage ="Requerido")]
        [Display(Name = "Fecha de apertura")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd\\/MM\\/yyyy HH:mm}")]
        public DateTime FechaApertura { get; set; }

        [Required(ErrorMessage ="Requerido")]
        [Display(Name = "Nombre caso")]
        public string NombreCaso { get; set; }

        [Display(Name ="Cliente")]
        [Required(ErrorMessage ="Requerido")]
        public decimal IdCliente { get; set; }

        [StringLength(1,ErrorMessage ="Máximo un caracter permitido")]
        [Required(ErrorMessage ="Requerido")]
        public string Categoria { get; set; }

        [MaxLength(1)]
        [Required(ErrorMessage ="Requerido")]
        public string Tipo { get; set; }

        [Required(ErrorMessage ="Requerido")]
        [Display(Name = "Precio pactado")]
        [RegularExpression(@"^[0-9]{1,3}(,[0-9]{3}){0,2}(\.[0-9]{2})$",ErrorMessage ="Cantidad no soportada")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,##0.00}")]
        public decimal PrecioPactado { get; set; }

        [Required(ErrorMessage ="Requerido")]
        [Display(Name = "Abogado")]
        public decimal IdAbogado { get; set; }

        [Required(ErrorMessage ="Requerido")]
        public decimal IdCreador { get; set; }

        [StringLength(2000, ErrorMessage = "Máximo 2000 caracteres permitidos")]
        [Display(Name = "Resolución")]
        public string Resolucion { get; set; }

        public decimal Cancelado { get; set; }

        [Display(Name = "Fecha de cierre")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd\\/MM\\/yyyy HH:mm}")]
        public DateTime? FechaCierre { get; set; }

        [Display(Name = "Usuario que cierra")]
        public decimal? IdUsuarioCierre { get; set; }

        [Required(ErrorMessage ="Requerido")]
        [MaxLength(1)]
        [Display(Name = "Estado del caso")]
        public string EstadoCaso { get; set; }

        [Required(ErrorMessage ="Requerido")]
        public DateTime CreatedAt { get; set; }
    
        public DateTime? UpdatedAt { get; set; }

        public DateTime? InactivatedAt { get; set; }

          [Required(ErrorMessage ="Requerido")]
        public decimal Inactivo { get; set; }
    }
}
