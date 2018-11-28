using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.Models
{ 
    
    public class SisPerPersona
    {
        [Key]
        public int per_IDPER { get; set; }

        [Display(Name ="Nombre / Razón",Prompt ="Nombre / Razón")]
        [Required(ErrorMessage = "Requerido")]
        [MaxLength(250)]
        public string per_nombre_razon { get; set; }
        [Display(Name = "Apellido / Comercial",Prompt ="Apellido / Comercial")]
        [Required(ErrorMessage = "Requerido")]
        [MaxLength(250)]
        public string per_apellido_comercial { get; set; }

        [Display(Name = "NIT",Prompt ="0000-000000-000-0")]
        [RegularExpression("[0-9]{4}-[0-9]{6}-[0-9]{3}-[0-9]{1}",ErrorMessage ="El formato no es válido Ejemplo 0123-123456-123-1")]
        [Required(ErrorMessage = "Requerido")]
        [MaxLength(50)]
        public string per_nit { get; set; }
        [Display(Name = "DUI / NCR", Prompt ="DUI / NCR")]
        [Required(ErrorMessage = "Requerido")]
        [MaxLength(50)]
        public string per_dui_nrc { get; set; }
        [Display(Name = "Departamento",Prompt ="Departamento")]
        [Required(ErrorMessage = "Requerido")]
        [MaxLength(50)]

        public string per_direccion_departamento { get; set; }
        [Display(Name = "Municipio",Prompt ="Municipio")]
        [Required(ErrorMessage = "Requerido")]
        [MaxLength(50)]
        public string per_direccion_municipio { get; set; }

        [Display(Name = "Dirección",Prompt ="Dirección")]
        [MaxLength(50)]
        public string per_direccion { get; set; }

        [Display(Name = "Teléfono",Prompt ="2666-6666")]
        [RegularExpression("[0-9]{4}-[0-9]{4} ", ErrorMessage = "El formato no es válido Ejemplo 7777-7777")]
        [MaxLength(20)]
        public string per_telefono { get; set; }

        [Display(Name = "Móvil",Prompt ="7777-7777")]
        [RegularExpression("[0-9]{4}-[0-9]{4}", ErrorMessage = "El formato no es válido Ejemplo 2777-7777")]
        [MaxLength(20)]
        public string per_movil { get; set; }

        [Display(Name = "Correo",Prompt ="ejemplo@ejemplo.com")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",ErrorMessage ="Formato de Correo incorrecto")]
        [MaxLength(80)]
        public string per_email { get; set; }

        [Display(Name = "Código",Prompt ="Código")]
        [MaxLength(50)]
        public string per_codigo { get; set; }

        [Display(Name = "Nacionalidad",Prompt ="Nacionalidad")]
        [MaxLength(50)]
        public string per_nacionalidad { get; set; }
        [Display(Name = "Tipo Contribuyente",Prompt ="Tipo Contribuyente")]
        [MaxLength(50)]
        public string per_tipo_contribullente { get; set; }

        [Display(Name = "Dirección Cliente",Prompt ="Dirección Cliente")]
        [MaxLength(50)]
        public string per_dir_cli { get; set; }

        [Display(Name = "Dirección Cobro",Prompt ="Dirección Cobro")]
        [MaxLength(50)]
        public string per_cobros { get; set; }
    }
}
