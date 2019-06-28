using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entity.ViewModels
{
   public class VwModelCargaPorUsuario
    {

        public decimal IdUsuario { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Usuario { get; set; }

        public int CantidadCasoAbierto { get; set; }
    }
}
