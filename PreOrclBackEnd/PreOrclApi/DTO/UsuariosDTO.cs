using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreOrclApi.DTO
{
    public class UsuariosDTO
    {
         public decimal IdUsuario { get; set; } 
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpired { get; set; }
        public string TokenRefresh { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? InactivatedAt { get; set; }
        public int Inactivo { get; set; }
    }
}
