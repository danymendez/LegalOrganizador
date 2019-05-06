
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.Models
{ 
   
    public class Usuarios
    {
        [Key]
        public decimal IdUsuario { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public string Apellido { get; set; }

        public DateTime? FechaNac { get; set; }
   
        public string Token { get; set; }
       
        public DateTime? TokenExpiredAt { get; set; }
       
        public decimal IdRol { get; set; }
      
        public string TokenRefresh { get; set; }
     
        public DateTime CreatedAt { get; set; }
      
        public DateTime? InactivatedAt { get; set; }
     
        public DateTime? UpdatedAt { get; set; }
        
        public int Inactivo { get; set; }
        [Display(Name ="Tipo usuario")]
        public string TipoUsuario { get; set; }

    }
}
