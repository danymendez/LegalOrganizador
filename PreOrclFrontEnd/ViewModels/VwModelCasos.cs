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
        public SisPerPersona Cliente { get; set; }

        public List<SisPerPersona> ListadoImputados { get; set; }

        public decimal[] IdImputados { get; set; }
        public List<Documentos> ListadoDocumentos { get; set; }
        public decimal[] IdDocumentos { get; set; }
        public Usuarios Abogado { get; set; }
        public List<VwModelActividadesAsistentes> ListVwModelActividadesAsistentes { get; set; }
    }
}
