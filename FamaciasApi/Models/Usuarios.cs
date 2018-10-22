using FamaciasApi.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamaciasApi.Models
{ 
    [Table(Name ="Usuarios1")]
    public class Usuarios
    {
        [PrimaryKey(AutoIncrement = true)]
        [Field(Name = "id")]
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
    }
}
