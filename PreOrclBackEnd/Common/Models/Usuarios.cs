using Common.Entity.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Entity.Models
{ 
    [Table(Name ="USUARIOS")]
    public class Usuarios
    {
        [PrimaryKey(AutoIncrement = true)]
        [Field(Name ="IDUSUARIO")]
        public int IdUsuario { get; set; }
        [Field(Name = "USUARIO")]
        public string Usuario { get; set; }
        [Field(Name = "PASSWORD")]
        public string Password { get; set; }
        [Field(Name = "NOMBRE")]
        public string Nombre { get; set; }
        [Field(Name = "APELLIDO")]
        public string Apellido { get; set; }
    }
}
