using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Entity.Models
{ 
    
    public class SisPerPersona
    {
        [Key]
        public int per_IDPER { get; set; }

        [Display(Name ="Nombre / Razón")]
        [Required(ErrorMessage = "Requerido")]
        [MaxLength(250)]
        public string per_nombre_razon { get; set; }
        [Display(Name = "Apellido / Comercial")]
        [Required(ErrorMessage = "Requerido")]
        [MaxLength(250)]
        public string per_apellido_comercial { get; set; }

        [Display(Name = "NIT")]
        [Required(ErrorMessage = "Requerido")]
        [MaxLength(50)]
        public string per_nit { get; set; }
        [Display(Name = "DUI / NCR")]
        [Required(ErrorMessage = "Requerido")]
        [MaxLength(50)]
        public string per_dui_nrc { get; set; }
        [Display(Name = "Departamento")]
        [Required(ErrorMessage = "Requerido")]
        [MaxLength(50)]

        public string per_direccion_departamento { get; set; }
        [Display(Name = "Municipio")]
        [Required(ErrorMessage = "Requerido")]
        [MaxLength(50)]
        public string per_direccion_municipio { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(50)]
        public string per_direccion { get; set; }

        [Display(Name = "Teléfono")]
        [MaxLength(20)]
        public string per_telefono { get; set; }

        [Display(Name = "Móvil")]
        [MaxLength(20)]
        public string per_movil { get; set; }

        [Display(Name = "Correo")]
        [MaxLength(80)]
        public string per_email { get; set; }

        [Display(Name = "Código")]
        [MaxLength(50)]
        public string per_codigo { get; set; }

        [Display(Name = "Nacionalidad")]
        [MaxLength(50)]
        public string per_nacionalidad { get; set; }
        [Display(Name = "Tipo Contribuyente")]
        [MaxLength(50)]
        public string per_tipo_contribullente { get; set; }

        [Display(Name = "Dirección Cliente")]
        [MaxLength(50)]
        public string per_dir_cli { get; set; }

        [Display(Name = "Dirección Cobro")]
        [MaxLength(50)]
        public string per_cobros { get; set; }
    }
}
