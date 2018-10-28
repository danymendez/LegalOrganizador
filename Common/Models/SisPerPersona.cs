using Common.Entity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Entity.Models
{ 
    [Table(Name ="SIS_PER_PERSONA")]
    public class SisPerPersona
    {
        [PrimaryKey]
        [Key]
        public int per_IDPER { get; set; }

        [Required]
        [MaxLength(250)]
        public string per_nombre_razon { get; set; }

        [Required]
        [MaxLength(250)]
        public string per_apellido_comercial { get; set; }

        [Required]
        [MaxLength(50)]
        public string per_nit { get; set; }

        [Required]
        [MaxLength(50)]
        public string per_dui_nrc { get; set; }
        [Required]
        [MaxLength(50)]

        public string per_direccion_departamento { get; set; }
        [Required]
        [MaxLength(50)]
        public string per_direccion_municipio { get; set; }

        [MaxLength(50)]
        public string per_direccion { get; set; }

        [MaxLength(20)]
        public string per_telefono { get; set; }

        [MaxLength(20)]
        public string per_movil { get; set; }

        [MaxLength(80)]
        public string per_email { get; set; }

        [MaxLength(50)]
        public string per_codigo { get; set; }
        public string per_nacionalidad { get; set; }

        [MaxLength(50)]
        public string per_tipo_contribullente { get; set; }


        [MaxLength(50)]
        public string per_dir_cli { get; set; }


        [MaxLength(50)]
        public string per_cobros { get; set; }
    }
}
