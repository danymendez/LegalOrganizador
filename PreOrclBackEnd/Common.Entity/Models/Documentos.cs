using Common.Entity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Entity.Models
{
    [Table(Name ="DOCUMENTOS")]
  public class Documentos
    {
        [PrimaryKey]
        [Field(Name ="IDDOCUMENTO")]
        public decimal IdDocumento { get; set; }
        [Field(Name = "IDCASO")]
        public decimal IdCaso { get; set; }
        [Field(Name = "URL")]
        public string Url { get; set; }
        [Field(Name = "NOMBRE")]
        public string Nombre { get; set; }

        [Field(Name = "CREATEDAT")]
        [Required]
        public DateTime CreatedAt { get; set; }
        [Field(Name = "UPDATEDAT")]
        public DateTime? UpdatedAt { get; set; }
        [Field(Name = "ARCHIVO")]
        public byte[] Archivo { get; set; }
    }
}
