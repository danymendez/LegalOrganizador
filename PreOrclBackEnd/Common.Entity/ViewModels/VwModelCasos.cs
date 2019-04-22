using Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entity.ViewModels
{
    public class VwModelCasos
    {
        public Casos Casos { get; set; }

        public List<VwModelActividadesAsistentes> ListVwModelActividadesAsistentes { get; set; }
    }
}
