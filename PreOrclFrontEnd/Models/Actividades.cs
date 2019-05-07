using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.Models
{
    public class Actividades
    {


        public decimal IdActividad { get; set; }

        [Display(Name = "Nombre de la actividad")]
        [StringLength(150)]
        [MaxLength(150)]
        [Required(ErrorMessage = "Requerido")]
        public string NombreActividad { get; set; }

        [Display(Name = "Calendario del Abogado")]
        [MaxLength(1000)]
        [Required(ErrorMessage = "Requerido")]
        public string IdCalendario { get; set; }

        [MaxLength(1000)]
        public string IdEvento { get; set; }

        [MaxLength(1)]
        [Required(ErrorMessage = "Requerido")]
        public string Estado { get; set; }

      
        [Required(ErrorMessage = "Requerido")]
       
        public decimal Costo { get; set; }

        [Display(Name ="Responsable")]
        [Required(ErrorMessage = "Requerido")]
        public decimal IdResponsable { get; set; }

        [Display(Name = "Caso")]
        public decimal IdCaso { get; set; }

    
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? InactivatedAt { get; set; }

        
        public decimal Inactivo { get; set; }

        [Display(Name = "Fecha / Hora Inicio")]
        [Required(ErrorMessage = "Requerido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd\\/MM\\/yyyy HH:mm}")]
        public DateTime StartTime { get; set; }
        [Display(Name = "Fecha / Hora Fin")]
        [Required(ErrorMessage = "Requerido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd\\/MM\\/yyyy HH:mm}")]
        
        public DateTime EndTime { get; set; }

     
        public string TimeZone { get; set; }
    }
}
