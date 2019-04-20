using PreOrclFrontEnd.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.ViewModels
{
    public class VwModelActividadesAsistentes
    {
        public Actividades Actividades { get; set; }
        public List<VwModelAsistentes> ListVwModelAsistentes { get; set; }
        [Required]
        public decimal[] IdAsistentes { get; set; }
    }
}
