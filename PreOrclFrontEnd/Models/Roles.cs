using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.Models
{
    public class Roles
    {
        [Key]
        public decimal IdRol { get; set; }
        [Required]
        [Display(Name ="Rol")]
        [MaxLength(20)]
        public string NombreRol { get; set; }

        public DateTime CreatedAt { get; set; }
 
        public DateTime? UpdatedAt { get; set; }
 
        public DateTime? InactivatedAt { get; set; }

        public int Inactivo { get; set; }
    }
}
