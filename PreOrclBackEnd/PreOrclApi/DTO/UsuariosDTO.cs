using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreOrclApi.DTO
{
    public class UsuariosDTO
    {
         public int IdUsuario { get; set; } 
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}
