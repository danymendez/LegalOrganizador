using Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entity.ViewModels
{
   public class VwModelActividadesAsistentes
    {
        public Actividades Actividades { get; set; }
        public List<VwModelAsistentes> ListVwModelAsistentes { get; set; }

        public decimal[] IdAsistentes { get; set; }
    }
}
