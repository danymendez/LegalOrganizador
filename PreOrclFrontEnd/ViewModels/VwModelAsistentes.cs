using PreOrclFrontEnd.Models;
using System;

namespace PreOrclFrontEnd.ViewModels
{
    public class VwModelAsistentes
    {
        public decimal IdActividadesAsistentes { get; set; }
        public decimal IdAsistente { get; set; }

        public Usuarios Asistente { get; set; }
        public string Correo { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}