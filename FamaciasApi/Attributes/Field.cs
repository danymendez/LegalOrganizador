using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamaciasApi.Attributes
{
    public class Field : Attribute
    {
        public string Name { get; set; }
    }
}
