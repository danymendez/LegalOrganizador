using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.Models
{
    public class RolesPermisos
    {
       [Key]
        public decimal IdRolPermiso { get; set; }
        public decimal IdRol { get; set; }
        public decimal IdPermiso { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? InactivatedAt { get; set; }
        public int Inactivo { get; set; }
    }
}
