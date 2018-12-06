using Common.Entity.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreOrclApi.Models
{ 
    [Table(Name ="Usuarios")]
    public class Usuarios
    {
        [PrimaryKey(AutoIncrement = false)]
        
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
    }
}
