using Common.Entity.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Entity.Models
{
    [Table(Name = "USUARIOS")]
    public class Usuarios
    {
        [PrimaryKey(AutoIncrement = true)]
        [Field(Name = "IDUSUARIO")]
        public decimal IdUsuario { get; set; }
        [Field(Name = "USUARIO")]
        public string Usuario { get; set; }
        [Field(Name = "PASSWORD")]
        public string Password { get; set; }
        [Field(Name = "NOMBRE")]
        public string Nombre { get; set; }
        [Field(Name = "APELLIDO")]
        public string Apellido { get; set; }
        [Field(Name = "FECHANAC")]
        public DateTime? FechaNac { get; set; }
        [Field(Name ="TOKEN")]
        public string Token { get; set; }
        [Field(Name = "TOKENEXPIREDAT")]
        public DateTime? TokenExpiredAt { get; set; }
        [Field(Name = "IDROL")]
        public decimal IdRol { get; set; }
        [Field(Name = "TOKENREFRESH")]
        public string TokenRefresh { get; set; }
        [Field(Name = "CREATEDAT")]
        public DateTime CreatedAt { get; set; }
        [Field(Name = "INACTIVATEDAT")]
        public DateTime? InactivatedAt { get; set; }
        [Field(Name = "UPDATEDAT")]
        public DateTime? UpdatedAt { get; set; }
        [Field(Name ="INACTIVO")]
        public int Inactivo { get; set; }
    }
}
