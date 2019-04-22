using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.Models
{
    public class Actividades
    {

     
        public decimal IdActividad { get; set; }
   
        [Display(Name ="Nombre de la actividad")]
        [MaxLength(150)]
        public string NombreActividad { get; set; }

        [Display(Name ="Calendario del Abogado")]
        [MaxLength(1000)]
        public string IdCalendario { get; set; }

        [MaxLength(1000)]
        public string IdEvento { get; set; }

        [MaxLength(1)]
        public string Estado { get; set; }

        public decimal Costo { get; set; }

        [Display(Name ="Responsable")]
        public decimal IdResponsable { get; set; }

        [Display(Name = "Caso")]
        public decimal IdCaso { get; set; }

    
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? InactivatedAt { get; set; }

        
        public decimal Inactivo { get; set; }

        [Display(Name = "Fecha / Hora Inicio")]
        [Required]
        public DateTime StartTime { get; set; }
        [Display(Name = "Fecha / Hora Fin")]
        [Required]
        public DateTime EndTime { get; set; }

     
        public string TimeZone { get; set; }
    }
}
