using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entity.Models
{
    public class GraphEvents
    {
        public decimal IdUsuario { get; set; }

        public string IdCalendar { get; set; }
        public Event Event { get; set; }

       
    }
}
