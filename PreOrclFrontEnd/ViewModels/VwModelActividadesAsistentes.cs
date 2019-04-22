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

        public Usuarios Responsable { get; set; }

        public List<VwModelAsistentes> ListVwModelAsistentes { get; set; }
        [Display(Name ="Asistentes")]
        [Required]
        public decimal[] IdAsistentes { get; set; }
    }
}
