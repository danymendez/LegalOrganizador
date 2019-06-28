using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.ViewModels
{
    public class VwModelActividadesPorEstado
    {
        public decimal IdActividad { get; set; }
        public string NombreActividad { get; set; }
        public DateTime StartDate { get; set; }


        public DateTime EndDate { get; set; }
        public string Estado { get; set; }
        public string Responsable { get; set; }
    }
}
