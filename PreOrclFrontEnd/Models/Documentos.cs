using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.Models
{
    public class Documentos
    {
     
        public decimal IdDocumento { get; set; }
  
        public decimal IdCaso { get; set; }
     
        public string Url { get; set; }
        
        public string Nombre { get; set; }

        public DateTime CreatedAt { get; set; }
      
        public DateTime? UpdatedAt { get; set; }

        public byte[] Archivo { get; set; }
    }
}
