using Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entity.ViewModels
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
