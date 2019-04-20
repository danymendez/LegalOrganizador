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
   
        [MaxLength(150)]
        public string NombreActividad { get; set; }

        [MaxLength(1000)]
        public string IdCalendario { get; set; }

        [MaxLength(1000)]
        public string IdEvento { get; set; }

        [MaxLength(1)]
        public string Estado { get; set; }

        public decimal Costo { get; set; }

        public decimal IdResponsable { get; set; }

        public decimal IdCaso { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? InactivatedAt { get; set; }

        [Required]
        public decimal Inactivo { get; set; }

 
        [Required]
        public DateTime StartTime { get; set; }
    
        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public string TimeZone { get; set; }
    }
}
