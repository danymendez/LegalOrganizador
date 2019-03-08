using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreOrclApi.DTO
{
    public class UsuariosDetailsDTO
    {
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }

        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public DateTime? FechaNac { get; set; }

        public string Token { get; set; }

        public DateTime? TokenExpiredAt { get; set; }

        public string TokenRefresh { get; set; }

        public int IdRol { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? InactivatedAt { get; set; }

        public int Inactivo { get; set; }
    }
}
