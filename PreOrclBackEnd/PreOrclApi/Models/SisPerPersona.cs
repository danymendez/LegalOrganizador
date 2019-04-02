using Common.Entity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PreOrclApi.Models
{ 
    [Table(Name ="SIS_PER_PERSONA")]
    public class SisPerPersona
    {
        [PrimaryKey(AutoIncrement = false)]
        [Key]
        public double per_IDPER { get; set; }
        public string per_nombre_razon { get; set; }
        public string per_apellido_comercial { get; set; }
        public string per_nit { get; set; }
        public string per_dui_nrc { get; set; }
        public string per_direccion_departamento { get; set; }
        public string per_direccion_municipio { get; set; }
        public string per_direccion { get; set; }
        public string per_telefono { get; set; }
        public string per_movil { get; set; }
        public string per_email { get; set; }
        public string per_codigo { get; set; }
        public string per_nacionalidad { get; set; }
        public string per_tipo_contribullente { get; set; }
        public string per_dir_cli { get; set; }
        public string per_cobros { get; set; }
    }
}
