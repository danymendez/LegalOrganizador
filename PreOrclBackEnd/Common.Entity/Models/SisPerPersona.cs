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
        [Field(Name = "PER_IDPER")]
        public decimal per_IDPER { get; set; }

        [Required]
        [MaxLength(250)]
        [Field(Name = "PER_NOMBRE_RAZON")]
        public string per_nombre_razon { get; set; }

        [Required]
        [MaxLength(250)]
        [Field(Name = "PER_APELLIDO_COMERCIAL")]
        public string per_apellido_comercial { get; set; }

        [Required]
        [MaxLength(50)]
        [Field(Name = "PER_NIT")]
        public string per_nit { get; set; }

        [Required]
        [MaxLength(50)]
        [Field(Name = "PER_DUI_NRC")]
        public string per_dui_nrc { get; set; }
        [Required]
        [MaxLength(50)]
        [Field(Name = "PER_DIRECCION_DEPARTAMENTO")]
        public string per_direccion_departamento { get; set; }
        [Required]
        [MaxLength(50)]
        [Field(Name = "PER_DIRECCION_MUNICIPIO")]
        public string per_direccion_municipio { get; set; }

        [MaxLength(50)]
        [Field(Name = "PER_DIRECCION")]
        public string per_direccion { get; set; }

        [MaxLength(20)]
        [Field(Name = "PER_TELEFONO")]
        public string per_telefono { get; set; }

        [MaxLength(20)]
        [Field(Name = "PER_MOVIL")]
        public string per_movil { get; set; }

        [MaxLength(80)]
        [Field(Name = "PER_EMAIL")]
        public string per_email { get; set; }

        [MaxLength(50)]
        [Field(Name = "PER_CODIGO")]
        public string per_codigo { get; set; }
        [MaxLength(50)]
        [Field(Name = "PER_NACIONALIDAD")]
        public string per_nacionalidad { get; set; }

        [MaxLength(50)]
        [Field(Name = "PER_TIPO_CONTRIBULLENTE")]
        public string per_tipo_contribullente { get; set; }


        [MaxLength(50)]
        [Field(Name = "PER_DIR_CLI")]
        public string per_dir_cli { get; set; }


        [MaxLength(50)]
        [Field(Name = "PER_COBROS")]
        public string per_cobros { get; set; }
    }
}
