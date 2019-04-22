using PreOrclFrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.ViewModels
{
    public class VwModelCasos
    {
        public Casos Casos { get; set; }

        public List<VwModelActividadesAsistentes> ListVwModelActividadesAsistentes { get; set; }
    }
}
