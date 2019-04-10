using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.Models
{
    public class Permisos
    {
        [Key]
        public decimal IdPermiso { get; set; }
   
        public string NombrePermiso { get; set; }
 
        public DateTime CreatedAt { get; set; }
       
        public DateTime? UpdatedAt { get; set; }
       
        public DateTime? InactivatedAt { get; set; }
       
        public int Inactivo { get; set; }
    }
}
