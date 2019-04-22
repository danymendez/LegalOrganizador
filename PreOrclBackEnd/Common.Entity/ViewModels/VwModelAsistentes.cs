using Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entity.ViewModels
{
   public class VwModelAsistentes
    {
        public decimal IdActividadesAsistentes { get; set; }
        public decimal IdAsistente { get; set; }
        public string Correo { get; set; }

        public Usuarios Asistente { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
